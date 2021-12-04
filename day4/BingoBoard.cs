using System;

namespace day4 {
	class BingoBoard {
		private int[,] _grid;
		private bool[,] _matches;
		private int _size;

		public BingoBoard(string[] lines) {
			_size = lines.Length;
			_grid = new int[_size, _size];
			_matches = new bool[_size, _size];

			for(int i = 0; i < _size; i++) {
				ParseNumberLine(lines[i], i);
			}
		}

		public bool MarkNumber(int number) {
			for(int x = 0; x < _size; x++) {
				for(int y = 0; y < _size; y++) {
					if(_grid[x, y] == number) {
						_matches[x, y] = true;
						return true;
					}
				}
			}

			return false;
		}

		public bool HasWon() {
			for(int x = 0; x < _size; x++) {
				if(CheckColumn(x)) {
					return true;
				}
			}

			for(int y = 0; y < _size; y++) {
				if(CheckRow(y)) {
					return true;
				}
			}

			return false;
		}

		public int GetScore(int calledNumber) {
			return GetUnmarkedSum() * calledNumber;
		}

		private bool CheckRow(int row) {
			for(int x = 0; x < _size; x++) {
				if(!_matches[x, row]) {
					return false;
				}
			}

			return true;
		}

		private bool CheckColumn(int column) {
			for(int y = 0; y < _size; y++) {
				if(!_matches[column, y]) {
					return false;
				}
			}

			return true;
		}

		private int GetUnmarkedSum() {
			int sum = 0;
			for(int x = 0; x < _size; x++) {
				for(int y = 0; y < _size; y++) {
					if(!_matches[x, y]) {
						sum += _grid[x, y];
					}
				}
			}

			return sum;
		}

		private void ParseNumberLine(string line, int lineNum) {
			string[] values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			for(int i = 0; i < values.Length; i++) {
				_grid[i, lineNum] = Convert.ToInt32(values[i]);
			}
		}
	}
}