using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(3)]
public class Day3 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var span = input.AsSpan();

		var part1 = 0;
		var part2 = 0;
		var line = 0;
		var hasMore = true;
		ReadOnlySpan<char> prev1 = span[0..1];
		ReadOnlySpan<char> prev2 = prev1;

		static int Score(char match)
		{
			if (char.IsLower(match))
			{
				return match - '`';
			}

			return match - '&';
		}

		while (hasMore)
		{
			var end = span.IndexOf('\n');
			ReadOnlySpan<char> full;

			if (end == -1)
			{
				hasMore = false;
				end = span.Length;
				full = span[0..end];
			}
			else
			{
				full = span[0..end];
				span = span[(end + 1)..];
			}

			var half = end / 2;
			var a = full[..half];
			var b = full[half..];

			part1 += Score(a[a.IndexOfAny(b)]);

			switch (line++ % 3)
			{
				case 0: prev1 = full; break;
				case 1: prev2 = full; break;
				case 2:
					foreach (var c in full)
					{
						if (prev1.IndexOf(c) >= 0 && prev2.IndexOf(c) >= 0)
						{
							part2 += Score(c);
							break;
						}
					}
					break;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}

