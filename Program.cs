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
            var map = new Map(new Point(0,0), new Point(3,3) );
            Console.WriteLine(map.ToString());
            Console.ReadKey();
        }
    }

    public class PathFinder
    {

    }
}
