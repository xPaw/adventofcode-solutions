using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(24)]
class Day24 : IAnswer
{
	readonly int[] a = new int[14];
	readonly int[] b = new int[14];
	readonly int[] c = new int[14];

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n');

		for (var i = 0; i < 14; i++)
		{
			a[i] = ParseNumber(lines[4 + 18 * i]);
			b[i] = ParseNumber(lines[5 + 18 * i]);
			c[i] = ParseNumber(lines[15 + 18 * i]);
		}

		var part1 = Solve(true);
		var part2 = Solve(false);

		return (part1.ToString(), part2.ToString());
	}

	int ParseNumber(string line) => int.Parse(line[line.LastIndexOf(' ')..]);

	long Solve(bool max)
	{
		var number = new int[14];
		var stack = new Stack<int>(7);

		for (var i = 0; i < 14; i++)
		{
			if (a[i] == 1)
			{
				number[i] = max ? 9 : 1;
				stack.Push(i);
			}
			else
			{
				var y = stack.Pop();
				var digit = number[y] + c[y] + b[i];
				number[i] = digit;

				if (digit > 9)
				{
					number[y] -= digit - 9;
					number[i] = 9;
				}
				else if (digit < 1)
				{
					number[y] += 1 - digit;
					number[i] = 1;
				}
			}
		}

		long n = 0;

		for (var i = 0; i < 14; i++)
		{
			n = 10 * n + number[i];
		}

		return n;
	}
}
