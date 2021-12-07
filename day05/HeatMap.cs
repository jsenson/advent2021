using System;
using System.Text;
using Shared;

namespace day5 {
	public class HeatMap {
		private int[,] _map;

		public HeatMap(int width, int height) {
			_map = new int[width, height];
		}

		public void ApplyLine(LineSegment line) {
			Vector3Int pos = line.Start;
			int xDir = line.Start.x <= line.End.x ? 1 : -1;
			int yDir = line.Start.y <= line.End.y ? 1 : -1;
			while(pos != line.End) {
				_map[pos.x, pos.y]++;
				// Console.WriteLine($"mark ({pos.x}, {pos.y}) = {_map[pos.x, pos.y]}");
				if((xDir == 1 && pos.x < line.End.x) || (xDir == -1 && pos.x > line.End.x)) pos.x += xDir;
				if((yDir == 1 && pos.y < line.End.y) || (yDir == -1 && pos.y > line.End.y)) pos.y += yDir;
			}

			_map[pos.x, pos.y]++;
		}

		public int GetOverlaps(int threshold) {
			int count = 0;
			for(int x = 0, xLength = _map.GetLength(0); x < xLength; x++) {
				for(int y = 0, yLength = _map.GetLength(1); y < yLength; y++) {
					if(_map[x, y] >= threshold) {
						count++;
					}
				}
			}

			return count;
		}

		public void Print() {
			var sb = new StringBuilder();
			for(int y = 0, yLength = _map.GetLength(1); y < yLength; y++) {
				sb.Clear();
				for(int x = 0, xLength = _map.GetLength(0); x < xLength; x++) {
					sb.Append(_map[x, y].ToString());
				}

				Console.WriteLine(sb.ToString());
			}
		}
	}
}