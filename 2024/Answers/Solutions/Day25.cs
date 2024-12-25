using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode;

[Answer(25)]
public class Day25 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var span = input.AsSpan();
		var locks = new List<byte[]>();
		var keys = new List<byte[]>();

		foreach (var schema in span.Split("\n\n"))
		{
			var grid = new ReadOnlyGrid(span[schema]);
			var isLock = grid[0, 0] == '#';
			var heights = new byte[5];

			Debug.Assert(grid.Width == 5);
			Debug.Assert(grid.Height == 7);

			for (var x = 0; x < 5; x++)
			{
				byte h = 0;

				for (var y = 0; y < 7; y++)
				{
					if (grid[y, x] == '#')
					{
						h++;
					}
				}

				heights[x] = --h;
			}

			if (isLock)
			{
				locks.Add(heights);
			}
			else
			{
				keys.Add(heights);
			}
		}

		foreach (var loc in locks)
		{
			foreach (var key in keys)
			{
				var fits = true;

				for (var x = 0; x < 5; x++)
				{
					if (loc[x] + key[x] >= 6)
					{
						fits = false;
						break;
					}
				}

				if (fits)
				{
					part1++;
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
