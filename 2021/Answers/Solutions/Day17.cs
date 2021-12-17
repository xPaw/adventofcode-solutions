using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(17)]
class Day17 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var split = input.Split(new char[] {
			'.',
			'=',
			',',
		});
		var x1 = int.Parse(split[1]);
		var x2 = int.Parse(split[3]);
		var y1 = int.Parse(split[5]);
		var y2 = int.Parse(split[7]);

		var part1 = y1 * (y1 + 1) / 2;
		var part2 = 0;
		var yMin = Math.Min(y1, y2);
		var yMax = Math.Abs(yMin);
		var xMin = ((int)Math.Sqrt(8 * x1 + 1) - 1) / 2;

		for (var x = xMin; x <= x2; x++)
		{
			if (x > x2 / 2 && x < x1)
			{
				continue;
			}

			for (var y = yMin; y <= yMax; y++)
			{
				var coordinates = (x: 0, y: 0);
				var velocity = (x, y);

				while (true)
				{
					coordinates.x += velocity.x;
					coordinates.y += velocity.y;

					if (coordinates.x > x2 || coordinates.y < yMin)
					{
						break;
					}

					if (coordinates.x >= x1 && coordinates.x <= x2 && coordinates.y >= y1 && coordinates.y <= y2)
					{
						part2++;
						break;
					}

					if (velocity.x > 0)
					{
						if (--velocity.x == 0 && coordinates.x < x1)
						{
							break;
						}
					}

					velocity.y--;
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
