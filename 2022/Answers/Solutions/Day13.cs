using System;

namespace AdventOfCode;

[Answer(13)]
public class Day13 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		input = input.Replace("10", ":");

		ReadOnlySpan<char> previous = string.Empty;
		var i = 0;
		var pair = 1;
		var part1 = 0;

		var div1 = "[[2]]".AsSpan();
		var div2 = "[[6]]".AsSpan();
		var div1pos = 1;
		var div2pos = 2;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			if (i == 2)
			{
				i = 0;
				pair++;
				continue;
			}

			if (i++ == 0)
			{
				previous = line;
			}
			else
			{
				if (ComparePackets(previous, line))
				{
					part1 += pair;
				}
			}

			if (!ComparePackets(div2, line))
			{
				div2pos++;

				if (!ComparePackets(div1, line))
				{
					div1pos++;
				}
			}
		}

		var part2 = div1pos * div2pos;

		return (part1.ToString(), part2.ToString());
	}

	bool ComparePackets(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
	{
		while (true)
		{
			if (left[0] == right[0])
			{
				left = left[1..];
				right = right[1..];
				continue;
			}

			if (left[0] == ']')
			{
				return true;
			}

			if (right[0] == ']')
			{
				return false;
			}

			if (left[0] == '[')
			{
				left = left[1..];
				right = string.Concat(right[0..1], "]".AsSpan(), right[1..]);
				continue;
			}

			if (right[0] == '[')
			{
				left = string.Concat(left[0..1], "]".AsSpan(), left[1..]);
				right = right[1..];
				continue;
			}

			return left[0] < right[0];
		}
	}
}
