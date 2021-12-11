using System;
using System.Linq;

namespace AdventOfCode2021;

[Answer(11)]
class Day11 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		const int SIZE = 10;

		var octopuses = new int[SIZE * SIZE];
		var offset = 0;

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == '\n')
			{
				continue;
			}

			octopuses[offset++] = input[i] - '0';
		}

		var part1 = 0;
		var part2 = 0;
		var step = 0;

		while (part2 == 0)
		{
			for (var x = 0; x < SIZE; x++)
			{
				for (var y = 0; y < SIZE; y++)
				{
					octopuses[x * SIZE + y]++;
				}
			}

			var flash = true;

			while (flash)
			{
				flash = false;

				for (var x = 0; x < SIZE; x++)
				{
					for (var y = 0; y < SIZE; y++)
					{
						offset = x * SIZE + y;

						if (octopuses[offset] > 9)
						{
							octopuses[offset] = int.MinValue;

							for (var dX = -1; dX <= 1; dX++)
							{
								for (var dY = -1; dY <= 1; dY++)
								{
									var x2 = x + dX;
									var y2 = y + dY;

									if (x2 >= 0 && x2 < SIZE && y2 >= 0 && y2 < SIZE)
									{
										offset = x2 * SIZE + y2;

										if (++octopuses[offset] > 9)
										{
											flash = true;
										}
									}
								}
							}
						}
					}
				}
			}

			var allZeroes = true;

			for (var x = 0; x < SIZE; x++)
			{
				for (var y = 0; y < SIZE; y++)
				{
					offset = x * SIZE + y;

					if (octopuses[offset] < 0)
					{
						if (step < 100)
						{
							part1++;
						}

						octopuses[offset] = 0;
					}
					else if (octopuses[offset] > 0)
					{
						allZeroes = false;
					}
				}
			}

			step++;

			if (allZeroes)
			{
				part2 = step;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
