using System;

namespace AdventOfCode;

[Answer(1)]
public class Day1 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var dial = 50;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var value = line[1..].ParseInt();

			if (line[0] == 'L')
			{
				if (dial == 0)
				{
					dial = 100 - value;
				}
				else
				{
					dial -= value;
				}

				if (dial < 0)
				{
					var crossings = (-dial + 99) / 100;
					part2 += crossings;
					dial += crossings * 100;
				}

				if (dial == 0)
				{
					part2++;
				}
			}
			else
			{
				dial += value;

				if (dial > 99)
				{
					var (quotient, remainder) = Math.DivRem(dial, 100);
					part2 += quotient;
					dial = remainder;
				}
			}

			if (dial == 0)
			{
				part1++;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
