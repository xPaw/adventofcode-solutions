using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(11)]
public class Day11 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;

		var grid = new ReadOnlyGrid(input);
		var emptyRows = new List<int>(16);
		var emptyCols = new List<int>(16);
		var galaxies = new List<(int X, int Y)>(512);

		for (var y = 0; y < grid.Height; y++)
		{
			bool emptyRow = true;
			bool emptyCol = true;

			for (var x = 0; x < grid.Width; x++)
			{
				if (grid[x, y] == '#')
				{
					emptyRow = false;
					galaxies.Add((y, x));
				}

				if (grid[y, x] == '#')
				{
					emptyCol = false;
				}
			}

			if (emptyRow)
			{
				emptyRows.Add(y);
			}

			if (emptyCol)
			{
				emptyCols.Add(y);
			}
		}

		for (var i = 0; i < galaxies.Count; i++)
		{
			for (var j = i + 1; j < galaxies.Count; j++)
			{
				var (x1, y1) = galaxies[i];
				var (x2, y2) = galaxies[j];

				var minX = Math.Min(x1, x2);
				var maxX = Math.Max(x1, x2);
				var minY = Math.Min(y1, y2);
				var maxY = Math.Max(y1, y2);

				var steps1 = (maxX - minX) + (maxY - minY);
				var steps2 = steps1;

				foreach (var x in emptyRows)
				{
					if (x >= maxX)
					{
						break;
					}

					if (x > minX)
					{
						steps1 += 1;
						steps2 += 999_999;
					}
				}

				foreach (var y in emptyCols)
				{
					if (y >= maxY)
					{
						break;
					}

					if (y > minY)
					{
						steps1 += 1;
						steps2 += 999_999;
					}
				}

				part1 += steps1;
				part2 += steps2;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
