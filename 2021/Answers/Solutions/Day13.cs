using System;
using System.Linq;
using System.Text;

namespace AdventOfCode2021;

[Answer(13)]
class Day13 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var inputs = input.Split("\n\n");
		var numbers = inputs[0]
			.Split('\n')
			.Select(l => l
				.Split(',', 2)
				.Select(n => int.Parse(n))
				.ToArray()
			)
			.ToArray();

		var maxX = 0;
		var maxY = 0;
		var part1 = 0;

		foreach (var line in inputs[1].Split('\n'))
		{
			var fold = int.Parse(line[13..]);
			var isY = line[11] == 'y';

			if (isY)
			{
				foreach (var number in numbers)
				{
					var y = number[1];
					if (y > fold)
					{
						number[1] = 2 * fold - y;
					}
				}

				maxY = fold;
			}
			else
			{
				foreach (var number in numbers)
				{
					var x = number[0];
					if (x > fold)
					{
						number[0] = 2 * fold - x;
					}
				}

				maxX = fold;
			}

			if (part1 == 0)
			{
				part1 = numbers.DistinctBy(x => x[1] * 10000 + x[0]).Count();
			}
		}

		var grid = new bool[maxY * maxX + maxX];

		foreach (var coord in numbers)
		{
			grid[coord[1] * maxX + coord[0]] = true;
		}

		var part2 = new StringBuilder(maxY * maxX + maxY + 1);
		part2.Append('\n');

		for (int y = 0; y < maxY; y++)
		{
			for (int x = 0; x < maxX; x++)
			{
				part2.Append(grid[y * maxX + x] ? '#' : ' ');
			}

			part2.Append('\n');
		}

		return (part1.ToString(), part2.ToString());
	}
}
