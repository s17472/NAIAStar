using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NAIAStar
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new string[]
            {
                "    ",
                "  # ",
                "  # ",
                "  # ",
                "  # ",
                "    ",
            };
            foreach (var s in m)
            {
                Console.WriteLine(s);
            }
            var map = new Map(new Point(1,1), new Point(3,2), m);
            var pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());


            map = new Map(new Point(0, 0), new Point(3, 2), m);
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            map = new Map(new Point(0, 0), new Point(3, 3), m);
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            map = new Map(new Point(0, 0), new Point(3, 4), m);
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            Console.ReadKey();
        }


    }
}
