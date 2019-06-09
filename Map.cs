using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NAIAStar
{
    public class Map
    {
        private IEnumerable<Node> _map;
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public int Height => _map.Max(n => n.Y) + 1;
        public int Width => _map.Max(n => n.X) + 1;
        public IEnumerable<Node> GetMap() => _map;

        public Map(string[] map)
        {
            Start = new Point(int.MaxValue, int.MaxValue);
            End = new Point(int.MaxValue, int.MaxValue);

            _map = ConvertToMap(map);

            if (Start == new Point(int.MaxValue, int.MaxValue))
                throw new Exception($"Starting point does not exists");

            if (End == new Point(int.MaxValue, int.MaxValue))
                throw new Exception($"Ending point does not exists");
            
            if (!InBounds(Start))
                throw new Exception($"Starting point ({Start.X}, {Start.Y}) was outside the bounds of the map.");

            if (!InBounds(End))
                throw new Exception($"Ending point ({End.X}, {End.Y}) was outside the bounds of the map.");

            if (!IsPassable(Start))
                throw new Exception($"Starting point ({Start.X}, {Start.Y}) can't be set on impassable node ({GetNode(Start.X, Start.Y).Type.ToString()}).");

            if (!IsPassable(End))
                throw new Exception($"Ending point ({End.X}, {End.Y}) can't be impassable node ({GetNode(End.Y, End.X).Type.ToString()}).");

            GetStartNode().IsOpen = false;

            foreach (var node in _map)
            {
                node.SetHeuristic(GetEndNode());
            }
        }


        public Node GetEndNode()
        {
            return GetNode(End);
        }

        public Node GetStartNode()
        {
            return GetNode(Start);
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
            return GetNode(point.X, point.Y).IsPassable;
        }

        public Node GetNode(int x, int y)
        {
            return _map.Single(n => n.X == x && n.Y == y);
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

        public List<Node> GetPassableOpenNeighbors(Node node)
        {
            var list = new List<Node>();

            foreach (var neighborPoint in GetNeighborPoints(node))
            {
                if (!InBounds(neighborPoint))
                    continue;

                var neighborNode = GetNode(neighborPoint);

                if (neighborNode.IsPassable && neighborNode.IsOpen)
                    list.Add(neighborNode);
            }

            return list;
        }

        private IEnumerable<Node> ConvertToMap(string[] array)
        {
            var height = array.Length;
            var width = array[0].Length;
            var list = new List<Node>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var type = array[y][x];

                    if (char.ToUpper(type) == 'S')
                        Start = new Point(x, y);

                    if (char.ToUpper(type) == 'E')
                        End = new Point(x, y);

                    list.Add(new Node(type.CharToType(), x, y));
                }
            }

            return list;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (IsStart(j, i) || IsEnd(j, i))
                    {
                        sb.Append('+');
                    }
                    else
                    {
                        sb.Append(GetNode(j, i).IsPath ? '~' : GetNode(j, i).Type.TypeToChar());
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}