using System;
using Shared;

namespace day2 {
	class Program {
		static void Main(string[] args) {
			string[] lines = InputParser.ParseRaw(InputParser.INPUT, InputParser.NEWLINE);
			Part1(lines);
			Console.WriteLine();
			Part2(lines);
		}

		private static void Part1(string[] lines) {
			Vector3Int total = new Vector3Int(0, 0, 0);
			for (int i = 0; i < lines.Length; i++) {
				total = total.Add(GetPart1Vector(lines[i]));
			}

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Final Position: {total}");
			Console.WriteLine($"x * y = {total.x * total.y}");
		}

		private static void Part2(string[] lines) {
			Vector3Int total = new Vector3Int(0, 0, 0);
			for (int i = 0; i < lines.Length; i++) {
				total = total.Add(GetPart2Vector(lines[i], total.z));
			}

			Console.WriteLine("Part 2:");
			Console.WriteLine($"Final Position: {total}");
			Console.WriteLine($"x * y = {total.x * total.y}");
		}

		private static Vector3Int GetPart1Vector(string input) {
			string[] inputs = input.TrimEnd().Split(' ');
			int value = Convert.ToInt32(inputs[1]);
			if (inputs[0].Equals("forward")) {
				return new Vector3Int(value, 0, 0);
			} else if (inputs[0].Equals("down")) {
				return new Vector3Int(0, value, 0);
			} else {
				return new Vector3Int(0, -value, 0);
			}
		}

		private static Vector3Int GetPart2Vector(string input, int currentZ) {
			string[] inputs = input.TrimEnd().Split(' ');
			int value = Convert.ToInt32(inputs[1]);
			if (inputs[0].Equals("forward")) {
				return new Vector3Int(value, value * currentZ, 0);
			} else if (inputs[0].Equals("down")) {
				return new Vector3Int(0, 0, value);
			} else {
				return new Vector3Int(0, 0, -value);
			}
		}
	}
}
