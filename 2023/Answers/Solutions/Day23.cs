using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(23)]
public class Day23 : IAnswer
{
	record struct Vector2i(int X, int Y)
	{
		public static Vector2i operator +(Vector2i left, Vector2i right)
			=> new(left.X + right.X, left.Y + right.Y);
	}

	record struct Vector2WithDistance(Vector2i Position, int Distance = 1);

	public Solution Solve(string input)
	{
		var grid = new ReadOnlyGrid(input, '#');
		var visited = new HashSet<int>(1024);

		var graph = BuildGraph(true);
		var part1 = Solve(new(0, 1), 0);

		visited.Clear();
		graph = BuildGraph(false);
		var part2 = Solve(new(0, 1), 0);

		Dictionary<Vector2i, List<Vector2WithDistance>> BuildGraph(bool part1)
		{
			var graph = new Dictionary<Vector2i, List<Vector2WithDistance>>();

			for (var y = 0; y < grid.Height; y++)
			{
				for (var x = 0; x < grid.Width; x++)
				{
					if (grid[y, x] == '#')
					{
						continue;
					}

					var position = new Vector2i(y, x);
					graph[position] = [];

					if (part1)
					{
						switch (grid[y, x])
						{
							case 'v':
								graph[position].Add(new(position + new Vector2i(1, 0)));
								continue;

							case '>':
								graph[position].Add(new(position + new Vector2i(0, 1)));
								continue;
						}
					}

					if (grid[y + 1, x] != '#')
					{
						graph[position].Add(new(position + new Vector2i(1, 0)));
					}

					if (grid[y - 1, x] != '#')
					{
						graph[position].Add(new(position + new Vector2i(-1, 0)));
					}

					if (grid[y, x + 1] != '#')
					{
						graph[position].Add(new(position + new Vector2i(0, 1)));
					}

					if (grid[y, x - 1] != '#')
					{
						graph[position].Add(new(position + new Vector2i(0, -1)));
					}
				}
			}

			while (true)
			{
				var trimmableEdges = graph.Where(n => n.Value.Count == 2).ToList();

				if (trimmableEdges.Count == 0)
				{
					break;
				}

				foreach (var edge in trimmableEdges)
				{
					var distance = edge.Value[0].Distance + edge.Value[1].Distance;
					var i = graph[edge.Value[0].Position].FindIndex(c => c.Position == edge.Key);

					if (i > -1)
					{
						graph[edge.Value[0].Position].RemoveAt(i);
					}

					graph[edge.Value[0].Position].Add(new(edge.Value[1].Position, distance));

					i = graph[edge.Value[1].Position].FindIndex(c => c.Position == edge.Key);

					if (i > -1)
					{
						graph[edge.Value[1].Position].RemoveAt(i);
					}

					graph[edge.Value[1].Position].Add(new(edge.Value[0].Position, distance));

					graph.Remove(edge.Key);
				}
			}

			return graph;
		}

		int Solve(Vector2i position, int distance)
		{
			var maxDistance = 0;

			if (position.X == grid.Height - 1 && position.Y == grid.Width - 2)
			{
				return distance;
			}

			foreach (var edge in graph[position])
			{
				var hash = edge.Position.X * grid.Width + edge.Position.Y;

				if (!visited.Add(hash))
				{
					continue;
				}

				var newDistance = Solve(edge.Position, distance + edge.Distance);

				if (maxDistance < newDistance)
				{
					maxDistance = newDistance;
				}

				visited.Remove(hash);
			}

			return maxDistance;
		}

		return new(part1.ToString(), part2.ToString());
	}
}
