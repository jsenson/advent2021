namespace Shared {
	public class Grid<T> {
		public int Width { get; private set; }
		public int Height { get; private set; }

		private Node[] _nodes;

		public Grid(int width, int height) {
			_nodes = new Node[width * height];
			Height = height;
			Width = width;
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					var node = new Node(x, y);
					_nodes[GetIndex(x, y)] = node;

					if (x > 0) {
						Node left = _nodes[GetIndex(x - 1, y)];
						node.Neighbours[(int)Node.Direction.West] = left;
						left.Neighbours[(int)Node.Direction.East] = node;
					}

					if (y > 0) {
						Node down = _nodes[GetIndex(x, y - 1)];
						node.Neighbours[(int)Node.Direction.South] = down;
						down.Neighbours[(int)Node.Direction.North] = node;
					}
				}
			}
		}

		public Node Get(int x, int y) {
			return _nodes[GetIndex(x, y)];
		}

		private int GetIndex(int x, int y) {
			return y * Width + x;
		}

		public class Node {
			public enum Direction {
				North,
				East,
				South,
				West,
				_Count
			}

			public Vector3Int Position;
			public T Value;
			public Node[] Neighbours;

			public Node(int x, int y) {
				Position = new Vector3Int(x, y, 0);
				Value = default(T);
				Neighbours = new Node[(int)Direction._Count];
			}

			public Node GetNeighbour(Direction direction) {
				return Neighbours[(int)direction];
			}

			public override string ToString() {
				return $"({Position.x}, {Position.y}): {Value}";
			}
		}
	}
}