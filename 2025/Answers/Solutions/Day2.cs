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
			if (length % check != 0)
			{
				continue;
			}

			var first = s[..check];
			var repetitions = length / check;
			var allMatch = true;

			for (var i = check; i < length; i += check)
			{
				if (!first.SequenceEqual(s[i..(i + check)]))
				{
					allMatch = false;
					break;
				}
			}

			if (allMatch)
			{
				return repetitions;
			}
		}

		return 0;
	}
}
