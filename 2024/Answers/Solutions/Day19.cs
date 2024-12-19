using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(19)]
public class Day19 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0L;
		var span = input.AsSpan();
		var nl = span.IndexOf("\n\n");
		var patterns = span[..nl].ToString().Split(", ");
		var designs = span[(nl + 2)..].ToString().Split("\n");
		var cache = new Dictionary<int, long>();

		foreach (var design in designs)
		{
			cache.Clear();

			var count = Simulate(design, 0, patterns, cache);

			if (count > 0)
			{
				part1++;
				part2 += count;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	private long Simulate(string design, int pos, string[] patterns, Dictionary<int, long> cache)
	{
		if (pos > design.Length)
		{
			return 0;
		}

		if (pos == design.Length)
		{
			return 1;
		}

		if (cache.TryGetValue(pos, out var count))
		{
			return count;
		}

		foreach (var pattern in patterns)
		{
			if (pos + pattern.Length > design.Length)
			{
				continue;
			}

			var designSpan = design.AsSpan(pos, pattern.Length);
			var patternSpan = pattern.AsSpan();

			if (designSpan.SequenceEqual(patternSpan))
			{
				count += Simulate(design, pos + pattern.Length, patterns, cache);
			}
		}

		cache[pos] = count;

		return count;
	}
}
