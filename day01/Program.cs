using System;
using System.IO;

namespace day1 {
	class Program {
		private const string INPUT = "input.txt";

		static void Main(string[] args) {
			string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), INPUT));
			int[] numbers = new int[lines.Length];
			for (int i = 0; i < lines.Length; i++) {
				numbers[i] = Convert.ToInt32(lines[i]);
			}

			Part1(numbers);
			Part2(numbers);
		}

		static void Part1(int[] numbers) {
			int lastNum = int.MinValue;
			int counter = 0;
			foreach (int number in numbers) {
				if (lastNum > int.MinValue && number > lastNum) {
					counter++;
				}

				lastNum = number;
			}

			Console.WriteLine($"{counter}");
		}

		static void Part2(int[] numbers) {
			int windowLength = 3;
			int[] windows = new int[numbers.Length - (windowLength - 1)];
			int windowIndex = 0;
			for (int i = 0, j = 0; i < numbers.Length; i++, j++) {
				if (j == windowLength) {
					windowIndex++;
					i -= (windowLength - 1);
					j = 0;
				}

				windows[windowIndex] += numbers[i];
			}

			Part1(windows);
		}
	}
}
