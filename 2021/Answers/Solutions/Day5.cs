using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(5)]
class Day5 : IAnswer
{
	record Coordinate(int X, int Y);

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input
			.Split('\n')
			.Select(l => l
				.Split(" -> ")
				.Select(n =>
				{
					var i = n
					.Split(',')
					.Select(x => int.Parse(x))
					.ToArray();

					return new Coordinate(i[0], i[1]);
				}
				)
				.ToList()
			)
			.ToList();

		var maxX = 1 + lines.Max(x => x.Max(x => x.X));
		var maxY = 1 + lines.Max(y => y.Max(y => y.Y));
		var grid = new byte[maxX * maxY];
		var part1 = CalculateGrid(grid, maxX, lines, false);
		var part2 = CalculateGrid(grid, maxX, lines, true);

		return (part1.ToString(), part2.ToString());
	}

	int CalculateGrid(byte[] grid, int stride, List<List<Coordinate>> lines, bool allowDiagonals = false)
	{
		foreach (var coords in lines)
		{
			var start = coords[0];
			var end = coords[1];
			var lenX = end.X - start.X;
			var lenY = end.Y - start.Y;
			var dX = Math.Sign(lenX);
			var dY = Math.Sign(lenY);

			if ((dX == 0 || dY == 0) == allowDiagonals)
			{
				continue;
			}

			var length = dY == 0 ? Math.Abs(lenX) : Math.Abs(lenY);

			for (var i = 0; i <= length; i++)
			{
				var x = start.X + i * dX;
				var y = start.Y + i * dY;
				grid[y * stride + x]++;
			}
		}

		return grid.Count(x => x > 1);
	}
}
