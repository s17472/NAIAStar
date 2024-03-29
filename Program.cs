﻿using System;
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
            //Legend:
            //# - wall
            //; - grass
            //S - starting point
            //E - ending point
            var m = new string[]
            {
                //Source: https://www.asciiart.eu/art-and-design/mazes
                "##################################################################### S #",
                "#   #               #               #           #                   #   #",
                "#   #   #########   #   #####   #########   #####   #####   #####   #   #",
                "#               #       #   #           #           #   #   #       #   #",
                "#########   #   #########   #########   #####   #   #   #   #########   #",
                "#       #   #               #           #   #   #   #   #           #   #",
                "#   #   #############   #   #   #########   #####   #   #########   #   #",
                "#   #               #   #   #       #           #           #       #   #",
                "#   #############   #####   #####       #####   ##### ###   #   #####   #",
                "#           #       #   #           #       #           #   #           #",
                "#   #####   #####       #####   #   #########   #   #   #   #############",
                "#       #       #   #   #       #       #       #   #   #       #       #",
                "##### #######   #   #   #   #########   #   #####   #   #####   #####   #",
                "#           #   #           #       #   #       #   #       #           #",
                "#   #####   #   ##### ###   #####   #   #####   #####   #############   #",
                "#   #       #           #           #       #   #   #               #   #",
                "#   #   #########   #   #####   #########   #   #   #############   #   #",
                "#   #           #   #   #   #   #           #               #   #       #",
                "#   #### ####       #   #   #####   #########   #########   #   #########",
                "#   #       #   #   #           #           #   #       #               #",
                "#   #   #####   #####   #####   #########   #####   #   #########   #   #",
                "#   #                   #                           #               #   #",
                "# E #####################################################################",
            };

            var map = new Map(m);
            var pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            m = new[]
            {
                "##################################################################### S #",
                "#   #               #               #           #                   #   #",
                "#   #   #########   #   #####   #########   #####   #####   #####   #   #",
                "#               #       #   #           #           #   #   #       #   #",
                "#########   #   #########   #########   #####   #  ;#   #   #########   #",
                "#       #   #               #           #   #   #  ;#   #           #   #",
                "#   #   #############   #   #   #########   #####  ;#   #########   #   #",
                "#   #                   #   #       #           #           #       #   #",
                "#   #############   #####   #####       #####   ##### ###   #   #####   #",
                "#           #       #   #           #       #   ;;      #   #           #",
                "#   #####   #####       #####   #   #########   #  ;#   #   #############",
                "#       #       #   #   #       #       #       #   #   #       #       #",
                "##### #######   #   #   #   #########   #   #####   #   #####   #####   #",
                "#           #   #     ;;;   #       #   #       #   #       #           #",
                "#   #####   #   ##### ###   #####   #   #####   #####   #############   #",
                "#   #       #           #           #       #   #   #               #   #",
                "#   #   #########   #   #####   #########   #   #   #############   #   #",
                "#   #           #   #   #   #   #           #               #   #       #",
                "#   ####;####       #   #   #####   #########   #########   #   #########",
                "#   #  ;    #   #   #           #           #   #       #               #",
                "#   #  ;#####   #####   #####   #########   #####   #   #########   #   #",
                "#   #;;;                #                           #               #   #",
                "# E #####################################################################",
            };
            map = new Map(m);
            pf = new PathFinder(map);
            pf.Find();
            Console.WriteLine(map.ToString());

            Console.ReadKey();
        }
    }
}
