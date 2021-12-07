using System;
using System.Collections.Generic;
using Shared;

namespace day4 {
	class Program {
		static void Main(string[] args) {
			string[] lines = InputParser.ParseRaw(InputParser.INPUT, InputParser.NEWLINE);
			int[] inputs = GetInputs(lines[0]);

			int boardStart = 2;
			var boards = new List<BingoBoard>();
			for(int i = boardStart; i < lines.Length; i++) {
				if(string.IsNullOrWhiteSpace(lines[i])) {
					boards.Add(CreateBoard(lines, boardStart, i));
					boardStart = i + 1;
				}
			}

			// Catch the last board since input doesn't end in a newline
			boards.Add(CreateBoard(lines, boardStart, lines.Length));

			Part1(inputs, boards);
			Console.WriteLine();
			Part2(inputs, boards);
		}

		static int[] GetInputs(string line) {
			string[] strings = line.Split(',');
			int[] numbers = new int[strings.Length];
			for(int i = 0; i < strings.Length; i++) {
				numbers[i] = Convert.ToInt32(strings[i]);
			}

			return numbers;
		}

		static BingoBoard CreateBoard(string[] inputs, int startIndex, int endIndex) {
			string[] boardLines = new string[endIndex - startIndex];
			for(int j = startIndex, k = 0; j < endIndex; j++, k++) {
				boardLines[k] = inputs[j];
			}

			return new BingoBoard(boardLines);
		}

		static void Part1(int[] inputs, List<BingoBoard> boards) {
			int result = -1;
			for(int i = 0; i < inputs.Length; i++) {
				if(result < 0) {
					int input = inputs[i];
					for(int j = 0, count = boards.Count; j < count; j++) {
						BingoBoard board = boards[j];
						if(board.MarkNumber(input)) {
							// Console.WriteLine($"Board #{j} marked {input} true");
							if(board.HasWon()) {
								result = board.GetScore(input);
								// Console.WriteLine($"Board #{j} won");
								break;
							}
						}
					}
				}
			}

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Score = {result}");
		}

		static void Part2(int[] inputs, List<BingoBoard> boards) {
			int result = -1;
			for(int i = 0; i < inputs.Length; i++) {
				int input = inputs[i];
				for(int j = 0, count = boards.Count; j < count; j++) {
					BingoBoard board = boards[j];
					if(board.MarkNumber(input)) {
						if(board.HasWon()) {
							result = board.GetScore(input);
							boards.RemoveAt(j--);  // Shouldn't really modify the source list but meh... we know we're not using it after this.
							count--;
						}
					}
				}
			}

			Console.WriteLine("Part 2:");
			Console.WriteLine($"Score = {result}");
		}
	}
}
