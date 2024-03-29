using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(14)]
class Day14 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		const int HASH_SIZE = 10000;
		var hash = static (int a, int b) => a * HASH_SIZE + b;
		var lines = input.Split('\n');
		var polymer = lines[0].ToArray();
		var insertions = lines
			.Skip(2)
			.Select(l => l
				.Split(" -> ")
				.ToArray()
			)
			.ToDictionary(x => hash(x[0][0], x[0][1]), y => y[1][0]);

		var pairs = new Dictionary<int, long>();
		var chars = new Dictionary<char, long>();
		long part1 = 0;

		for (var i = 0; i < polymer.Length; i++)
		{
			chars[polymer[i]] = chars.GetValueOrDefault(polymer[i]) + 1;

			if (i < polymer.Length - 1)
			{
				var pair = hash(polymer[i], polymer[i + 1]);
				pairs[pair] = pairs.GetValueOrDefault(pair) + 1;
			}
		}

		for (var step = 0; step < 40; step++)
		{
			var copy = new Dictionary<int, long>(pairs);

			foreach (var pair in copy)
			{
				var insert = insertions[pair.Key];
				pairs[pair.Key] -= pair.Value;

				var newPair = hash(pair.Key / HASH_SIZE, insert);
				pairs[newPair] = pairs.GetValueOrDefault(newPair) + pair.Value;

				newPair = hash(insert, pair.Key % HASH_SIZE);
				pairs[newPair] = pairs.GetValueOrDefault(newPair) + pair.Value;
				chars[insert] = chars.GetValueOrDefault(insert) + pair.Value;
			}

			if (step == 9)
			{
				part1 = chars.Values.Max() - chars.Values.Min();
			}
		}

		var part2 = chars.Values.Max() - chars.Values.Min();

		return (part1.ToString(), part2.ToString());
	}
}
