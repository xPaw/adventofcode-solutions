using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(1)]
public class Day1 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var span = input.AsSpan();
		var elves = new List<int>();
		var current = 0;

		while (true)
		{
			var newLine = span.IndexOf('\n');

			if (newLine == 0)
			{
				elves.Add(current);
				current = 0;
				span = span[1..];
				continue;
			}

			ReadOnlySpan<char> str;

			if (newLine == -1)
			{
				str = span;
			}
			else
			{
				str = span[..newLine];
			}

			var num = int.Parse(str);
			current += num;

			if (newLine == -1)
			{
				break;
			}

			span = span[(newLine + 1)..];
		}

		elves.Add(current);

		elves = elves.OrderDescending().ToList();

		var part1 = elves[0];
		var part2 = elves[0] + elves[1] + elves[2];

		return (part1.ToString(), part2.ToString());
	}
}
