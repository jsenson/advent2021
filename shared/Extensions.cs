using System.Collections.Generic;

namespace Shared {
	public static class Extensions {
		public static void IncrementOrCreate<T>(this Dictionary<T, int> dict, T key, int valueToAdd) {
			if (dict.ContainsKey(key)) {
				dict[key] += valueToAdd;
			} else {
				dict[key] = valueToAdd;
			}
		}

		public static void IncrementOrCreate<T>(this Dictionary<T, long> dict, T key, long valueToAdd) {
			if (dict.ContainsKey(key)) {
				dict[key] += valueToAdd;
			} else {
				dict[key] = valueToAdd;
			}
		}

		public static void IncrementOrCreate<T>(this Dictionary<T, short> dict, T key, short valueToAdd) {
			if (dict.ContainsKey(key)) {
				dict[key] += valueToAdd;
			} else {
				dict[key] = valueToAdd;
			}
		}

		public static void IncrementOrCreate<T>(this Dictionary<T, float> dict, T key, float valueToAdd) {
			if (dict.ContainsKey(key)) {
				dict[key] += valueToAdd;
			} else {
				dict[key] = valueToAdd;
			}
		}

		public static void IncrementOrCreate<T>(this Dictionary<T, double> dict, T key, double valueToAdd) {
			if (dict.ContainsKey(key)) {
				dict[key] += valueToAdd;
			} else {
				dict[key] = valueToAdd;
			}
		}
	}
}