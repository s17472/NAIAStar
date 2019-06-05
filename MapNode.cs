using System.Drawing;

namespace NAI_AStar
{
    public struct MapNode
    {
        public int X, Y;
        public NodeType Type;

        public MapNode(int x, int y, NodeType type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}