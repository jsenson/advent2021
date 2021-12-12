using System;
using Shared;

namespace day11 {
	class Program {
		static void Main(string[] args) {
			string[] lines = InputParser.ParseRaw(InputParser.INPUT, InputParser.NEWLINE);
			int width = lines[0].TrimEnd().Length;
			var grid = new Grid<Squid>(width, lines.Length);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < lines.Length; y++) {
					var squid = new Squid();
					squid.Energy = Convert.ToInt16(lines[y][x].ToString());
					grid.Get(x, y).Value = squid;
				}
			}

			Part1(grid.Clone());
			Console.WriteLine();
			Part2(grid);
		}

		static void Part1(Grid<Squid> grid) {
			int totalFlashes = 0;
			for (int i = 0; i < 100; i++) {
				IncrementSquids(grid);
				bool done = false;
				while (!done) {
					int flashed = FlashSquids(grid);
					totalFlashes += flashed;
					done = flashed == 0;
				}

				ResetFlashedSquids(grid);
			}

			// Console.WriteLine(grid.ToString());
			Console.WriteLine("Part 1:");
			Console.WriteLine($"Total Flashes: {totalFlashes}");
		}

		static void Part2(Grid<Squid> grid) {
			int step = 0;
			bool allFlashed = false;
			while (!allFlashed) {
				IncrementSquids(grid);

				step++;
				bool done = false;
				int flashedThisLoop = 0;
				while (!done) {
					int flashed = FlashSquids(grid);
					flashedThisLoop += flashed;
					done = flashed == 0;
				}

				allFlashed = flashedThisLoop == grid.Width * grid.Height;
				ResetFlashedSquids(grid);
			}

			Console.WriteLine("Part 2:");
			Console.WriteLine($"Step #{step}");
		}

		static void IncrementSquids(Grid<Squid> grid) {
			for (int x = 0; x < grid.Width; x++) {
				for (int y = 0; y < grid.Height; y++) {
					var node = grid.Get(x, y);
					var squid = node.Value;
					squid.Energy++;
					node.Value = squid;
				}
			}
		}

		static int FlashSquids(Grid<Squid> grid) {
			int numFlashed = 0;
			for (int x = 0; x < grid.Width; x++) {
				for (int y = 0; y < grid.Height; y++) {
					var node = grid.Get(x, y);
					var squid = node.Value;

					if (squid.Energy > Squid.FLASH_TRIGGER && !squid.Flashed) {
						// Console.WriteLine($"Flash at ({x}, {y})");
						numFlashed++;
						squid.Flashed = true;
						node.Value = squid;
						int directions = (int)Grid<Squid>.Node.Direction._Count;
						for (int i = 0; i < directions; i++) {
							var neighbour = node.GetNeighbour((Grid<Squid>.Node.Direction)i);
							if (neighbour != null) {
								var nSquid = neighbour.Value;
								nSquid.Energy++;
								neighbour.Value = nSquid;
							}
						}
					}
				}
			}

			return numFlashed;
		}

		static void ResetFlashedSquids(Grid<Squid> grid) {
			for (int x = 0; x < grid.Width; x++) {
				for (int y = 0; y < grid.Height; y++) {
					var node = grid.Get(x, y);
					var squid = node.Value;
					if (squid.Flashed) {
						squid.Flashed = false;
						squid.Energy = 0;
						node.Value = squid;
					}
				}
			}
		}

		private struct Squid {
			public const short FLASH_TRIGGER = 9;

			public short Energy;
			public bool Flashed;

			public override string ToString() {
				return Energy.ToString();
			}
		}
	}
}
