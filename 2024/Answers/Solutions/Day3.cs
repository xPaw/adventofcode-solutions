using System;
using System.Buffers;

namespace AdventOfCode;

[Answer(3)]
public class Day3 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var doing = true;
		var span = input.AsSpan();

		var instructions = SearchValues.Create([
			"mul(",
			"do()",
			"don't()"
		], StringComparison.Ordinal);
		int match;

		while ((match = span.IndexOfAny(instructions)) != -1)
		{
			if (span[match] == 'd')
			{
				doing = span[match + 2] == '(';
				match += doing ? "do()".Length : "don't()".Length;
				span = span[match..];
				continue;
			}

			span = span[(match + 4)..];

			var a = 0;
			var b = 0;

			while (char.IsAsciiDigit(span[0]))
			{
				a = 10 * a + span[0] - '0';
				span = span[1..];
			}

			if (span[0] != ',')
			{
				continue;
			}

			span = span[1..]; // ,

			while (char.IsAsciiDigit(span[0]))
			{
				b = 10 * b + span[0] - '0';
				span = span[1..];
			}

			if (span[0] != ')')
			{
				continue;
			}

			span = span[1..]; // )

			part1 += a * b;

			if (doing)
			{
				part2 += a * b;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
