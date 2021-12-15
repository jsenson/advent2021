using System;

namespace Shared {
	public struct Vector2Int {
		public int x;
		public int y;

		public double Magnitude => Math.Sqrt(x * x + y * y);

		public int SqrMagnitude => x * x + y * y;

		public Vector2Int(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public Vector2Int(string valueString, string separator) {
			string[] values = valueString.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
			this.x = values.Length > 0 ? Convert.ToInt32(values[0]) : 0;
			this.y = values.Length > 1 ? Convert.ToInt32(values[1]) : 0;
		}

		public Vector2Int Add(Vector2Int other) {
			return new Vector2Int(this.x + other.x, this.y + other.y);
		}

		public Vector2Int Subtract(Vector2Int other) {
			return new Vector2Int(this.x - other.x, this.y - other.y);
		}

		public static bool operator ==(Vector2Int a, Vector2Int b) {
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=(Vector2Int a, Vector2Int b) {
			return !(a == b);
		}

		public override string ToString() {
			return $"({x}, {y})";
		}

		public override bool Equals(object obj) {
			if(obj is Vector2Int other) {
				return this == other;
			} else {
				return object.ReferenceEquals(obj, this);
			}
		}

		public override int GetHashCode() {
			return x.GetHashCode() & y.GetHashCode();
		}
	}
}
