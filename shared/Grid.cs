using System.Text;

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
						Node west = _nodes[GetIndex(x - 1, y)];
						node.Neighbours[(int)Node.Direction.West] = west;
						west.Neighbours[(int)Node.Direction.East] = node;
						if (y > 0) {
							Node sw = _nodes[GetIndex(x - 1, y - 1)];
							node.Neighbours[(int)Node.Direction.SouthWest] = sw;
							sw.Neighbours[(int)Node.Direction.NorthEast] = node;
						}

						if (y < Height - 1) {
							Node nw = _nodes[GetIndex(x - 1, y + 1)];
							node.Neighbours[(int)Node.Direction.NorthWest] = nw;
							nw.Neighbours[(int)Node.Direction.SouthEast] = node;
						}
					}

					if (y > 0) {
						Node south = _nodes[GetIndex(x, y - 1)];
						node.Neighbours[(int)Node.Direction.South] = south;
						south.Neighbours[(int)Node.Direction.North] = node;
					}
				}
			}
		}

		public Node Get(int x, int y) {
			return _nodes[GetIndex(x, y)];
		}

		public Grid<T> Clone() {
			var clone = new Grid<T>(Width, Height);
			for (int i = 0; i < _nodes.Length; i++) {
				clone._nodes[i].Value = _nodes[i].Value;
			}

			return clone;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			StringBuilder lineBuilder = new StringBuilder();
			for (int y = 0; y < Height; y++) {
				lineBuilder.Clear();
				for (int x = 0; x < Width; x++) {
					lineBuilder.Append(Get(x, y).Value.ToString());
				}

				sb.AppendLine(lineBuilder.ToString());
			}

			return sb.ToString();
		}

		private int GetIndex(int x, int y) {
			return y * Width + x;
		}

		public class Node {
			public enum Direction {
				North,
				NorthEast,
				East,
				SouthEast,
				South,
				SouthWest,
				West,
				NorthWest,
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