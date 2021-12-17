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

		var part1 = 0;
		var part2 = 0;
		var yMin = Math.Min(y1, y2);
		var yMax = Math.Abs(yMin);

		for (var x = 1; x <= x2; x++)
		{
			for (var y = yMin; y <= yMax; y++)
			{
				var coordinates = (x: 0, y: 0);
				var velocity = (x, y);
				var localMax = 0;

				while (true)
				{
					coordinates.x += velocity.x;
					coordinates.y += velocity.y;

					if (localMax < coordinates.y)
					{
						localMax = coordinates.y;
					}

					if (coordinates.x > x2 || coordinates.y < yMin)
					{
						break;
					}

					if (coordinates.x >= x1 && coordinates.x <= x2 && coordinates.y >= y1 && coordinates.y <= y2)
					{
						part2++;

						if (part1 < localMax)
						{
							part1 = localMax;
						}

						break;
					}

					if (velocity.x != 0)
					{
						velocity.x += velocity.x > 0 ? -1 : 1;
					}

					velocity.y--;
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
