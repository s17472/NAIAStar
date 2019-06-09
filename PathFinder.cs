using System.Collections.Generic;
using System.Linq;

namespace NAI_AStar
{
    public class PathFinder
    {
        public Map Map { get; set; }

        public IEnumerable<Node> _nodes;

        public PathFinder(Map map)
        {
            Map = map;
            _nodes = map.GetMapAsEnumerable();
        }

        public void Find()
        {
            var openList = new List<Node>();
            var closedList = new List<Node>();
            Node current = null;

            openList.Add(Map.GetStartNode());

            while (openList.Count > 0)
            {
                current = openList.OrderBy(x => x.F).First();

                closedList.Add(current);

                openList.Remove(current);

                if (closedList.Any(x => x == Map.GetEndNode()))
                    break;

                var openNeighbors = Map.GetPassableOpenNeighbors(current);

                foreach (var nextNode in openNeighbors)
                {
                    if (closedList.FirstOrDefault(x => x == nextNode) != null)
                        continue;

                    if (openList.FirstOrDefault(x => x == nextNode) == null)
                    {
                        nextNode.Parent = current;
                        openList.Insert(0, nextNode);
                    }
                    else
                    {
                        if (current.G + nextNode.H < nextNode.F)
                        {
                            nextNode.Parent = current;
                        }
                    }
                }
            }

            while (current != null)
            {
                current.IsPath = true;
                current = current.Parent;
            }
        }
    }
}