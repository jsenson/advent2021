using System;
using System.Collections.Generic;
using Shared;

namespace day15 {
	class Program {
		static void Main(string[] args) {
			string[] lines = InputParser.ParseRaw(InputParser.INPUT, InputParser.NEWLINE);
			Grid<short> grid = new Grid<short>(lines[0].TrimEnd().Length, lines.Length);
			for (int y = 0; y < lines.Length; y++) {
				string line = lines[y].TrimEnd();
				for (int x = 0; x < line.Length; x++) {
					grid.Get(x, y).Value = Convert.ToInt16(line[x].ToString());
				}
			}

			var start = new Vector2Int(0, 0);
			var end = new Vector2Int(grid.Width - 1, grid.Height - 1);
			Console.WriteLine("Part 1:");
			Part1(grid, start, end);
			Console.WriteLine();
			Part2(grid, 5);
		}

		static void Part1(Grid<short> grid, Vector2Int start, Vector2Int end) {
			var config = new AStar.Config {
				StartPosition = start,
				EndPosition = end,
				GridWidth = grid.Width,
				GridHeight = grid.Height,
				IsWalkable = _ => true,
				GetNeighbours = pos => GetNeighbours(grid.Get(pos.x, pos.y)),
				GetHeuristic = pos => GetHeuristic(pos, end),
				GetTraversalCost = (_, to) => grid.Get(to.x, to.y).Value
			};

			List<Vector2Int> path = AStar.GetPath(config);

			// Don't count the first step
			int total = -grid.Get(start.x, start.y).Value;
			foreach (var pos in path) {
				// Console.WriteLine($"{pos} = {grid.Get(pos.x, pos.y).Value}");
				total += grid.Get(pos.x, pos.y).Value;
			}

			Console.WriteLine($"Lowest Risk: {total:n0}");
		}

		static void Part2(Grid<short> sourceGrid, int multiplier) {
			Grid<short> grid = new Grid<short>(sourceGrid.Width * multiplier, sourceGrid.Height * multiplier);
			for (int x = 0; x < multiplier; x++) {
				for (int y = 0; y < multiplier; y++) {
					var origin = new Vector2Int(sourceGrid.Width * x, sourceGrid.Height * y);
					for (int sx = 0; sx < sourceGrid.Width; sx++) {
						for (int sy = 0; sy < sourceGrid.Height; sy++) {
							int value = sourceGrid.Get(sx, sy).Value;
							var pos = new Vector2Int(origin.x + sx, origin.y + sy);
							value += x + y;
							if (value > 9) {
								value -= 9;
							}

							grid.Get(pos.x, pos.y).Value = (short)value;
						}
					}
				}
			}

			Console.WriteLine("Part 2:");
			Part1(grid, new Vector2Int(0, 0), new Vector2Int(grid.Width - 1, grid.Height - 1));
		}

		static Vector2Int[] GetNeighbours(Grid<short>.Node node) {
			var neighbours = new List<Vector2Int>();
			var neighbour = node.GetNeighbour(Grid<short>.Node.Direction.East);
			if (neighbour != null) neighbours.Add(neighbour.Position);
			neighbour = node.GetNeighbour(Grid<short>.Node.Direction.West);
			if (neighbour != null) neighbours.Add(neighbour.Position);
			neighbour = node.GetNeighbour(Grid<short>.Node.Direction.North);
			if (neighbour != null) neighbours.Add(neighbour.Position);
			neighbour = node.GetNeighbour(Grid<short>.Node.Direction.South);
			if (neighbour != null) neighbours.Add(neighbour.Position);

			return neighbours.ToArray();
		}

		static double GetHeuristic(Vector2Int pos, Vector2Int endPos) {
			return endPos.Subtract(pos).Magnitude;
		}
	}
}
