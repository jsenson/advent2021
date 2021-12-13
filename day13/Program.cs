using System;
using System.Collections.Generic;
using Shared;

namespace day13 {
	class Program {
		static void Main(string[] args) {
			List<Vector3Int> folds = new List<Vector3Int>();
			Vector3Int[] positions = InputParser.ParseToArray<Vector3Int>(InputParser.INPUT, InputParser.NEWLINE, line => {
				if(line.StartsWith("fold") || string.IsNullOrWhiteSpace(line)) {
					if(!string.IsNullOrWhiteSpace(line)) {
						folds.Add(ParseFold(line));
					}

					return default(Vector3Int);
				} else {
					return new Vector3Int(line, ",");
				}
			});

			// Trim out the extra (0,0,0) entries for the fold instructions and blank line
			Array.Resize(ref positions, positions.Length - folds.Count);
			var grid1 = GetGrid(positions);
			var grid2 = GetGrid(positions);

			Part1(grid1, folds[0]);
			Console.WriteLine();
			Part2(grid2, folds);
		}

		static Vector3Int ParseFold(string line) {
			int equalPos = line.IndexOf('=');
			if(equalPos >= 0) {
				int value = Convert.ToInt32(line.Substring(equalPos + 1));
				char axis = line[equalPos - 1];

				return axis == 'x' ? new Vector3Int(value, 0, 0) : new Vector3Int(0, value, 0);
			}

			return default(Vector3Int);
		}

		static Grid<bool> GetGrid(Vector3Int[] positions) {
			int width = 0, height = 0;
			foreach(var position in positions) {
				if(position.x > width) {
					width = position.x;
				}

				if(position.y > height) {
					height = position.y;
				}
			}

			var grid = new Grid<bool>(width + 1, height + 1);
			foreach(var position in positions) {
				grid.Get(position.x, position.y).Value = true;
			}

			return grid;
		}

		static void Part1(Grid<bool> grid, Vector3Int fold) {
			Vector3Int foldedBounds = Fold(grid, fold, new Vector3Int(grid.Width, grid.Height, 0));
			Console.WriteLine("Part 1:");
			Console.WriteLine($"Visible dots: {CountDots(grid, foldedBounds):n0}");
		}

		static void Part2(Grid<bool> grid, List<Vector3Int> folds) {
			Vector3Int foldedBounds = new Vector3Int(grid.Width, grid.Height, 0);
			foreach(var fold in folds) {
				foldedBounds = Fold(grid, fold, foldedBounds);
			}

			Console.WriteLine("Part 2:");
			for(int y = 0; y < foldedBounds.y; y++) {
				for(int x = 0; x < foldedBounds.x; x++) {
					Console.Write(grid.Get(x, y).Value ? '#' : '.');
				}

				Console.Write('\n');
			}
		}

		static Vector3Int Fold(Grid<bool> grid, Vector3Int fold, Vector3Int bounds) {
			Vector3Int newBounds = fold.x > 0 ? new Vector3Int(fold.x, bounds.y, bounds.z) : new Vector3Int(bounds.x, fold.y, bounds.z);
			for(int x = 0; x < newBounds.x; x++) {
				for(int y = 0; y < newBounds.y; y++) {
					var oppositeNode = fold.x > 0 ? grid.Get(bounds.x - x - 1, y) : grid.Get(x, bounds.y - y - 1);
					if(oppositeNode.Value) {
						grid.Get(x, y).Value = true;
					}
				}
			}

			return newBounds;
		}

		static long CountDots(Grid<bool> grid, Vector3Int bounds) {
			long total = 0;
			for(int x = 0; x < bounds.x; x++) {
				for(int y = 0; y < bounds.y; y++) {
					if(grid.Get(x, y).Value) {
						total++;
					}
				}
			}

			return total;
		}
	}
}
