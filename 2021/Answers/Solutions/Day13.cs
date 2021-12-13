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

		var maxX = numbers.Max(x => x[0]) + 2;
		var maxY = numbers.Max(y => y[1]) + 2;
		var part1 = 0;
		var grid = new bool[maxY, maxX];

		foreach (var coord in numbers)
		{
			grid[coord[1], coord[0]] = true;
		}

		foreach (var line in inputs[1].Split('\n'))
		{
			var fold = int.Parse(line[13..]);
			var isY = line[11] == 'y';

			if (isY)
			{
				var newGrid = new bool[fold, maxX];
				for (int y = 0; y < fold; y++)
				{
					for (int x = 0; x < maxX; x++)
					{
						var y2 = 2 * fold - y;

						if (grid[y, x] || grid[y2, x])
						{
							newGrid[y, x] = true;
						}
					}
				}
				maxY = fold;
				grid = newGrid;
			}
			else
			{
				var newGrid = new bool[maxY, fold];
				for (int y = 0; y < maxY; y++)
				{
					for (int x = 0; x < fold; x++)
					{
						var x2 = 2 * fold - x;

						if (grid[y, x] || grid[y, x2])
						{
							newGrid[y, x] = true;
						}
					}
				}
				maxX = fold;
				grid = newGrid;
			}

			if (part1 == 0)
			{
				for (int y = 0; y < maxY; y++)
				{
					for (int x = 0; x < maxX; x++)
					{
						if (grid[y, x])
						{
							part1++;
						}
					}
				}
			}
		}

		var part2 = new StringBuilder(maxY * maxX + maxY + 1);
		part2.Append('\n');

		for (int y = 0; y < maxY; y++)
		{
			for (int x = 0; x < maxX; x++)
			{
				part2.Append(grid[y, x] ? '#' : ' ');
			}

			part2.Append('\n');
		}

		return (part1.ToString(), part2.ToString());
	}
}
