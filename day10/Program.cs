using System;
using System.Collections.Generic;
using Shared;

namespace day10 {
	class Program {
		static readonly Dictionary<string, int> ERROR_SCORES = new Dictionary<string, int> {
			{ ")", 3 },
			{ "]", 57 },
			{ "}", 1197 },
			{ ">", 25137 }
		};

		static readonly Dictionary<char, int> AUTOCOMPLETE_SCORES = new Dictionary<char, int> {
			{ ')', 1 },
			{ ']', 2 },
			{ '}', 3 },
			{ '>', 4 }
		};

		static void Main(string[] args) {
			long errorScore = 0;
			Line[] input = InputParser.ParseToArray<Line>(InputParser.INPUT, InputParser.NEWLINE, line => {
				try {
					return new Line(line);
				} catch (FormatException ex) {
					// Console.WriteLine(ex.Message);
					if (ERROR_SCORES.TryGetValue(ex.InnerException.Message, out int value)) {
						errorScore += value;
					}

					return null;
				}
			});

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Error Score: {errorScore}");
			Console.WriteLine();
			Part2(input);
		}

		static void Part2(Line[] input) {
			List<long> scores = new List<long>();
			foreach (Line line in input) {
				if (line != null) {
					long score = 0;
					string missingChars = line.GetCompletionString();
					foreach (char character in missingChars) {
						score *= 5;
						score += AUTOCOMPLETE_SCORES[character];
					}

					scores.Add(score);
				}
			}

			scores.Sort();
			// foreach(long score in scores) {
			// 	Console.WriteLine(score.ToString());
			// }

			int mid = scores.Count / 2;
			Console.WriteLine("Part 2:");
			Console.WriteLine($"Middle Score: {scores[mid]:n0}");
		}
	}
}
