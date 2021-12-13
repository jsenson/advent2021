using System.Collections.Generic;
using System.Text;

namespace day12 {
	public struct Node {
		public const string START = "start";
		public const string END = "end";

		public enum Sizes {
			Small,
			Big
		}

		public string Name;
		public int Visits;
		public List<string> Neighbours;

		public Sizes Size { get; private set; }

		public Node(string name) {
			Name = name;
			Neighbours = new List<string>();
			Size = name == name.ToLower() ? Sizes.Small : Sizes.Big;
			Visits = 0;
		}

		public override string ToString() {
			var sb = new StringBuilder($"{Name}, {Size}, {Visits}, {{");
			foreach (var neighbour in Neighbours) {
				sb.Append($"{neighbour}, ");
			}
			sb.Remove(sb.Length - 2, 2);
			sb.Append("}");

			return sb.ToString();
		}
	}
}