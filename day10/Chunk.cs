using System;
using System.Collections.Generic;

namespace day10 {
	public class Chunk {
		private static readonly Dictionary<char, char> PAIRS = new Dictionary<char, char> {
			{ '(', ')' },
			{ '[', ']' },
			{ '{', '}' },
			{ '<', '>' }};

		public int StartIndex { get; private set; } = -1;
		public int EndIndex { get; private set; } = -1;

		private (char, char) _delimiter = ('\0', '\0');
		private List<Chunk> _children = new List<Chunk>();

		public Chunk(string input, int startIndex) {
			// Console.WriteLine($"start: {startIndex}");
			StartIndex = startIndex;
			for (int i = startIndex; i < input.Length; i++) {
				char current = input[i];
				if (_delimiter.Item1 == '\0') {
					if (PAIRS.ContainsKey(current)) {
						// Console.WriteLine($"open: {current}");
						_delimiter.Item1 = current;
						_delimiter.Item2 = PAIRS[current];
					} else {
						throw new FormatException($"Invalid start character: {current}");
					}
				} else {
					if (current == _delimiter.Item2) {
						// Console.WriteLine($"end: {i}");
						EndIndex = i;
						break;
					} else if (PAIRS.ContainsKey(current)) {
						var child = new Chunk(input, i);
						_children.Add(child);
						i = child.EndIndex;
						if (i == -1) {
							break;
						}
					} else {
						throw new FormatException($"Expected {_delimiter.Item2}, but found {current} instead.", new FormatException(current.ToString()));
					}
				}
			}
		}

		public string GetCompletionString() {
			string result = string.Empty;
			for (int i = _children.Count - 1; i >= 0; i--) {
				result += _children[i].GetCompletionString();
			}

			if (this.EndIndex == -1) {
				result += _delimiter.Item2;
			}

			return result;
		}

		public override string ToString() {
			return $"{_delimiter.Item1}{string.Join(string.Empty, _children)}{_delimiter.Item2}";
		}
	}
}