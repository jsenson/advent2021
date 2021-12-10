using System;
using System.Collections.Generic;
using Shared;

namespace day08 {
	class Program {
		static void Main(string[] args) {
			Input[] inputs = InputParser.ParseToArray<Input>(InputParser.INPUT, InputParser.NEWLINE, line => new Input(line));
			Part1(inputs);
			Console.WriteLine();
			Part2(inputs);
		}

		static void Part1(Input[] inputs) {
			int counter = 0;
			foreach (Input input in inputs) {
				foreach (string output in input.Outputs) {
					int len = output.Length;
					if (len == 2 || len == 3 || len == 4 || len == 7) {
						counter++;
					}
				}
			}

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Count = {counter}");
		}

		
		static void Part2(Input[] inputs) {
			int total = 0;
			foreach (Input input in inputs) {
				int num = GetNumber(input);
				total += num;
			}

			Console.WriteLine("Part 2:");
			Console.WriteLine($"Count = {total:n0}");
		}

		// This is such a stupid solution but my brain is dead and I want to take a nap.
		static int GetNumber(Input input) {
			var codesByCount = input.GetCodesByCount();
			var numberMap = new Dictionary<int, HashSet<char>>();
			numberMap[1] = new HashSet<char>(codesByCount[2][0]);
			numberMap[4] = new HashSet<char>(codesByCount[4][0]);
			numberMap[7] = new HashSet<char>(codesByCount[3][0]);
			numberMap[8] = new HashSet<char>(codesByCount[7][0]);

			List<HashSet<char>> fives = new List<HashSet<char>>(codesByCount[5].Count);
			for (int i = 0; i < codesByCount[5].Count; i++) {
				fives.Add(new HashSet<char>(codesByCount[5][i]));
			}

			List<HashSet<char>> sixes = new List<HashSet<char>>(codesByCount[6].Count);
			for (int i = 0; i < codesByCount[6].Count; i++) {
				sixes.Add(new HashSet<char>(codesByCount[6][i]));
			}
			
			// Three is the only 5 part number that contains the same parts as 1
			foreach (HashSet<char> five in fives) {
				if (five.IsSupersetOf(numberMap[1])) {
					numberMap[3] = five;
					break;
				}
			}

			foreach (HashSet<char> six in sixes) {
				if (six.IsSupersetOf(numberMap[4])) {
					numberMap[9] = six;
					break;
				}
			}

			fives.Remove(numberMap[3]);
			sixes.Remove(numberMap[9]);

			foreach (HashSet<char> five in fives) {
				if (numberMap[9].IsSupersetOf(five)) {
					numberMap[5] = five;
					break;
				}
			}

			foreach (HashSet<char> six in sixes) {
				if (six.IsSupersetOf(numberMap[5])) {
					numberMap[6] = six;
					break;
				}
			}

			fives.Remove(numberMap[5]);
			sixes.Remove(numberMap[6]);
			numberMap[2] = fives[0];
			numberMap[0] = sixes[0];
			return input.GetOutput(numberMap);
		}

		private struct Input {
			public string[] Outputs;
			public string[] Cyphers;

			public Input(string inputLine) {
				string[] halves = inputLine.Split(" | ");
				Cyphers = halves[0].Split(' ');
				Outputs = halves[1].Split(' ');
			}

			public Dictionary<int, List<string>> GetCodesByCount() {
				var dict = new Dictionary<int, List<string>>();
				foreach (string cypher in Cyphers) {
					if (!dict.ContainsKey(cypher.Length)) {
						dict[cypher.Length] = new List<string>();
					}

					dict[cypher.Length].Add(cypher);
				}

				return dict;
			}

			public int GetOutput(Dictionary<int, HashSet<char>> cypher) {
				int	number = 0;
				for (int i = 0, len = Outputs.Length; i < len; i++) {
					HashSet<char> chars = new HashSet<char>(Outputs[i]);
					foreach (var kvp in cypher) {
						if (kvp.Value.SetEquals(chars)) {
							int digit = len - i - 1;
							number += kvp.Key * (digit == 0 ? 1 : (int)Math.Pow(10, digit));
						}
					}
				}

				return number;
			}
		}
	}
}
