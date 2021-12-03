using System;
using System.Collections.Generic;
using System.IO;

namespace day3 {
	class Program {
		private const string INPUT = "input.txt";

		static void Main(string[] args) {
			string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), INPUT));
			int[] input = new int[lines.Length];
			for (int i = 0; i < lines.Length; i++) {
				input[i] = Convert.ToInt32(lines[i], 2);
			}

			Part1(input, 12);
			Console.WriteLine();
			Part2(input, 12);
		}

		static void Part1(int[] inputs, int bits) {
			var counts = new Dictionary<int, int>();
			for (int i = 0; i < bits; i++) {
				counts[i] = 0;
			}

			foreach (int input in inputs) {
				for (int i = 0; i < bits; i++) {
					if ((input & (1 << i)) > 0) {
						counts[i]++;
					}
				}
			}

			int gamma = 0;
			int halfLen = (int)Math.Floor(inputs.Length * 0.5f);
			for (int i = 0; i < bits; i++) {
				gamma |= counts[i] >= halfLen ? 1 << i : 0;
			}

			int epsilon = ~gamma & ((int)Math.Pow(2, bits) - 1);
			Console.WriteLine("Part 1:");
			Console.WriteLine($"gamma = {ToBinary(gamma, bits)}, epsilon = {ToBinary(epsilon, bits)}");
			Console.WriteLine($"Result = {gamma * epsilon}");
		}

		static void Part2(int[] inputs, int bits) {
			int oxygen = GetRating(inputs, bits, bits - 1, true);
			int co2 = GetRating(inputs, bits, bits - 1, false);

			Console.WriteLine("Part 2:");
			Console.WriteLine($"oxygen = {oxygen}, CO2 = {co2}");
			Console.WriteLine($"Result = {oxygen * co2}");
		}

		static int GetRating(int[] inputs, int bits, int currentBit, bool takeHighest) {
			if (inputs.Length == 0) {
				Console.WriteLine("Umm... something dun fucked.");
				return 0;
			} else if (inputs.Length == 1) {
				return inputs[0];
			}

			List<int> ones = new List<int>(inputs.Length);
			List<int> zeroes = new List<int>(inputs.Length);
			foreach (int input in inputs) {
				if ((input & (1 << currentBit)) > 0) {
					ones.Add(input);
				} else {
					zeroes.Add(input);
				}
			}

			//  Got tripped up in the decription.  It refers to the "first" bit be he meant the leftmost.  He's reading left to right...
			int nextBit = currentBit == 0 ? bits - 1 : currentBit - 1;
			int[] nextInputs;
			if (takeHighest) {
				nextInputs = zeroes.Count > ones.Count ? zeroes.ToArray() : ones.ToArray();
			} else {
				nextInputs = zeroes.Count > ones.Count ? ones.ToArray() : zeroes.ToArray();
			}

			return GetRating(nextInputs, bits, nextBit, takeHighest);
		}

		static string ToBinary(int val, int bits) {
			return Convert.ToString(val, 2).PadLeft(bits, '0');
		}
	}
}
