using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(10)]
public class Day10 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var grid = new ReadOnlyGrid(input);
		var visited = new HashSet<int>(128);

		for (var y = 0; y < grid.Height; y++)
		{
			for (var x = 0; x < grid.Width; x++)
			{
				var c = grid[y, x];

				if (c != '0')
				{
					continue;
				}

				visited.Clear();
				var (a, b) = FollowTrail(grid, 0, x, y, c, visited);
				part1 += a;
				part2 += b;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	(int Part1, int Part2) FollowTrail(ReadOnlyGrid grid, int currentScore, int x, int y, char current, HashSet<int> visited)
	{
		var p1 = 0;
		var p2 = 0;

		foreach (var (dx, dy) in Coord.Directions)
		{
			var xx = x + dx;
			var yy = y + dy;
			var c = grid[yy, xx];

			if (c - current != 1)
			{
				continue;
			}

			if (c == '9')
			{
				p1 += visited.Add(xx * 10000 + yy) ? 1 : 0;
				p2 += 1;
				continue;
			}

			var (a, b) = FollowTrail(grid, currentScore + 1, xx, yy, c, visited);
			p1 += a;
			p2 += b;
		}

		return (p1, p2);
	}
}
