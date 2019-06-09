using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NAI_AStar
{
    public class Map
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public int Height => _map.GetLength(0);
        public int Width => _map.GetLength(1);
        private readonly Node[,] _map;

        public Map(Point start, Point end)
        {
            _map = new[,]
            {
                {new Node(NodeType.Street), new Node(NodeType.Street),new Node(NodeType.Grass) , new Node(NodeType.Street) },
                {new Node(NodeType.Street), new Node(NodeType.Street),new Node(NodeType.Wall) , new Node(NodeType.Street) },
                {new Node(NodeType.Street), new Node(NodeType.Street),new Node(NodeType.Wall) , new Node(NodeType.Street) },
                {new Node(NodeType.Street), new Node(NodeType.Street),new Node(NodeType.Wall) , new Node(NodeType.Street) },
                {new Node(NodeType.Street), new Node(NodeType.Street),new Node(NodeType.Wall) , new Node(NodeType.Street) },
                {new Node(NodeType.Street), new Node(NodeType.Street),new Node(NodeType.Street) , new Node(NodeType.Street) }
            };

            if (!InBounds(start))
                throw new Exception($"Starting point ({start.X}, {start.Y}) was outside the bounds of the map.");

            if (!InBounds(end))
                throw new Exception($"Ending point ({end.X}, {end.Y}) was outside the bounds of the map.");

            if (!IsPassable(start))
                throw new Exception($"Starting point ({start.X}, {start.Y}) can't be set on impassable node ({_map[start.X, start.Y].Type.ToString()}).");

            if (!IsPassable(end))
                throw new Exception($"Ending point ({end.X}, {end.Y}) can't be impassable node ({_map[end.Y, end.X].Type.ToString()}).");
            
            Start = start;
            End = end;
        }

        private bool InBounds(int x, int y)
        {
            return x < Width && x >= 0 && y < Height && y >= 0;
        }

        private bool InBounds(Point point)
        {
            return InBounds(point.X, point.Y);
        }

        private bool InBounds(Node node)
        {
            return InBounds(node.X, node.Y);
        }

        private bool IsStart(int x, int y)
        {
            return Start.X == x && Start.Y == y;
        }

        private bool IsEnd(int x, int y)
        {
            return End.X == x && End.Y == y;
        }
        private bool IsPassable(Point point)
        {
            return _map[point.Y, point.X].Type <= 0;
        }
        public Node GetNode(int x, int y)
        {
            return _map[y, x];
        }

        public Node GetNode(Point location)
        {
            return GetNode(location.X, location.Y);
        }

        private static IEnumerable<Point> GetNeighborPoints(int x, int y)
        {
            return new[]
            {
                new Point(x+1, y  ),
                new Point(x,   y-1),
                new Point(x-1, y  ),
                new Point(x,   y+1)
            };
        }

        private static IEnumerable<Point> GetNeighborPoints(Point location)
        {
            return GetNeighborPoints(location.X, location.Y);
        }

        private static IEnumerable<Point> GetNeighborPoints(Node node)
        {
            return GetNeighborPoints(node.X, node.Y);
        }

        public IEnumerable<Node> GetPassableNeighbors(Node node)
        {
            foreach (var neighborPoint in GetNeighborPoints(node))
            {
                var neighborNode = GetNode(neighborPoint);

                if (InBounds(neighborNode) && neighborNode.IsPassable)
                    yield return neighborNode;
            }
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('+');
            sb.Append('-', Width);
            sb.Append('+');
            sb.AppendLine();

            for (int i = 0; i < Height; i++)
            {
                sb.Append('|');
                for (int j = 0; j < Width; j++)
                {
                    if (IsStart(j, i) || IsEnd(j, i))
                    {
                        sb.Append('+');
                    }
                    else
                    {
                        sb.Append(_map[i, j].Type.TypeToChar());
                    }
                }

                sb.Append('|');
                sb.AppendLine();
            }

            sb.Append('+');
            sb.Append('-', Width);
            sb.AppendLine("+");

            sb.AppendLine($"Starting point: {Start.X}, {Start.Y}");
            sb.AppendLine($"Ending point: {End.X}, {End.Y}");

            return sb.ToString();
        }
    }
}