using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace NAI_AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(new Map().ToString());
            Console.ReadKey();
        }
    }

    public class MapSettings
    {
        public Point Starting { get; set; }
        public Point EndingPoint{ get; set; }
        public Map Map { get; set; }

        public MapSettings(Point starting, Point endingPoint, Map map)
        {
            Starting = starting;
            EndingPoint = endingPoint;
            Map = map;
        }
    }


    public class Map
    {
        public int Width => _map.GetLength(0);
        public int Height => _map.GetLength(1);
        public MapNode[,] _map;

        public Map()
        {
            _map = new[,]
            {
                 {new MapNode(0,0, NodeType.Street), new MapNode(1,0, NodeType.Grass) , new MapNode(3,0, NodeType.Street) },
                 {new MapNode(0,1, NodeType.Street), new MapNode(1,1, NodeType.Wall) , new MapNode(3,1, NodeType.Street) },
                 {new MapNode(0,2, NodeType.Street), new MapNode(1,2, NodeType.Wall) , new MapNode(3,2, NodeType.Street) },
                 {new MapNode(0,3, NodeType.Street), new MapNode(1,3, NodeType.Street) , new MapNode(3,3, NodeType.Street) }
            };
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append('+');
            sb.Append('-', Height);
            sb.Append('+');
            sb.AppendLine();

            for (int i = 0; i < Width; i++)
            {
                sb.Append('|');
                for (int j = 0; j < Height; j++)
                {
                    sb.Append(_map[i, j].Type.TypeToChar());
                }
                sb.Append('|');
                sb.AppendLine();
            }
            sb.Append('+');
            sb.Append('-', Height);
            sb.Append('+');
            return sb.ToString();
        }
    }
}
