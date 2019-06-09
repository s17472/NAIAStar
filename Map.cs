﻿using System;
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
        public IEnumerable<Node> GetMapAsEnumerable() => _map.Cast<Node>().AsEnumerable();

        public Map(Point start, Point end)
        {
            _map = new[,]
            {
                {new Node(NodeType.Street, 0, 0), new Node(NodeType.Street, 1, 0),new Node(NodeType.Street, 2, 0) , new Node(NodeType.Street, 3, 0) },
                {new Node(NodeType.Street, 0, 1), new Node(NodeType.Street, 1, 1),new Node(NodeType.Wall, 2, 1) , new Node(NodeType.Street, 3, 1) },
                {new Node(NodeType.Street, 0, 2), new Node(NodeType.Street, 1, 2),new Node(NodeType.Wall, 2, 2) , new Node(NodeType.Street, 3, 2) },
                {new Node(NodeType.Street, 0, 3), new Node(NodeType.Street, 1, 3),new Node(NodeType.Wall, 2, 3) , new Node(NodeType.Street, 3, 3) },
                {new Node(NodeType.Street, 0, 4), new Node(NodeType.Street, 1, 4),new Node(NodeType.Wall, 2, 4) , new Node(NodeType.Street, 3, 4) },
                {new Node(NodeType.Street, 0, 5), new Node(NodeType.Street, 1, 5),new Node(NodeType.Street, 2, 5) , new Node(NodeType.Street, 3, 5) }
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
            GetNode(start).IsOpen = false;
            End = end;

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
            return _map[point.Y, point.X].IsPassable;
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
                        if (GetNode(j, i).IsPath)
                        {
                            sb.Append('%');
                        }
                        else
                        {
                            sb.Append(_map[i, j].Type.TypeToChar());
                        }
                    }
                }

                sb.Append('|');
                sb.AppendLine();
            }

            sb.Append('+');
            sb.Append('-', Width);
            sb.Append("+");

//            sb.AppendLine($"Starting point: {Start.X}, {Start.Y}");
//            sb.AppendLine($"Ending point: {End.X}, {End.Y}");

            return sb.ToString();
        }
    }
}