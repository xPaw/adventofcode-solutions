using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(8)]
public class Day8 : IAnswer
{
	readonly record struct Node(string Left, string Right);

	public Solution Solve(string inputStr)
	{
		var part1 = 0;

		var input = inputStr.AsSpan();
		var nl = input.IndexOf('\n');
		var map = new Dictionary<string, Node>();
		var alphaStarts = new List<string>();

		foreach (var line in input[(nl + 2)..].EnumerateLines())
		{
			var key = line[0..3].ToString();
			var left = line[7..10].ToString();
			var right = line[12..15].ToString();

			map[key] = new(left, right);

			if (key[2] == 'A')
			{
				alphaStarts.Add(key);
			}
		}

		var instructions = input[..nl];
		var current = "AAA";
		var i = 0;

		do
		{
			var node = map[current];
			current = instructions[i] == 'R' ? node.Right : node.Left;

			part1++;

			if (current == "ZZZ")
			{
				break;
			}

			i++;
			i %= instructions.Length;
		}
		while (true);

		var test = new List<long>(alphaStarts.Count);

		foreach (var start in alphaStarts)
		{
			i = 0;
			var steps = 0;
			current = start;

			do
			{
				var node = map[current];
				current = instructions[i] == 'R' ? node.Right : node.Left;

				steps++;

				if (current[2] == 'Z')
				{
					break;
				}

				i++;
				i %= instructions.Length;
			}
			while (true);

			test.Add(steps);
		}

		var part2 = test.Aggregate((S, val) => S * val / Gcd(S, val));

		return new(part1.ToString(), part2.ToString());
	}

	static long Gcd(long n1, long n2)
	{
		if (n2 == 0)
		{
			return n1;
		}

		return Gcd(n2, n1 % n2);
	}
}
