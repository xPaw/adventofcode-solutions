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
				var (startX, startY) = line[i - 1];
				var (endX, endY) = line[i];

				if (startX == endX)
				{
					var start = Math.Min(startY, endY);
					var end = Math.Max(startY, endY);

					for (var y = start; y <= end; y++)
					{
						map[y * maxX + startX] = true;
					}
				}
				else
				{
					var start = Math.Min(startX, endX);
					var end = Math.Max(startX, endX);

					for (var x = start; x <= end; x++)
					{
						map[startY * maxX + x] = true;
					}
				}
			}
		}

		var landed = true;
		var countp1 = true;

		var fall = 1;
		var path = new (short X, short Y)[maxY];
		path[0] = (500, 0);

		do
		{
			var (prevX, prevY) = path[--fall];
			var sandX = prevX;
			var sandY = prevY;
			landed = false;

			while (true)
			{
				path[fall++] = (sandX, sandY);

				if (sandY == maxY - 1)
				{
					countp1 = false;
					landed = true;
					part2++;
					fall--;
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
				fall--;
				map[sandY * maxX + sandX] = true;
				break;
			}
		}
		while (landed && !map[500]);

		return (part1.ToString(), part2.ToString());
	}
}
