using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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
        public Point EndingPoint { get; set; }
        public Map Map { get; set; }

        public MapSettings(Point starting, Point endingPoint, Map map)
        {
            Starting = starting;
            EndingPoint = endingPoint;
            Map = map;
        }
    }
}
