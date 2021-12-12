using System.Collections.Generic;

namespace day10 {
	public class Line {
		public readonly List<Chunk> Chunks;

		public Line(string input) {
			Chunks = new List<Chunk>();
			for (int i = 0; i < input.Length; i++) {
				var chunk = new Chunk(input, i);
				Chunks.Add(chunk);
				if (chunk.EndIndex == -1) {
					break;
				}

				i = chunk.EndIndex;
			}
		}

		public string GetCompletionString() {
			return Chunks[Chunks.Count - 1].GetCompletionString();
		}

		public override string ToString() {
			return string.Join(string.Empty, Chunks);
		}
	}
}