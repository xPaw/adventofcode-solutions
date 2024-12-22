using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(22)]
public class Day22 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var sequences = new Dictionary<int, int>(19 * 19 * 19 * 19);
		var seenSequences = new HashSet<int>(1024 * 5);
		var prices = new byte[2000];
		var changes = new short[2000];

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var secret = long.Parse(line);

			var previousValue = (byte)(secret % 10);

			for (var i = 0; i < 2000; i++)
			{
				secret ^= secret * 64;
				secret %= 0x1000000;

				secret ^= secret / 32;
				secret %= 0x1000000;

				secret ^= secret * 2048;
				secret %= 0x1000000;

				prices[i] = (byte)(secret % 10);
				changes[i] = (short)(prices[i] - previousValue);
				previousValue = prices[i];
			}

			part1 += secret;

			seenSequences.Clear();

			for (var i = 1; i < 1997; i++)
			{
				var hash = 0;

				for (var j = 0; j < 4; j++)
				{
					hash = hash * 19 + changes[i + j] + 9;
				}

				if (seenSequences.Add(hash))
				{
					sequences.TryGetValue(hash, out var price);
					sequences[hash] = price + prices[i + 3];
				}
			}
		}

		var part2 = sequences.Values.Max();

		return new Solution(part1.ToString(), part2.ToString());
	}
}
