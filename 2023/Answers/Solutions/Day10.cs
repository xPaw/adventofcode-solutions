using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(10)]
public class Day10 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var map = input.Split('\n');
		var width = input.IndexOf('\n') + 1;
		var start = input.IndexOf('S');
		var (startY, startX) = Math.DivRem(start, width);

		var startPipe = width > 50 ? 'L' : 'F'; // dont care to write out every single condition

		map[startY] = map[startY][..startX] + startPipe + map[startY][(startX + 1)..];

		var x = startX;
		var y = startY;
		var visited = new HashSet<int>();

		while (true)
		{
			var c = map[y][x];
			var up = c is '|' or 'L' or 'J';
			var down = c is '|' or '7' or 'F';
			var left = c is '-' or '7' or 'J';
			var right = c is '-' or 'L' or 'F';

			if (right && visited.Add(y * width + x + 1))
			{
				part1++;
				x++;
			}
			else if (left && visited.Add(y * width + (x - 1)))
			{
				part1++;
				x--;
			}
			else if (down && visited.Add((y + 1) * width + x))
			{
				part1++;
				y++;
			}
			else if (up && visited.Add((y - 1) * width + x))
			{
				part1++;
				y--;
			}
			else
			{
				break;
			}
		}

		part1 /= 2;

		for (y = 0; y < width; y++)
		{
			var xLeft = -1;
			var xRight = 0;

			for (x = 0; x < width; x++)
			{
				if (visited.Contains(y * width + x))
				{
					xRight = x;

					if (xLeft < 0)
					{
						xLeft = x;
					}
				}
			}

			if (xLeft == -1)
			{
				continue;
			}

			var inside = false;

			for (x = xLeft; x <= xRight; x++)
			{
				if (visited.Contains(y * width + x))
				{
					// F---7 collapse
					// L---J collapse
					// F---J wall
					// L---7 wall
					if (map[y][x] is 'F' or 'L')
					{
						for (var x2 = x; x2 <= xRight; x2++)
						{
							var c = map[y][x2];

							if (c is 'J' or '7')
							{
								if (c == 'J' && map[y][x] == 'F')
								{
									inside = !inside;
								}
								else if (c == '7' && map[y][x] == 'L')
								{
									inside = !inside;
								}

								x = x2;
								break;
							}
						}
					}
					else
					{
						inside = !inside;
					}
				}
				else if (inside)
				{
					part2++;
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
