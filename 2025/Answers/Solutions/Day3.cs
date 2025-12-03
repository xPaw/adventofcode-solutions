using System;

namespace AdventOfCode;

[Answer(3)]
public class Day3 : IAnswer
{
	private static readonly long[] Powers =
	[
		1,
		10,
		100,
		1000,
		10000,
		100000,
		1000000,
		10000000,
		100000000,
		1000000000,
		10000000000,
		100000000000,
	];

	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			part1 += Check(line, 1);
			part2 += Check(line, 11);
		}

		return new(part1.ToString(), part2.ToString());
	}

	private long Check(ReadOnlySpan<char> line, int digits)
	{
		var result = 0L;

		for (var d = 0; d <= digits; d++)
		{
			var span = line[..^(digits - d)];
			var largestDigit = 0;
			var largestDigitIndex = 0;

			for (var i = 0; i < span.Length; i++)
			{
				var n = span[i] - '0';

				if (largestDigit < n)
				{
					largestDigit = n;
					largestDigitIndex = i;

					if (n == 9)
					{
						break;
					}
				}
			}

			result += Powers[digits - d] * largestDigit;
			line = line[(largestDigitIndex + 1)..];
		}

		return result;
	}
}
