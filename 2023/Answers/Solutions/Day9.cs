using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(9)]
public class Day9 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var list = new List<List<int>>(32);

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var original = line.ToString().Split(' ').Select(int.Parse).ToList();
			var previous = original;
			bool nonZero;

			do
			{
				var current = new List<int>(previous.Count + 2);
				list.Add(current);

				nonZero = false;

				for (var i = 1; i < previous.Count; i++)
				{
					var diff = previous[i] - previous[i - 1];
					nonZero |= diff != 0;

					current.Add(diff);
				}

				previous = current;
			}
			while (nonZero);

			var up = 0;
			var down = 0;

			for (var i = list.Count - 2; i >= 0; i--)
			{
				up = list[i + 1][^1] + list[i][^1];
				list[i].Add(up);

				down = list[i][0] - list[i + 1][0];
				list[i].Insert(0, down);
			}

			list.Clear();

			part1 += original[^1] + up;
			part2 += original[0] - down;
		}

		return new(part1.ToString(), part2.ToString());
	}
}
