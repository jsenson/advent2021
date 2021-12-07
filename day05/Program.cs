using System;
using Shared;

namespace day5 {
	class Program {
		static void Main(string[] args) {
			LineSegment[] segments = InputParser.ParseToArray<LineSegment>(InputParser.INPUT, InputParser.NEWLINE, entry => new LineSegment(entry));
			Part1(segments);
			Console.WriteLine();
			Part2(segments);
		}

		static void Part1(LineSegment[] lines) {
			(int width, int height) dimensions = GetDimensions(lines);
			HeatMap map = new HeatMap(dimensions.width, dimensions.height);
			foreach(var line in lines) {
				if(line.Start.x == line.End.x || line.Start.y == line.End.y) {
					// Console.WriteLine(line.ToString());
					map.ApplyLine(line);
				}
			}

			// map.Print();
			Console.WriteLine("Part 1:");
			Console.WriteLine($"Overlaps = {map.GetOverlaps(2)}");
		}

		static void Part2(LineSegment[] lines) {
			(int width, int height) dimensions = GetDimensions(lines);
			HeatMap map = new HeatMap(dimensions.width, dimensions.height);
			foreach(var line in lines) {
				// Console.WriteLine(line.ToString());
				map.ApplyLine(line);
			}

			// map.Print();
			Console.WriteLine("Part 2:");
			Console.WriteLine($"Overlaps = {map.GetOverlaps(2)}");
		}

		private static (int, int) GetDimensions(LineSegment[] lines) {
			(int width, int height) dimensions = (0, 0);
			foreach(var line in lines) {
				if(line.Start.x > dimensions.width) {
					dimensions.width = line.Start.x;
				}

				if(line.End.x > dimensions.width) {
					dimensions.width = line.End.x;
				}

				if(line.Start.y > dimensions.height) {
					dimensions.height = line.Start.y;
				}

				if(line.End.y > dimensions.height) {
					dimensions.height = line.End.y;
				}
			}

			dimensions.width++;
			dimensions.height++;
			return dimensions;
		}
	}
}
