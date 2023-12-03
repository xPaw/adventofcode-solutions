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
		var seen = new HashSet<int>(size * 15);

		char GetAtPosition(int x, int y) => input[y * size + x];

		(int Number, int NumberZeroIfSeen) SumNumber(bool isGear, int x, int y)
		{
			while (--x >= 0 && char.IsAsciiDigit(GetAtPosition(x, y)))
			{
				//
			}

			var number = 0;

			do
			{
				number = number * 10 + (GetAtPosition(++x, y) - '0');
			}
			while (char.IsAsciiDigit(GetAtPosition(x + 1, y)));

			var leftReturn = seen.Add(x * size + y) ? number : 0;
			var rightReturn = isGear && seen.Add(x * size * 1000 + y) ? number : 0;

			return (leftReturn, rightReturn);
		}

		for (var i = 0; i < input.Length; i++)
		{
			var c = input[i];

			if (c == '\n' || c == '.')
			{
				continue;
			}

			if (!char.IsAsciiDigit(c))
			{
				var (y, x) = Math.DivRem(i, size);
				var isGear = c == '*';
				var leftNumber = 0;

				for (int x2 = x - 1; x2 <= x + 1; x2++)
				{
					for (int y2 = y - 1; y2 <= y + 1; y2++)
					{
						var c2 = GetAtPosition(x2, y2);

						if (char.IsAsciiDigit(c2))
						{
							var (leftReturn, rightReturn) = SumNumber(isGear, x2, y2);

							part1 += leftReturn;

							if (rightReturn > 0)
							{
								if (leftNumber > 0)
								{
									part2 += leftNumber * rightReturn;
								}
								else
								{
									leftNumber = rightReturn;
								}
							}
						}
					}
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
