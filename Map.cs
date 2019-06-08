using System;
using System.Drawing;
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
            {
                throw new Exception($"Starting point ({start.X}, {start.Y}) was outside the bounds of the map.");
            }
            if (!InBounds(end))
            {
                throw new Exception($"Ending point ({end.X}, {end.Y}) was outside the bounds of the map.");
            }
            if (!IsPassable(start))
            {
                throw new Exception($"Starting point ({start.X}, {start.Y}) can't be set on impassable node ({_map[start.X, start.Y].Type.ToString()}).");
            }
            if (!IsPassable(end))
            {
                throw new Exception($"Ending point ({end.X}, {end.Y}) can't be impassable node ({_map[end.Y, end.X].Type.ToString()}).");
            }

            Start = start;
            End = end;
        }


        public Map() : this(new Point(), new Point())
        {

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

        private bool InBounds(Point point)
        {
            return point.X < Width && point.X >= 0 && point.Y < Height && point.Y >= 0;
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
    }
}