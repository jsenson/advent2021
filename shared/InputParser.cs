using System;
using System.IO;

namespace Shared {
	public static class InputParser {
		public const string INPUT = "input.txt";
		public const string DEBUG = "debuginput.txt";
		public const string COMMA = ",";
		public const string NEWLINE = "\n";

		public static string[] ParseRaw(string fileName, string separator) {
			return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName)).Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}

		public static int[] ParseToInts(string fileName, string separator) {
			return ParseToArray<int>(fileName, separator, entry => Convert.ToInt32(entry));
		}

		public static T[] ParseToArray<T>(string fileName, string separator, Func<string, T> parseFunc) {
			string[] inputStrings = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), fileName)).Split(separator, StringSplitOptions.RemoveEmptyEntries);
			T[] inputs = new T[inputStrings.Length];
			for (int i = 0; i < inputStrings.Length; i++) {
				inputs[i] = parseFunc(inputStrings[i]);
			}

			return inputs;
		}
	}
}