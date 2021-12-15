using System;
using System.Collections.Generic;
using System.IO;
using Shared;

namespace day14 {
	class Program {
		static void Main(string[] args) {
			string input = null;
			int lineNum = 0;
			var rawRules = InputParser.ParseToArray<InsertionRule>(InputParser.INPUT, InputParser.NEWLINE, line => {
				lineNum++;
				if(lineNum == 1) {
					input = line.TrimEnd();
					return default(InsertionRule);
				}

				return new InsertionRule(line);
			});

			var rules = new InsertionRule[rawRules.Length - 1];
			Array.Copy(rawRules, 1, rules, 0, rules.Length);	// Remove the default(InsertionRule) from parser limitations.
			Part1(input, rules, 15);
			Console.WriteLine();
			Part2(input, rules, 40);
		}

		static void Part1(string input, InsertionRule[] rules, int iterations) {
			// Console.WriteLine($"Before: {input}");
			for(int i = 0; i < iterations; i++) {
				input = ApplyInsertion(input, rules);
				// Console.WriteLine($"After: {input}");
			}

			var counts = GetCharacterCounts(input);
			var minMax = GetMinMax(counts);

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Result: {minMax.Item2} - {minMax.Item1} = {minMax.Item2 - minMax.Item1}");
		}

		static void Part2(string input, InsertionRule[] rules, int iterations) {
			var counts = new Dictionary<string, long>();
			var letterCounts = new Dictionary<char, long>();
			foreach (var rule in rules) {
				counts[rule.PairString] = 0;
			}

			letterCounts.IncrementOrCreate(input[input.Length - 1], 1);
			for (int i = 0; i < input.Length - 1; i++) {
				string pair = input.Substring(i, 2);
				counts.IncrementOrCreate(pair, 1);
				letterCounts.IncrementOrCreate(input[i], 1);
			}

			for (int i = 0; i < iterations; i++) {
				var newPairs = new Dictionary<string, long>();
				foreach (var rule in rules) {
					long pairCount = counts[rule.PairString];
					if (pairCount > 0) {
						string newPair1 = new string(new char[] { rule.Pair.Item1, rule.Insertion });
						string newPair2 = new string(new char[] { rule.Insertion, rule.Pair.Item2 });
						newPairs.IncrementOrCreate(newPair1, pairCount);
						newPairs.IncrementOrCreate(newPair2, pairCount);
						// Console.WriteLine($"{pairCount}: -{rule.PairString} +{newPair1} +{newPair2}");
						counts[rule.PairString] -= pairCount;
						letterCounts.IncrementOrCreate(rule.Insertion, pairCount);
					}
				}

				foreach (var kvp in newPairs) {
					counts.IncrementOrCreate(kvp.Key, kvp.Value);
				}
			}

			var minMax = GetMinMax(letterCounts);
			Console.WriteLine("Part 2:");
			Console.WriteLine($"Result: {minMax.Item2:n0} - {minMax.Item1:n0} = {minMax.Item2 - minMax.Item1:n0}");
		}

		static string ApplyInsertion(string input, InsertionRule[] rules) {
			var reader = new StringReader(input);
			var writer = new StringWriter();
			char[] buffer = new char[] { '\0', '\0' };
			while(reader.Peek() > -1) {
				buffer[0] = buffer[1];
				buffer[1] = (char)reader.Read();
				foreach(var rule in rules) {
					if(rule.Matches(buffer)) {
						writer.Write(rule.Insertion);
					}
				}

				writer.Write(buffer[1]);
			}

			return writer.ToString();
		}

		static Dictionary<char, long> GetCharacterCounts(string input) {
			var counts = new Dictionary<char, long>();
			foreach (char ch in input) {
				counts.IncrementOrCreate(ch, 1);
			}

			return counts;
		}

		static (long, long) GetMinMax(Dictionary<char, long> counts) {
			long min = long.MaxValue;
			long max = long.MinValue;
			foreach (var kvp in counts) {
				if (kvp.Value < min) {
					min = kvp.Value;
				}

				if (kvp.Value > max) {
					max = kvp.Value;
				}
			}

			return (min, max);
		}

		private struct InsertionRule {
			public (char, char) Pair;
			public string PairString;
			public char Insertion;

			public InsertionRule(string input) {
				string[] inputs = input.Split(" -> ");
				PairString = inputs[0];
				Pair = (inputs[0][0], inputs[0][1]);
				Insertion = inputs[1][0];
			}

			public bool Matches(char[] chars) {
				return chars.Length == 2 && chars[0] == Pair.Item1 && chars[1] == Pair.Item2;
			}

			public override string ToString() {
				return $"{Pair.Item1}{Pair.Item2} -> {Insertion}";
			}
		}
	}
}
