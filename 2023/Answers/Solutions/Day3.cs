using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(3)]
public class Day3 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var size = input.IndexOf('\n') + 1;

		char GetAtPosition(int x, int y) => input[y * size + x];

		for (var i = 0; i < input.Length; i++)
		{
			var c = input[i];

			if (c == '\n' || c == '.' || char.IsAsciiDigit(c))
			{
				continue;
			}

			var (y, x) = Math.DivRem(i, size);
			var isGear = c == '*';
			var leftNumber = 0;

			for (int y2 = y - 1; y2 <= y + 1; y2++)
			{
				for (int x2 = x - 1; x2 <= x + 1; x2++)
				{
					if (!char.IsAsciiDigit(GetAtPosition(x2, y2)))
					{
						continue;
					}

					while (--x2 >= 0 && char.IsAsciiDigit(GetAtPosition(x2, y2)))
					{
						//
					}

					var number = 0;

					do
					{
						number = number * 10 + (GetAtPosition(++x2, y2) - '0');
					}
					while (char.IsAsciiDigit(GetAtPosition(x2 + 1, y2)));

					part1 += number;

					if (isGear)
					{
						if (leftNumber > 0)
						{
							part2 += leftNumber * number;
						}
						else
						{
							leftNumber = number;
						}
					}
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
