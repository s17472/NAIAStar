using System;
using System.Drawing;

namespace NAI_AStar
{
    public enum NodeType
    {
        Street = 10,
        Grass = 15,
        Wall = 0
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
        public Point Location { get; set; }

        public Node(Node? parentNode, NodeType type, double g, double h, Point location)
        {
            _parentNode = parentNode;
            Type = type;
            G = g;
            H = h;
            Location = location;
        }

        public Node(NodeType type) : this(null, type, 0,0,new Point())
        {
            Type = type;
        }

        public static double GetTravelCost(Node startNode, Node endNode)
        {
            var deltaX = endNode.Location.X - startNode.Location.X;
            var deltaY = endNode.Location.Y - startNode.Location.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY) * ((double)startNode.Type / 10);
        }
    }
}