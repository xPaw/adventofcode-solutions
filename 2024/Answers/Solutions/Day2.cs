using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(2)]
public class Day2 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		Span<int> levels = stackalloc int[10];

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var count = 0;
			var number = 0;

			foreach (var c in line)
			{
				if (c != ' ')
				{
					number = (number * 10) + (c - '0');
				}
				else
				{
					levels[count++] = number;
					number = 0;
				}
			}

			levels[count++] = number;

			var unsafeIndex = GetFirstUnsafeIndex(levels[..count], -1);

			if (unsafeIndex == -1)
			{
				part1++;
				part2++;
				continue;
			}

			for (var i = unsafeIndex - 1; i <= unsafeIndex + 1; i++)
			{
				unsafeIndex = GetFirstUnsafeIndex(levels[..count], i);

				if (unsafeIndex == -1)
				{
					part2++;
					break;
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	private static int GetFirstUnsafeIndex(Span<int> levels, int skip)
	{
		var prevNum = -1;
		var prevDiff = 0;

		for (var i = 0; i < levels.Length; i++)
		{
			if (i == skip)
			{
				continue;
			}

			var num = levels[i];

			if (prevNum != -1)
			{
				var diff = prevNum - num;

				if (diff == 0 || diff > 3 || diff < -3)
				{
					return i - 1;
				}
				else if (prevDiff < 0 && diff > 0)
				{
					return i - 1;
				}
				else if (prevDiff > 0 && diff < 0)
				{
					return i - 1;
				}

				prevDiff = diff;
			}

			prevNum = num;
		}

		return -1;
	}

	static int ParseInt(ReadOnlySpan<char> line)
	{
		var result = 0;
		var i = 0;

		do
		{
			result = 10 * result + line[i++] - '0';
		}
		while (i < line.Length);

		return result;
	}
}
