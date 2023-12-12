using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode;

[Answer(12)]
public class Day12 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;

		static long Count(Dictionary<string, long> cache, ReadOnlySpan<char> springs, ReadOnlySpan<int> numbers)
		{
			if (numbers.Length == 0)
			{
				if (springs.Contains('#'))
				{
					return 0;
				}

				return 1;
			}

			if (springs.Length == 0)
			{
				return 0;
			}

			var hash = springs.ToString() + string.Join(',', numbers.ToArray());

			if (cache.TryGetValue(hash, out var result))
			{
				return result;
			}

			var c = springs[0];
			var n = numbers[0];

			if (c != '#')
			{
				result += Count(cache, springs[1..], numbers);
			}

			if (c != '.')
			{
				if (n == springs.Length)
				{
					if (!springs[..n].Contains('.'))
					{
						result += Count(cache, string.Empty, numbers[1..]);
					}
				}
				else if (n < springs.Length && springs[n] != '#' && !springs[..n].Contains('.'))
				{
					result += Count(cache, springs[(n + 1)..], numbers[1..]);
				}
			}

			cache[hash] = result;

			return result;
		}

		Parallel.ForEach(input.Split('\n'), line =>
		{
			var cache = new Dictionary<string, long>();

			var space = line.IndexOf(' ');
			var springs = line[..space].AsSpan();
			var numbers = line[(space + 1)..].ToString().Split(',').Select(int.Parse).ToArray();

			Interlocked.Add(ref part1, Count(cache, springs, numbers));

			springs = $"{springs}?{springs}?{springs}?{springs}?{springs}".AsSpan();
			numbers = [.. numbers, .. numbers, .. numbers, .. numbers, .. numbers];

			Interlocked.Add(ref part2, Count(cache, springs, numbers));
		});

		return new(part1.ToString(), part2.ToString());
	}
}
