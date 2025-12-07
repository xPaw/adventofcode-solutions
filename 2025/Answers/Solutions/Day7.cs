using System;

namespace AdventOfCode;

[Answer(7)]
public class Day7 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0L;

		var grid = new ReadOnlyGrid(input);
		var rows = new long[grid.Height];
		rows[grid.IndexOf('S').X] = 1;

		for (var y = 1; y < grid.Height; y++)
		{
			for (var x = 0; x < grid.Width; x++)
			{
				if (grid[y, x] == '^')
				{
					var count = rows[x];
					rows[x - 1] += count;
					rows[x] = 0;
					rows[x + 1] += count;

					if (count != 0)
					{
						part1++;
					}
				}
			}
		}

		foreach (var count in rows)
		{
			part2 += count;
		}

		return new(part1.ToString(), part2.ToString());
	}
}
