using System;
using System.Linq;

namespace AdventOfCode;

[Answer(14)]
public class Day14 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var maxX = 0;
		var maxY = 0;

		var lines = input
			.Split('\n')
			.Select(line => line
				.Split(" -> ")
				.Select(coords =>
				{
					var parsed = coords
						.Split(',')
						.Select(num => int.Parse(num))
						.ToArray();

					if (maxX < parsed[0])
					{
						maxX = parsed[0];
					}

					if (maxY < parsed[1])
					{
						maxY = parsed[1];
					}

					return (X: parsed[0], Y: parsed[1]);
				})
				.ToArray()
			)
			.ToArray();

		maxX += maxY;
		maxY += 2;

		var map = new bool[maxX * maxY];

		foreach (var line in lines)
		{
			for (var i = 1; i < line.Length; i++)
			{
				var start = line[i - 1];
				var end = line[i];

				if (start.X == end.X)
				{
					var startY = Math.Min(start.Y, end.Y);
					var endY = Math.Max(start.Y, end.Y);

					for (var y = startY; y <= endY; y++)
					{
						map[y * maxX + start.X] = true;
					}
				}
				else
				{
					var startX = Math.Min(start.X, end.X);
					var endX = Math.Max(start.X, end.X);

					for (var x = startX; x <= endX; x++)
					{
						map[start.Y * maxX + x] = true;
					}
				}
			}
		}

		var landed = true;
		var countp1 = true;

		do
		{
			var sandX = 500;
			var sandY = 0;
			landed = false;

			while (true)
			{
				if (sandY == maxY - 1)
				{
					countp1 = false;
					landed = true;
					part2++;
					map[sandY * maxX + sandX] = true;
					break;
				}

				if (!map[(sandY + 1) * maxX + sandX])
				{
					sandY++;
					continue;
				}

				if (!map[(sandY + 1) * maxX + sandX - 1])
				{
					sandX--;
					sandY++;
					continue;
				}

				if (!map[(sandY + 1) * maxX + sandX + 1])
				{
					sandX++;
					sandY++;
					continue;
				}

				if (countp1)
				{
					part1++;
				}

				landed = true;
				part2++;
				map[sandY * maxX + sandX] = true;
				break;
			}
		}
		while (landed && !map[500]);

		return (part1.ToString(), part2.ToString());
	}
}
