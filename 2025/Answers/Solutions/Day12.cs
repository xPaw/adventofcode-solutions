using System;
using System.Linq;

namespace AdventOfCode;

[Answer(12)]
public class Day12 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;

		var blocks = input.Split("\n\n");
		var shapes = new int[6];

		for (var i = 0; i < 6; i++)
		{
			shapes[i] = blocks[i].Count(static c => c == '#');
		}

		foreach (var line in blocks[^1].EnumerateLines())
		{
			var i = line.IndexOf('x');
			var j = line.IndexOf(':');
			var a = line[..i].ParseInt();
			var b = line[(i + 1)..j].ParseInt();

			var ints = line[(j + 2)..].ToString().Split(' ').Select(s => s.ParseInt()).ToArray();
			var area = 0;

			for (var k = 0; k < 6; k++)
			{
				area += ints[k] * shapes[k];
			}

			if (area <= a * b)
			{
				part1++;
			}
		}

		return new(part1.ToString(), "0");
	}
}
