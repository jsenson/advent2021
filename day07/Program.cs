using System;
using Shared;

namespace day07 {
	class Program {
		static void Main(string[] args) {
			int[] inputs = InputParser.ParseToInts(InputParser.INPUT, InputParser.COMMA);
			Part1(inputs);
			Console.WriteLine();
			Part2(inputs);
		}

		static void Part1(int[] inputs) {
			int result;
			int[] copy = (int[])inputs.Clone();
			Array.Sort(copy);
			result = copy[copy.Length / 2];

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Align to {result}: Costs {AlignTo(copy, result):n0} fuel.");
		}

		// Kinda cheated on this one and saw an answer on Reddit.  Answer is always aligned to mean +/- 0.5
		static void Part2(int[] inputs) {
			int total = 0;
			int result = 0;
			for(int i = 0; i < inputs.Length; i++) {
				total += inputs[i];
			}

			float mean = total / (float)inputs.Length;
			int mean1 = (int)(mean - 0.5f);
			int mean2 = (int)(mean + 0.5f);
			int cost1 = AlignTo(inputs, (int)(mean - 0.5f), 1);
			int cost2 = AlignTo(inputs, (int)(mean + 0.5f), 1);
			int finalCost;
			if(cost1 < cost2) {
				result = mean1;
				finalCost = cost1;
			} else {
				result = mean2;
				finalCost = cost2;
			}

			Console.WriteLine("Part 2:");
			Console.WriteLine($"Align to {result}: Costs {finalCost:n0} fuel.");
		}

		static int AlignTo(int[] inputs, int pos, int costIncrease = 0) {
			int total = 0;
			for(int i = 0; i < inputs.Length; i++) {
				int cost = Math.Abs(pos - inputs[i]);
				if(costIncrease != 0) {
					for(int j = 1, distance = cost; j <= distance; j++) {
						cost += (j - 1) * costIncrease;
					}
				}

				total += cost;
			}

			return total;
		}
	}
}
