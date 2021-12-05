using System;

namespace Shared {
	public struct Vector3Int {
		public int x;
		public int y;
		public int z;

		public Vector3Int(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3Int(string valueString, string separator) {
			string[] values = valueString.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
			this.x = values.Length > 0 ? Convert.ToInt32(values[0]) : 0;
			this.y = values.Length > 1 ? Convert.ToInt32(values[1]) : 0;
			this.z = values.Length > 2 ? Convert.ToInt32(values[2]) : 0;
		}

		public Vector3Int Add(Vector3Int other) {
			return new Vector3Int(this.x + other.x, this.y + other.y, this.z + other.z);
		}

		public static bool operator ==(Vector3Int a, Vector3Int b) {
			return a.x == b.x && a.y == b.y && a.z == b.z;
		}

		public static bool operator !=(Vector3Int a, Vector3Int b) {
			return !(a == b);
		}

		public override string ToString() {
			return $"({x}, {y}, {z})";
		}

		public override bool Equals(object obj) {
			if(obj is Vector3Int other) {
				return this == other;
			} else {
				return object.ReferenceEquals(obj, this);
			}
		}

		public override int GetHashCode() {
			return x.GetHashCode() & y.GetHashCode() | z.GetHashCode();
		}
	}
}
