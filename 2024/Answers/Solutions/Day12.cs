
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(12)]
public class Day12 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var grid = new ReadOnlyGrid(input);
		var visited = new bool[grid.Width * grid.Height];
		var queue = new Queue<(int X, int Y)>(128);
		var fences = new HashSet<(int X, int Y, int Direction)>(256);

		int Walk(int x2, int y2, int direction, char plant)
		{
			if (grid[y2, x2] != plant)
			{
				fences.Add((x2, y2, direction));
				return 1;
			}

			if (!visited[y2 * grid.Height + x2])
			{
				queue.Enqueue((x2, y2));
			}

			return 0;
		}

		for (var y = 0; y < grid.Height; y++)
		{
			for (var x = 0; x < grid.Width; x++)
			{
				if (visited[y * grid.Height + x])
				{
					continue;
				}

				var plant = grid[y, x];
				var area = 0;
				var perimeter = 0;
				var sides = 0;

				fences.Clear();
				queue.Enqueue((x, y));

				while (queue.TryDequeue(out var n))
				{
					var hash = n.Y * grid.Height + n.X;

					if (visited[hash])
					{
						continue;
					}

					visited[hash] = true;

					area++;
					perimeter += Walk(n.X + 1, n.Y, 1, plant);
					perimeter += Walk(n.X - 1, n.Y, 2, plant);
					perimeter += Walk(n.X, n.Y + 1, 3, plant);
					perimeter += Walk(n.X, n.Y - 1, 4, plant);
				}

				foreach (var cur in fences)
				{
					sides++;

					var (x2, y2, dir) = cur;
					fences.Remove(cur);

					var d = 0;
					while (fences.Remove((x2 + ++d, y2, dir))) { }

					d = 0;
					while (fences.Remove((x2 - ++d, y2, dir))) { }

					d = 0;
					while (fences.Remove((x2, y2 + ++d, dir))) { }

					d = 0;
					while (fences.Remove((x2, y2 - ++d, dir))) { }
				}

				part1 += area * perimeter;
				part2 += area * sides;
			}
		}


		return new(part1.ToString(), part2.ToString());
	}
}
