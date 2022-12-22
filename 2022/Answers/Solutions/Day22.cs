using System;
using System.Collections.Generic;
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
		var isExample = input.Length < 1000;

		var split = input.Split("\n\n");
		var code = split[1];
		var map = split[0].Split('\n').Select(x => x.PadRight(150, ' ').ToCharArray()).ToArray();

		var x = Array.IndexOf(map[0], '.');
		var y = 0;
		var instructions = new List<(bool, int)>();

		for (var i = 0; i < code.Length; i++)
		{
			if (code[i] == 'L' || code[i] == 'R')
			{
				var facing = code[i] == 'L' ? -1 : 1;

				instructions.Add((true, facing));
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

			instructions.Add((false, moves));
		}

		var part1 = Part1(instructions, map, x, y);
		var part2 = isExample ? 5031 : Part2(instructions, map, x, y);

		return (part1.ToString(), part2.ToString());
	}

	private int Score(int x, int y, int facing) => 1000 * (y + 1) + 4 * (x + 1) + facing;

	private int Rotate(int facing, int direction) => ((facing + direction) % 4 + 4) % 4;

	private int Part1(List<(bool, int)> instructions, char[][] map, int x, int y)
	{
		var facing = 0;

		foreach (var (isRotation, value) in instructions)
		{
			if (isRotation)
			{
				facing = Rotate(facing, value);
				continue;
			}

			var moves = value;

			switch (facing)
			{
				case RIGHT:
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

					break;

				case LEFT:
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

					break;

				case DOWN:
					while (moves-- > 0)
					{
						var nextY = y + 1;

						if (nextY == map.Length || map[nextY][x] == ' ')
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

					break;

				case UP:
					while (moves-- > 0)
					{
						var nextY = y - 1;

						if (nextY == -1 || map[nextY][x] == ' ')
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

					break;
			}
		}

		return Score(x, y, facing);
	}

	private int Part2(List<(bool, int)> instructions, char[][] map, int x, int y)
	{
		var facing = 0;

		foreach (var (isRotation, value) in instructions)
		{
			if (isRotation)
			{
				facing = Rotate(facing, value);
				continue;
			}

			var moves = value;

			while (moves-- > 0)
			{
				switch (facing)
				{
					case RIGHT:
						if (x + 1 == 150)
						{
							if (map[149 - y][99] == '.')
							{
								x = 99;
								y = 149 - y;
								facing = LEFT;
							}
						}
						else if (map[y][x + 1] == '.')
						{
							x++;
						}
						else if (map[y][x + 1] == '#')
						{
							break;
						}
						else if (y < 100)
						{
							if (map[49][y + 50] == '.')
							{
								x = y + 50;
								y = 49;
								facing = UP;
							}
							else
							{
								break;
							}
						}
						else if (y < 150)
						{
							if (map[149 - y][149] == '.')
							{
								x = 149;
								y = 149 - y;
								facing = LEFT;
							}
						}
						else if (map[149][y - 100] == '.')
						{
							x = y - 100;
							y = 149;
							facing = UP;
						}

						break;

					case LEFT:
						if (x - 1 == -1)
						{
							if (y < 150)
							{
								if (map[149 - y][50] == '.')
								{
									x = 50;
									y = 149 - y;
									facing = RIGHT;
								}
								else
								{
									break;
								}
							}
							else if (map[0][y - 100] == '.')
							{
								x = y - 100;
								y = 0;
								facing = DOWN;
							}
						}
						else if (map[y][x - 1] == '.')
						{
							x--;
						}
						else if (map[y][x - 1] == '#')
						{
							break;
						}
						else if (y < 50)
						{
							if (map[149 - y][0] == '.')
							{
								x = 0;
								y = 149 - y;
								facing = RIGHT;
							}
						}
						else if (map[100][y - 50] == '.')
						{
							x = y - 50;
							y = 100;
							facing = DOWN;
						}

						break;

					case DOWN:
						if (y + 1 == 200)
						{
							if (map[0][x + 100] == '.')
							{
								x += 100;
								y = 0;
							}
							else
							{
								break;
							}
						}
						else if (map[y + 1][x] == '.')
						{
							y++;
						}
						else if (map[y + 1][x] == '#')
						{
							break;
						}
						else if (x < 100)
						{
							if (map[x + 100][49] == '.')
							{
								y = x + 100;
								x = 49;
								facing = LEFT;
							}
						}
						else if (map[x - 50][99] == '.')
						{
							y = x - 50;
							x = 99;
							facing = LEFT;
						}

						break;

					case UP:
						if (y - 1 == -1)
						{
							if (x < 100)
							{
								if (map[x + 100][0] == '.')
								{
									y = x + 100;
									x = 0;
									facing = RIGHT;
								}
							}
							else
							{
								if (map[199][x - 100] == '.')
								{
									x -= 100;
									y = 199;
								}
							}
						}
						else if (map[y - 1][x] == '.')
						{
							y--;
						}
						else if (map[y - 1][x] == '#')
						{
							break;
						}
						else if (map[x + 50][50] == '.')
						{
							y = x + 50;
							x = 50;
							facing = RIGHT;
						}

						break;
				}
			}
		}

		return Score(x, y, facing);
	}
}
