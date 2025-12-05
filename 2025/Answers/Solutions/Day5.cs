using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(5)]
public class Day5 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0L;
		var count = 0;

		var i = input.IndexOf("\n\n");
		var left = input.AsSpan(0, i);
		var right = input.AsSpan(i + 2);

		var ranges = new List<(long Min, long Max)>(256);

		foreach (var line in left.EnumerateLines())
		{
			i = line.IndexOf('-');

			var min = line[..i].ParseLong();
			var max = line[(i + 1)..].ParseLong();

			ranges.Add((min, max));
		}

		ranges.Sort(static (a, b) => a.Min.CompareTo(b.Min));

		for (i = 0; i < ranges.Count; i++)
		{
			var (min, max) = ranges[i];

			while (i + 1 < ranges.Count && ranges[i + 1].Min <= max + 1)
			{
				i++;

				if (max < ranges[i].Max)
				{
					max = ranges[i].Max;
				}
			}

			ranges[count++] = (min, max);
			part2 += max - min + 1;
		}

		foreach (var line in right.EnumerateLines())
		{
			var value = line.ParseLong();

			for (i = 0; i < count; i++)
			{
				var (min, max) = ranges[i];

				if (value >= min && value <= max)
				{
					part1++;
					break;
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
