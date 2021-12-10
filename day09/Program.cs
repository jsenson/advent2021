using System;
using System.Collections.Generic;
using Shared;

namespace day09 {
	class Program {
		static void Main(string[] args) {
			string[] lines = InputParser.ParseRaw(InputParser.INPUT, InputParser.NEWLINE);
			int width = lines[0].TrimEnd().Length;
			Grid<int> grid = new Grid<int>(width, lines.Length);
			for (int y = 0; y < lines.Length; y++) {
				for (int x = 0; x < lines[y].Length; x++) {
					grid.Get(x, y).Value = Convert.ToInt32(lines[y][x].ToString());
					// Console.WriteLine($"({x}, {y}) = {grid.Get(x, y).Value}");
				}
			}

			Part1(grid);
			Console.WriteLine();
			Part2(grid);
		}

		static void Part1(Grid<int> grid) {
			int total = 0;
			for (int x = 0; x < grid.Width; x++) {
				for (int y = 0; y < grid.Height; y++) {
					var node = grid.Get(x, y);
					if (IsLowPoint(node)) {
						// Console.WriteLine($"{node}: {1 + node.Value}");
						total += 1 + node.Value;
					}
				}
			}

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Risk Level = {total}");
		}

		static bool IsLowPoint(Grid<int>.Node node) {
			int val = node.Value;
			return NeighbourGreater(node, Grid<int>.Node.Direction.West) &&
						NeighbourGreater(node, Grid<int>.Node.Direction.South) &&
						NeighbourGreater(node, Grid<int>.Node.Direction.North) &&
						NeighbourGreater(node, Grid<int>.Node.Direction.East);
		}

		static bool NeighbourGreater(Grid<int>.Node node, Grid<int>.Node.Direction direction) {
			var neighbour = node.GetNeighbour(direction);
			return neighbour == null || neighbour.Value > node.Value;
		}

		static void Part2(Grid<int> grid) {
			bool[,] marked = new bool[grid.Width, grid.Height];
			List<int> basinSizes = new List<int>();
			for (int x = 0; x < grid.Width; x++) {
				for (int y = 0; y < grid.Height; y++) {
					if (!marked[x, y]) {
						var node = grid.Get(x, y);
						if (node.Value < 9) {
							int basinSize = GetBasinSize(node, marked, 0);
							// Console.WriteLine($"{node}, size = {basinSize}");
							basinSizes.Add(basinSize);
						}

						marked[x, y] = true;
					}
				}
			}

			basinSizes.Sort();
			int basinCount = basinSizes.Count;
			Console.WriteLine("Part 2:");
			Console.WriteLine($"Risk Level = {basinSizes[basinCount - 1] * basinSizes[basinCount - 2] * basinSizes[basinCount - 3]:n0}");
		}

		static int GetBasinSize(Grid<int>.Node node, bool[,] marked, int currentSize) {
			currentSize++;
			marked[node.Position.x, node.Position.y] = true;

			foreach (var neighbour in node.Neighbours) {
				if (neighbour != null && !marked[neighbour.Position.x, neighbour.Position.y] && neighbour.Value < 9) {
					currentSize = GetBasinSize(neighbour, marked, currentSize);
				}
			}

			return currentSize;
		}
	}
}
