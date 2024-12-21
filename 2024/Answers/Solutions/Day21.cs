using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(21)]
public class Day21 : IAnswer
{
	public Solution Solve(string input)
	{
		var pad = new ReadOnlyGrid("789\n456\n123\n 0A", ' ');
		var dir = new ReadOnlyGrid(" ^A\n<v>", ' ');
		var cache = new Dictionary<int, long>(1024);
		var part1 = 0L;
		var part2 = 0L;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var len = 0L;
			var current = 'A';

			foreach (var target in line)
			{
				len += Simulate(cache, pad, dir, current, target, 0, 3);
				current = target;
			}

			var numericValue = int.Parse(line[..(line.Length - 1)]);
			part1 += numericValue * len;
		}

		cache.Clear();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var len = 0L;
			var current = 'A';

			foreach (var target in line)
			{
				len += Simulate(cache, pad, dir, current, target, 0, 26);
				current = target;
			}

			var numericValue = int.Parse(line[..(line.Length - 1)]);
			part2 += numericValue * len;
		}

		return new(part1.ToString(), part2.ToString());
	}

	long Simulate(Dictionary<int, long> cache, ReadOnlyGrid pad, ReadOnlyGrid dir, char from, char to, int depth, int max)
	{
		if (depth == max)
		{
			return 1;
		}

		var hash = (from - '0') * 100000 + (to - '0') * 1000 + depth;

		if (cache.TryGetValue(hash, out var cached))
		{
			return cached;
		}

		var min = long.MaxValue;
		var grid = depth == 0 ? pad : dir;
		var paths = Move(grid, grid.IndexOf(from), grid.IndexOf(to));

		foreach (var path in paths)
		{
			var len = 0L;
			var current = 'A';

			foreach (var next in path)
			{
				len += Simulate(cache, pad, dir, current, next, depth + 1, max);
				current = next;
			}

			if (min > len)
			{
				min = len;
			}
		}

		cache[hash] = min;

		return min;
	}

	List<string> Move(ReadOnlyGrid grid, Coord pos, Coord target)
	{
		var paths = new List<string>();

		if (pos == target)
		{
			paths.Add("A");
			return paths;
		}

		if (grid[pos] == ' ')
		{
			return paths;
		}

		if (target.X < pos.X)
		{
			foreach (var p in Move(grid, pos - (1, 0), target))
			{
				paths.Add("<" + p);
			}
		}
		else if (target.X > pos.X)
		{
			foreach (var p in Move(grid, pos + (1, 0), target))
			{
				paths.Add(">" + p);
			}
		}

		if (target.Y < pos.Y)
		{
			foreach (var p in Move(grid, pos - (0, 1), target))
			{
				paths.Add("^" + p);
			}
		}
		else if (target.Y > pos.Y)
		{
			foreach (var p in Move(grid, pos + (0, 1), target))
			{
				paths.Add("v" + p);
			}
		}

		return paths;
	}
}
