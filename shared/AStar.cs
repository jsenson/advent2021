using System;
using System.Collections.Generic;

namespace Shared {
	public static class AStar {
		public static List<Vector2Int> GetPath(Config config) {
			Node[,] grid = BuildInternalGrid(config);
			Search(grid, grid[config.StartPosition.x, config.StartPosition.y], config);
			return BuildPath(grid[config.EndPosition.x, config.EndPosition.y]);
		}

		private static Node[,] BuildInternalGrid(Config config) {
			var grid = new Node[config.GridWidth, config.GridHeight];
			for (int x = 0; x < config.GridWidth; x++) {
				for (int y = 0; y < config.GridHeight; y++) {
					var pos = new Vector2Int(x, y);
					var node = new Node {
						Position = pos,
						State = Node.States.Untested,
						Walkable = config.IsWalkable(pos)
					};

					grid[x, y] = node;
				}
			}

			return grid;
		}

		private static List<Node> GetWalkableNeighbours(Node[,] grid, Node node, Config config) {
			var walkable = new List<Node>();
			var neighbours = config.GetNeighbours(node.Position);
			foreach (var neighbour in neighbours) {
				var neighbourNode = grid[neighbour.x, neighbour.y];
				if (neighbourNode.Walkable && neighbourNode.State != Node.States.Closed) {
					int gCost = node.G + config.GetTraversalCost(node.Position, neighbourNode.Position);
					if (neighbourNode.State == Node.States.Open) {
						if (gCost < neighbourNode.G) {
							// Console.WriteLine($"{neighbourNode.Position}: Update G {neighbourNode.G} -> {gCost}");
							neighbourNode.Parent = node;
							neighbourNode.G = gCost;
							walkable.Add(neighbourNode);
						}
					} else {
						neighbourNode.Parent = node;
						neighbourNode.G = gCost;
						neighbourNode.H = config.GetHeuristic(neighbourNode.Position);
						neighbourNode.State = Node.States.Open;
						walkable.Add(neighbourNode);
						// Console.WriteLine($"{neighbourNode.Position}: Open, G = {neighbourNode.G}, H = {neighbourNode.H}, F = {neighbourNode.F}");
					}
				}
			}

			return walkable;
		}

		private static void Search(Node[,] grid, Node node, Config config) {
			List<Node> openList = new List<Node>();
			openList.Add(node);
			while (openList.Count > 0) {
				openList.Sort((x, y) => x.F.CompareTo(y.F));
				node = openList[0];

				if (node.Position == config.EndPosition) {
					return;
				}

				openList.Remove(node);
				node.State = Node.States.Closed;
				var neighbours = GetWalkableNeighbours(grid, node, config);
				foreach (var neighbour in neighbours) {
					openList.Add(neighbour);
				}
			}
		}

		private static List<Vector2Int> BuildPath(Node endNode) {
			var path = new List<Vector2Int>();
			var currentNode = endNode;
			do {
				path.Add(currentNode.Position);
				currentNode = currentNode.Parent;
			} while (currentNode != null);

			path.Reverse();
			return path;
		}

		private class Node {
			public Node Parent;
			public Vector2Int Position;
			public States State;
			public bool Walkable;
			public double H;
			public int G;

			public double F => G + H;

			public enum States {
				Untested,
				Open,
				Closed
			}
		}

		public class Config {
			public Vector2Int StartPosition;
			public Vector2Int EndPosition;
			public int GridWidth;
			public int GridHeight;
			public Func<Vector2Int, bool> IsWalkable;
			public Func<Vector2Int, Vector2Int[]> GetNeighbours;
			public Func<Vector2Int, double> GetHeuristic;
			public Func<Vector2Int, Vector2Int, int> GetTraversalCost;
		}
	}
}