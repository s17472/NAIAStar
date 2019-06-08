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
            var map = new Map();
            Console.WriteLine(map.ToString());
            Console.ReadKey();
        }
    }

    public class PathFinder
    {

    }
}
