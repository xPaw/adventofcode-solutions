using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(7)]
public class Day7 : IAnswer
{
	private static readonly long[] Powers = [1, 10, 100, 1000, 10000, 100000, 1000000];

	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;
		var numbers = new List<long>(32);

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			numbers.Clear();

			var colon = line.IndexOf(':');
			var result = ParseInt(line[..colon]);
			var numbersSpan = line[(colon + 2)..];

			foreach (var num in numbersSpan.Split(' '))
			{
				numbers.Add(ParseInt(numbersSpan[num]));
			}

			if (TryOperators(false, numbers, 0, numbers[0], result))
			{
				part1 += result;
			}

			if (TryOperators(true, numbers, 0, numbers[0], result))
			{
				part2 += result;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	static bool TryOperators(bool allowConcat, List<long> values, int pos, long current, long target)
	{
		if (current > target)
		{
			return false;
		}

		if (pos == values.Count - 1)
		{
			return current == target;
		}

		pos++;
		var next = values[pos];

		if (TryOperators(allowConcat, values, pos, current + next, target)) // add
		{
			return true;
		}

		if (TryOperators(allowConcat, values, pos, current * next, target)) // multiply
		{
			return true;
		}

		if (!allowConcat)
		{
			return false;
		}

		var digits = 1;
		var remainder = next;
		while (remainder >= 10)
		{
			digits++;
			remainder /= 10;
		}
		var concatenated = current * Powers[digits] + next;

		if (TryOperators(allowConcat, values, pos, concatenated, target))
		{
			return true;
		}

		return false;
	}

	static long ParseInt(ReadOnlySpan<char> line)
	{
		var result = 0L;
		var i = 0;

		do
		{
			result = 10 * result + line[i++] - '0';
		}
		while (i < line.Length);

		return result;
	}
}
