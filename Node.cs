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

        public NodeType Type { get; private set; }
        public int G { get; private set; }
        public int H { get; private set; }
        public int F => G + H;
        public int X { get; }
        public int Y { get; }
        public bool IsOpen { get; set; } = true;
        public bool IsPassable => Type != NodeType.Wall;


        public Node(Node parentNode, NodeType type, int x, int y)
        {
            _parentNode = parentNode;
            Type = type;
            G = (int)type;
            X = x;
            Y = y;
        }

        public Node(NodeType type, int x, int y) : this(null, type, x, y) {}

        public Node(NodeType type) : this(null, type, 0, 0) {}

        public void SetHeuristic(Node endNode)
        {
            var D = 1;
            var dx = Math.Abs(X - endNode.X);
            var dy = Math.Abs(Y - endNode.Y);
            H = D * (dx + dy);
        }


        public static int GetTravelCost(Node startNode, Node endNode)
        {
            var D = 1;
            var dx = Math.Abs(startNode.X - endNode.X);
            var dy = Math.Abs(startNode.Y - endNode.Y);
            return D * (dx + dy);
        }
    }
}