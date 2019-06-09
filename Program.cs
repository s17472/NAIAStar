using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NAI_AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = new Map(new Point(1,1), new Point(3,2));
            var pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());


            map = new Map(new Point(0, 0), new Point(3, 2));
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            map = new Map(new Point(0, 0), new Point(3, 3));
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            map = new Map(new Point(0, 0), new Point(3, 4));
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            Console.ReadKey();
        }
    }
}
