using System;
using System.Runtime.CompilerServices;

namespace AdventOfCode;

[Answer(7)]
public class Day7 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;
		var count = 0;
		Span<long> numbers = stackalloc long[16];

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var colon = line.IndexOf(':');
			var target = ParseInt(line[..colon]);
			var numbersSpan = line[(colon + 2)..];

			foreach (var num in numbersSpan.Split(' '))
			{
				numbers[count++] = ParseInt(numbersSpan[num]);
			}

			var nums = numbers[..count];

			if (TryOperators(true, nums, target))
			{
				part2 += target;

				if (TryOperators(false, nums, target))
				{
					part1 += target;
				}
			}

			count = 0;
		}

		return new(part1.ToString(), part2.ToString());
	}

	static bool TryOperators(bool allowConcat, Span<long> numbers, long target)
	{
		if (numbers.Length == 0)
		{
			return target == 0;
		}

		if (numbers.Length == 1)
		{
			return target == numbers[0];
		}

		var last = numbers[^1];
		numbers = numbers[..^1];

		if (target % last == 0 && TryOperators(allowConcat, numbers, target / last)) // multiply
		{
			return true;
		}

		target -= last;

		if (allowConcat)
		{
			var mag = Magnitude(last);

			if (target % mag == 0 && TryOperators(allowConcat, numbers, target / mag))
			{
				return true;
			}
		}

		if (TryOperators(allowConcat, numbers, target)) // add
		{
			return true;
		}

		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static long Magnitude(long value) => value switch
	{
		>= 1000 => 10000,
		>= 100 => 1000,
		>= 10 => 100,
		_ => 10,
	};

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
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
