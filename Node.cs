using System;
using System.Drawing;

namespace NAI_AStar
{
    public enum NodeType
    {
        Street = 0,
        Grass = 5,
        Wall = int.MaxValue
    }

    public static class NodeTypeExtensions
    {
        public static char TypeToChar(this NodeType t)
        {
            switch (t)
            {
                case NodeType.Street:
                    return ' ';
                case NodeType.Wall:
                    return '░';
                case NodeType.Grass:
                    return ',';
                default:
                    return ' ';
            }
        }
    }

    public class Node
    {
        private Node? _parent;

        public Node Parent
        {
            get => _parent;
            set
            {
                if (!IsPassable)
                    throw new Exception("Node not passable, cannot be child.");

                _parent = value;
                G = (int)Type;

                if (_parent != null)
                    G += GetTravelCost(this, _parent) + _parent.G;
            }
        }

        public NodeType Type { get; }
        public int G { get; private set; }
        public int H { get; }
        public int F => G + H;
        public int X { get; }
        public int Y { get; }
        public bool IsOpen { get; set; } = true;
        public bool IsPassable => (int)Type != (int)NodeType.Wall;
        
        public Node(NodeType type, int x, int y)
        {
            Type = type;
            G = (int)type;
            X = x;
            Y = y;
        }

        public Node(NodeType type) : this(type, 0, 0) {}

        public int SetHeuristic(Node endNode)
        {
            var D = 1;
            var dx = Math.Abs(X - endNode.X);
            var dy = Math.Abs(Y - endNode.Y);
            return D * (dx + dy);
        }


        public static int GetTravelCost(Node startNode, Node endNode)
        {
            var D = 1;
            var dx = Math.Abs(startNode.X - endNode.X);
            var dy = Math.Abs(startNode.Y - endNode.Y);
            return D * (dx + dy);
        }

        public override string ToString()
        {
            return $"{X}, {Y}, G: {G}, H: {H}, {Type}, open: {IsOpen}, passable: {IsPassable}";
        }
    }
}