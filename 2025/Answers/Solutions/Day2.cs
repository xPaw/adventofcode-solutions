using System;

namespace AdventOfCode;

[Answer(2)]
public class Day2 : IAnswer
{
	public Solution Solve(string inputStr)
	{
		var part1 = 0L;
		var part2 = 0L;
		var input = inputStr.AsSpan();
		Span<char> s = stackalloc char[19];

		foreach (var numberRange in input.Split(','))
		{
			var range = input[numberRange];
			var dashIndex = range.IndexOf('-');
			var start = range[..dashIndex].ParseLong();
			var end = range[(dashIndex + 1)..].ParseLong();

			for (var n = start; n <= end; n++)
			{
				var t = Check2(n, s);

				if (t > 0)
				{
					if (t % 2 == 0)
					{
						part1 += n;
					}

					part2 += n;
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	private int Check2(long n, Span<char> s)
	{
		n.TryFormat(s, out var length);

		for (var check = 1; check <= length / 2; check++)
		{
			var first = s[..check];
			var found = 1;

			for (var i = check; i + check <= length; i += check)
			{
				var second = s[i..(i + check)];

				if (!first.SequenceEqual(second))
				{
					break;
				}

				found++;
			}

			if (found == Math.Ceiling(length / (float)check))
			{
				return found;
			}
		}

		return 0;
	}
}
