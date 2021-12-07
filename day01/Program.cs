using System;
using Shared;

namespace day1 {
	class Program {
		static void Main(string[] args) {
			int[] numbers = InputParser.ParseToInts(InputParser.INPUT, InputParser.NEWLINE);
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
