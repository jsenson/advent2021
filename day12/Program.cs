using System;
using System.Collections.Generic;
using Shared;

namespace day12 {
	class Program {
		static void Main(string[] args) {
			var nodes = new Dictionary<string, Node>();
			InputParser.ParseRaw(InputParser.INPUT, InputParser.NEWLINE, line => {
				AddNodes(line, nodes);
				return line;
			});

			// PrintNodes(nodes);
			Part1(nodes);
			Console.WriteLine();
			Part2(nodes);
		}

		static void Part1(Dictionary<string, Node> nodes) {
			Stack<string> path = new Stack<string>();
			List<Stack<string>> paths = new List<Stack<string>>();	// Actually don't need to track full paths at all.  Could just increment a counter.
			BuildPaths(nodes[Node.END], path, nodes, paths);
			// PrintPaths(paths);

			Console.WriteLine("Part 1:");
			Console.WriteLine($"Total Paths: {paths.Count}");
		}

		static void Part2(Dictionary<string, Node> nodes) {
			Stack<string> path = new Stack<string>();
			List<Stack<string>> paths = new List<Stack<string>>();
			BuildPaths2(nodes[Node.END], path, nodes, paths, 2);
			// PrintPaths(paths);

			Console.WriteLine("Part 2:");
			Console.WriteLine($"Total Paths: {paths.Count}");
		}

		static void AddNodes(string line, Dictionary<string, Node> nodes) {
			string[] nodeNames = line.Split('-');
			Node n1, n2;
			if (!nodes.TryGetValue(nodeNames[0], out n1)) {
				n1 = new Node(nodeNames[0]);
				nodes[nodeNames[0]] = n1;
			}

			if (!nodes.TryGetValue(nodeNames[1], out n2)) {
				n2 = new Node(nodeNames[1]);
				nodes[nodeNames[1]] = n2;
			}

			n1.Neighbours.Add(n2.Name);
			n2.Neighbours.Add(n1.Name);
		}

		static void BuildPaths(Node node, Stack<string> path, Dictionary<string, Node> nodes, List<Stack<string>> paths) {
			path.Push(node.Name);
			node.Visits++;
			nodes[node.Name] = node;
			if (node.Name == Node.START) {
				paths.Add(new Stack<string>(path));
			} else {
				foreach (var neighbourName in node.Neighbours) {
					Node neighbour = nodes[neighbourName];
					if (neighbour.Size == Node.Sizes.Big || neighbour.Visits == 0) {
						BuildPaths(neighbour, path, nodes, paths);
					}
				}
			}

			path.Pop();
			node.Visits--;
			nodes[node.Name] = node;
		}

		static void BuildPaths2(Node node, Stack<string> path, Dictionary<string, Node> nodes, List<Stack<string>> paths, int smallLimit) {
			path.Push(node.Name);
			node.Visits++;
			nodes[node.Name] = node;
			// Console.WriteLine($"push {node.Name}: {node.Visits} - {smallLimit}");
			if (smallLimit > 1 && node.Size == Node.Sizes.Small && node.Visits == smallLimit) {
				smallLimit--;
			}

			if (node.Name == Node.START) {
				paths.Add(new Stack<string>(path));
			} else {
				foreach (var neighbourName in node.Neighbours) {
					Node neighbour = nodes[neighbourName];
					if (neighbour.Name == Node.START || neighbour.Name == Node.END) {
						if (neighbour.Visits == 0) {
							BuildPaths2(neighbour, path, nodes, paths, smallLimit);
						}
					} else if (neighbour.Size == Node.Sizes.Big || neighbour.Visits < smallLimit) {
						BuildPaths2(neighbour, path, nodes, paths, smallLimit);
					}
				}
			}

			path.Pop();
			node.Visits--;
			nodes[node.Name] = node;
			// Console.WriteLine($"pop {node.Name}: {node.Visits} - {smallLimit}");
		}

		static void PrintNodes(Dictionary<string, Node> nodes) {
			foreach (var node in nodes) {
				Console.WriteLine(node.Value.ToString());
			}
		}

		static void PrintPaths(List<Stack<string>> paths) {
			foreach (var path in paths) {
				Console.WriteLine(string.Join(',', path));
			}

			Console.WriteLine(paths.Count);
		}
	}
}
