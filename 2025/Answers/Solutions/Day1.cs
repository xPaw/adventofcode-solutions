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
			var nonZero = dial != 0;

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

				while (dial < 0)
				{
					part2++;
					dial += 100;
				}

				if (dial == 0)
				{
					part2++;
				}
			}
			else
			{
				dial += value;

				while (dial > 99)
				{
					part2++;
					dial -= 100;
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
