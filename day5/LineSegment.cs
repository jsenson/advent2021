namespace day5 {
	public struct LineSegment {
		private const string VALUE_SEPARATOR = " -> ";
		private const string VECTOR_SEPARATOR = ",";

		public Vector3Int Start { get; private set; }
		public Vector3Int End { get; private set; }

		public LineSegment(string input) {
			string[] values = input.Split(VALUE_SEPARATOR);
			Start = new Vector3Int(values[0], VECTOR_SEPARATOR);
			End = new Vector3Int(values[1], VECTOR_SEPARATOR);
		}

		public override string ToString() {
			return $"{Start} -> {End}";
		}
	}
}