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
		if (current == '9')
		{
			return visited.Add(x * 10000 + y) ? (1, 1) : (0, 1);
		}

		var score = (Part1: 0, Part2: 0);

		if (grid[y + 1, x] - current == 1)
		{
			var (a, b) = FollowTrail(grid, currentScore + 1, x, y + 1, grid[y + 1, x], visited);
			score.Part1 += a;
			score.Part2 += b;
		}

		if (grid[y - 1, x] - current == 1)
		{
			var (a, b) = FollowTrail(grid, currentScore + 1, x, y - 1, grid[y - 1, x], visited);
			score.Part1 += a;
			score.Part2 += b;
		}

		if (grid[y, x + 1] - current == 1)
		{
			var (a, b) = FollowTrail(grid, currentScore + 1, x + 1, y, grid[y, x + 1], visited);
			score.Part1 += a;
			score.Part2 += b;
		}

		if (grid[y, x - 1] - current == 1)
		{
			var (a, b) = FollowTrail(grid, currentScore + 1, x - 1, y, grid[y, x - 1], visited);
			score.Part1 += a;
			score.Part2 += b;
		}

		return score;
	}
}
