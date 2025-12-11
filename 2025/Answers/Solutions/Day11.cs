using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(11)]
public class Day11 : IAnswer
{
	public Solution Solve(string input)
	{
		var graph = new Dictionary<string, string[]>(1024);
		var cache = new Dictionary<int, long>(2048);

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var colon = line.IndexOf(':');
			var name = line[..colon].ToString();
			var destinations = line[(colon + 2)..].ToString().Split(' ');
			graph[name] = destinations;
		}

		long CountPaths(string start, string end)
		{
			if (start == end)
			{
				return 1;
			}

			var key = HashCode.Combine(start, end);

			if (cache.TryGetValue(key, out var count))
			{
				return count;
			}

			if (!graph.TryGetValue(start, out var neighbors))
			{
				return 0;
			}

			foreach (var next in neighbors)
			{
				count += CountPaths(next, end);
			}

			cache[key] = count;

			return count;
		}

		var part1 = CountPaths("you", "out");
		var part2 = CountPaths("svr", "fft") * CountPaths("fft", "dac") * CountPaths("dac", "out");

		return new(part1.ToString(), part2.ToString());
	}
}
