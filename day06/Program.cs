using System;
using System.IO;

namespace day6 {
	class Program {
		private const string INPUT = "input.txt";
		private const int MAX_AGE = 8;
		private const int RESET_AGE = 6;

		static void Main(string[] args) {
			string[] inputStrings = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), INPUT)).Split(',', StringSplitOptions.RemoveEmptyEntries);
			long[] part1Input = new long[MAX_AGE + 1];
			for(int i = 0; i < inputStrings.Length; i++) {
				int age = Convert.ToInt32(inputStrings[i]);
				part1Input[age]++;
			}

			long[] part2Input = new long[MAX_AGE + 1];
			Array.Copy(part1Input, part2Input, part1Input.Length);
			Simulate(part1Input, 80, "Part 1:");
			Console.WriteLine();
			Simulate(part2Input, 256, "Part 2:");
		}

		static void Simulate(long[] fishCounters, int days, string label) {
			for(int i = 0; i < days; i++) {
				// PrintFish(i, fishCounters);
				long readyToSpawn = fishCounters[0];
				for(int j = 1; j <= MAX_AGE; j++) {
					fishCounters[j - 1] = fishCounters[j];
					fishCounters[j] = 0;
				}

				fishCounters[RESET_AGE] += readyToSpawn;
				fishCounters[MAX_AGE] += readyToSpawn;
			}

			// PrintFish(days, fishCounters);
			Console.WriteLine(label);
			Console.WriteLine($"Total fish after {days} days: {CountFish(fishCounters):n0}");
		}

		static long CountFish(long[] counters) {
			long total = 0;
			for(int i = 0; i < counters.Length; i++) {
				total += counters[i];
			}

			return total;
		}

		static void PrintFish(int days, long[] counters) {
			Console.WriteLine($"After {days} days: {string.Join(',', counters)}");
		}
	}
}
