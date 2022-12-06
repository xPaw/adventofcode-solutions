using System;

namespace AdventOfCode;

[Answer(6)]
public class Day6 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		static int SlideThatWindow(ReadOnlySpan<char> span, int size)
		{
			var start = 0;
			var end = 0;

			do
			{
				end++;

				var duplicate = span[start..end].IndexOf(span[end]);

				if (duplicate > -1)
				{
					start += duplicate + 1;
				}
			}
			while (1 + end - start != size);

			return end + 1;
		}

		var span = input.AsSpan();
		var part1 = SlideThatWindow(span, 4);
		var part2 = SlideThatWindow(span, 14);

		return (part1.ToString(), part2.ToString());
	}
}
