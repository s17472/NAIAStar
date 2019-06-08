using System;
using System.Drawing;

namespace NAI_AStar
{
    public enum NodeType
    {
        Street = 0,
        Grass = 5,
        Wall = int.MaxValue, 
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
        private Node? _parentNode;

        public Node Parent
        {
            get => _parentNode;
            set
            {
                _parentNode = value;
                G = _parentNode.G + GetTravelCost(this, _parentNode);
            }
        }

        public NodeType Type { get; set; }
        public double G { get; private set; }
        public double H { get; set; }
        public double F => G + H;
        public int X { get; set; }
        public int Y { get; set; }

        public Node(Node? parentNode, NodeType type, double g, double h, int x, int y)
        {
            _parentNode = parentNode;
            Type = type;
            G = g;
            H = h;
            X = x;
            Y = y;
        }

        public Node(NodeType type) : this(null, type, 0, 0, 0, 0)
        {
            Type = type;
        }

        public static double GetTravelCost(Node startNode, Node endNode)
        {
            var D = 1;
            var dx = Math.Abs(startNode.X - endNode.X);
            var dy = Math.Abs(startNode.Y - endNode.Y);
            return D * (dx + dy);
        }
    }
}