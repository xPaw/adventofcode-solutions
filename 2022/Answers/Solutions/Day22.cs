using System;
using System.Linq;

namespace AdventOfCode;

[Answer(22)]
public class Day22 : IAnswer
{
	const int RIGHT = 0;
	const int DOWN = 1;
	const int LEFT = 2;
	const int UP = 3;

	public (string Part1, string Part2) Solve(string input)
	{
		var part2 = 0;

		var split = input.Split("\n\n");
		var map = split[0].Split('\n').Select(x => x.ToCharArray()).ToArray();
		var code = split[1];

		var x = Array.IndexOf(map[0], '.');
		var y = 0;
		var facing = 0;

		for (var i = 0; i < code.Length; i++)
		{
			if (code[i] == 'L' || code[i] == 'R')
			{
				facing += code[i] == 'L' ? -1 : 1;
				facing = (facing % 4 + 4) % 4;

				continue;
			}

			var moves = 0;

			do
			{
				var t = code[i++];

				if (t == 'L' || t == 'R')
				{
					i -= 2;
					break;
				}

				moves = 10 * moves + t - '0';
			}
			while (i < code.Length);

			if (facing == RIGHT)
			{
				while (moves-- > 0)
				{
					var nextX = x + 1;

					if (nextX == map[y].Length || map[y][nextX] == ' ')
					{
						for (nextX = 0; nextX < x; nextX++)
						{
							if (map[y][nextX] != ' ')
							{
								break;
							}
						}
					}

					if (map[y][nextX] == '#')
					{
						break;
					}

					x = nextX;
				}
			}
			else if (facing == LEFT)
			{
				while (moves-- > 0)
				{
					var nextX = x - 1;

					if (nextX == -1 || map[y][nextX] == ' ')
					{
						for (nextX = map[y].Length - 1; nextX > x; nextX--)
						{
							if (map[y][nextX] != ' ')
							{
								break;
							}
						}
					}

					if (map[y][nextX] == '#')
					{
						break;
					}

					x = nextX;
				}
			}
			else if (facing == DOWN)
			{
				while (moves-- > 0)
				{
					var nextY = y + 1;

					if (nextY == map.Length || x > map[nextY].Length || map[nextY][x] == ' ')
					{
						for (nextY = 0; nextY < y; nextY++)
						{
							if (map[nextY][x] != ' ')
							{
								break;
							}
						}
					}

					if (map[nextY][x] == '#')
					{
						break;
					}

					y = nextY;
				}
			}
			else if (facing == UP)
			{
				while (moves-- > 0)
				{
					var nextY = y - 1;

					if (nextY == -1 || x > map[nextY].Length || map[nextY][x] == ' ')
					{
						for (nextY = map.Length - 1; nextY > y; nextY--)
						{
							if (x < map[nextY].Length && map[nextY][x] != ' ')
							{
								break;
							}
						}
					}

					if (map[nextY][x] == '#')
					{
						break;
					}

					y = nextY;
				}
			}
		}

		var part1 = 1000 * (y + 1) + 4 * (x + 1) + facing;

		return (part1.ToString(), part2.ToString());
	}
}
