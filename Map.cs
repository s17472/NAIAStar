using System.Text;

namespace NAI_AStar
{
    public class Map
    {
        public int Width => _map.GetLength(1);
        public int Height => _map.GetLength(0);
        public MapNode[,] _map;

        public Map()
        {
            _map = new[,]
            {
                {new MapNode(0,0, NodeType.Street), new MapNode(1,0, NodeType.Grass) , new MapNode(3,0, NodeType.Street) },
                {new MapNode(0,1, NodeType.Street), new MapNode(1,1, NodeType.Wall) , new MapNode(3,1, NodeType.Street) },
                {new MapNode(0,2, NodeType.Street), new MapNode(1,2, NodeType.Wall) , new MapNode(3,2, NodeType.Street) },
                {new MapNode(0,3, NodeType.Street), new MapNode(1,3, NodeType.Street) , new MapNode(3,3, NodeType.Street) }
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('+');
            sb.Append('-', Width);
            sb.Append('+');
            sb.AppendLine();

            for (int i = 0; i < Height; i++)
            {
                sb.Append('|');
                for (int j = 0; j < Width; j++)
                {
                    sb.Append(_map[i, j].Type.TypeToChar());
                }

                sb.Append('|');
                sb.AppendLine();
            }

            sb.Append('+');
            sb.Append('-', Width);
            sb.Append('+');

            return sb.ToString();
        }
    }
}