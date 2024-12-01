using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(1)]
public class Day1 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var listA = new List<int>();
		var listB = new List<int>();
		var occurrences = new Dictionary<int, int>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var numLength = (line.Length - 3) / 2;
			var a = ParseInt(line[..numLength]);
			var b = ParseInt(line[(numLength + 3)..]);

			listA.Add(a);
			listB.Add(b);

			if (occurrences.TryGetValue(b, out var count))
			{
				occurrences[b] = count + 1;
			}
			else
			{
				occurrences[b] = 1;
			}
		}

		listA.Sort();
		listB.Sort();

		for (var i = 0; i < listA.Count; i++)
		{
			var a = listA[i];

			part1 += Math.Abs(listB[i] - a);

			if (occurrences.TryGetValue(a, out var count))
			{
				part2 += a * count;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	static int ParseInt(ReadOnlySpan<char> line)
	{
		var result = 0;
		var i = 0;

		do
		{
			result = 10 * result + line[i++] - '0';
		}
		while (i < line.Length);

		return result;
	}
}
