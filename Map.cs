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
        public MapNode[,] _map;

        public Map(Point start, Point end)
        { 
            _map = new[,]
            {
                {new MapNode(NodeType.Street), new MapNode(NodeType.Grass) , new MapNode(NodeType.Street) },
                {new MapNode(NodeType.Street), new MapNode(NodeType.Wall) , new MapNode(NodeType.Street) },
                {new MapNode(NodeType.Street), new MapNode(NodeType.Wall) , new MapNode(NodeType.Street) },
                {new MapNode(NodeType.Street), new MapNode(NodeType.Street) , new MapNode(NodeType.Street) }
            };

            if (start.X > Width && start.Y > Height)
            {
                throw new Exception($"STARTING point ({start.X}, {start.Y}) does not exists.");
            }
            if (end.X > Width && end.Y > Height)
            {
                throw new Exception($"ENDING point ({end.X}, {end.Y}) does not exists.");
            }
            if (_map[start.X, start.Y].Type <= 0)
            {
                throw new Exception($"STARTING point ({start.X}, {start.Y}) can't be set on impassable node ({_map[start.X, start.Y].Type.ToString()}).");
            }
            if (_map[end.X, end.Y].Type <= 0)
            {
                throw new Exception($"ENDING point ({end.X}, {end.Y}) can't be set on impassable node ({_map[end.X, end.Y].Type.ToString()}).");
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
                    sb.Append(_map[i, j].Type.TypeToChar());
                }

                sb.Append('|');
                sb.AppendLine();
            }

            sb.Append('+');
            sb.Append('-', Width);
            sb.Append('+');

            return sb.ToString();
        }
    }
}