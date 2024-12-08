using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(8)]
public class Day8 : IAnswer
{
	public Solution Solve(string input)
	{
		var grid = new ReadOnlyGrid(input);
		var antennas = new Dictionary<char, List<(int X, int Y)>>(64);

		for (var y = 0; y < grid.Height; y++)
		{
			for (var x = 0; x < grid.Width; x++)
			{
				var c = grid[y, x];

				if (c == '.')
				{
					continue;
				}

				if (antennas.TryGetValue(c, out var list))
				{
					list.Add((x, y));
				}
				else
				{
					antennas.Add(c, [(x, y)]);
				}
			}
		}

		var antinodes1 = new HashSet<int>(512);
		var antinodes2 = new HashSet<int>(2048);

		foreach (var list in antennas.Values)
		{
			for (var i = 0; i < list.Count; i++)
			{
				var a = list[i];

				for (var j = 0; j < list.Count; j++)
				{
					if (i == j)
					{
						continue;
					}

					var (x, y) = list[j];
					antinodes2.Add(x * 10000 + y);

					var dx = x - a.X;
					var dy = y - a.Y;

					x += dx;
					y += dy;

					if (grid[y, x] == '\0')
					{
						continue;
					}

					antinodes1.Add(x * 10000 + y);

					while (grid[y, x] != '\0')
					{
						antinodes2.Add(x * 10000 + y);

						x += dx;
						y += dy;
					}
				}
			}
		}

		var part1 = antinodes1.Count;
		var part2 = antinodes2.Count;

		return new(part1.ToString(), part2.ToString());
	}
}
