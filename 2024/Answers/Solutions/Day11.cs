using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(11)]
public class Day11 : IAnswer
{
	static readonly int[] Powers = [0, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000];

	public Solution Solve(string input)
	{
		var span = input.AsSpan();
		var stones1 = new Dictionary<long, long>(1024 * 9);
		var stones2 = new Dictionary<long, long>(stones1.Capacity);

		foreach (var range in span.Split(' '))
		{
			var stone = long.Parse(span[range]);
			Set(stones1, stone, 1);
		}

		for (var day = 1; day <= 25; day++) Blink(ref stones1, ref stones2);
		var part1 = stones1.Values.Sum();

		for (var day = 26; day <= 75; day++) Blink(ref stones1, ref stones2);
		var part2 = stones1.Values.Sum();

		return new(part1.ToString(), part2.ToString());
	}

	static void Blink(ref Dictionary<long, long> stones1, ref Dictionary<long, long> stones2)
	{
		foreach (var (stone, count) in stones1)
		{
			if (stone == 0)
			{
				Set(stones2, 1, count);
				continue;
			}

			var digits = Digits(stone);

			if (digits % 2 != 0)
			{
				Set(stones2, stone * 2024, count);
				continue;
			}

			var (a, b) = Math.DivRem(stone, Powers[digits / 2]);
			Set(stones2, a, count);
			Set(stones2, b, count);
		}

		(stones1, stones2) = (stones2, stones1);
		stones2.Clear();
	}

	static void Set(Dictionary<long, long> stoneCounts, long key, long count)
	{
		if (!stoneCounts.TryGetValue(key, out var c))
		{
			stoneCounts[key] = count;
		}
		else
		{
			stoneCounts[key] = c + count;
		}
	}

	static int Digits(long num)
	{
		var i = 0;

		while (num > 0)
		{
			num /= 10;
			++i;
		}

		return i;
	}
}
