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

			if (allowDiagonals)
			{
				if (Math.Abs(start.X - end.X) != Math.Abs(start.Y - end.Y))
				{
					continue;
				}

				/*
				var dX = start.X < end.X ? 1 : -1;
				var dY = start.Y < end.Y ? 1 : -1;

				for (int x = start.X, y = start.Y; x != end.X || y != end.Y; x += dX, y += dY)
				{
					grid[y * stride + x]++;
				}
				*/

				if (start.X < end.X)
				{
					if (start.Y < end.Y)
					{
						for (int x = start.X, y = start.Y; x <= end.X && y <= end.Y; x++, y++)
						{
							grid[y * stride + x]++;
						}
					}
					else
					{
						for (int x = start.X, y = start.Y; x <= end.X && y >= end.Y; x++, y--)
						{
							grid[y * stride + x]++;
						}
					}
				}
				else
				{
					if (start.Y < end.Y)
					{
						for (int x = start.X, y = start.Y; x >= end.X && y <= end.Y; x--, y++)
						{
							grid[y * stride + x]++;
						}
					}
					else
					{
						for (int x = start.X, y = start.Y; x >= end.X && y >= end.Y; x--, y--)
						{
							grid[y * stride + x]++;
						}
					}
				}
			}
			else
			{
				if (!(start.X == end.X || start.Y == end.Y))
				{
					continue;
				}

				var startX = Math.Min(start.X, end.X);
				var startY = Math.Min(start.Y, end.Y);
				var endX = Math.Max(start.X, end.X);
				var endY = Math.Max(start.Y, end.Y);

				for (var x = startX; x <= endX; x++)
				{
					for (var y = startY; y <= endY; y++)
					{
						grid[y * stride + x]++;
					}
				}
			}
		}

		return grid.Count(x => x > 1);
	}
}
