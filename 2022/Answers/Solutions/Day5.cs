using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode;

[Answer(5)]
public class Day5 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		int i;
		var length = input.IndexOf('1') - 3;

		int ParseIntUntilFromSpace(char c)
		{
			var result = 0;

			i = input.IndexOf(' ', i) + 1;

			do
			{
				var t = input[i++];

				if (t == c)
				{
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < length);

			return result;
		}

		var stacks1 = new LinkedList<char>[10];
		var stacks2 = new LinkedList<char>[10];
		var stack = 0;

		for (i = 0; i < stacks1.Length; i++)
		{
			stacks1[i] = new();
			stacks2[i] = new();
		}

		for (i = 0; i < length; i++)
		{
			var c = input[i];

			if (c == '\n')
			{
				stack = 0;
				continue;
			}

			if ((i % 4) == 1)
			{
				if (c != ' ')
				{
					stacks1[stack].AddLast(c);
					stacks2[stack].AddLast(c);
				}

				stack++;
			}
		}

		i = input.IndexOf('m', length);
		length = input.Length;

		for (; i < length; i++)
		{
			var move = ParseIntUntilFromSpace(' ');
			var from = ParseIntUntilFromSpace(' ') - 1;
			var to = ParseIntUntilFromSpace('\n') - 1;

			var fromStack1 = stacks1[from];
			var toStack1 = stacks1[to];
			var fromStack2 = stacks2[from];
			var toStack2 = stacks2[to];
			var before = toStack2.First!;

			while (move-- > 0)
			{
				toStack1.AddFirst(fromStack1.First!.Value);
				fromStack1.RemoveFirst();

				var item = fromStack2.First!.Value;
				fromStack2.RemoveFirst();

				if (before == null)
				{
					toStack2.AddLast(item);
				}
				else
				{
					toStack2.AddBefore(before, item);
				}
			}
		}

		string GetResult(LinkedList<char>[] stacks)
		{
			var result = new StringBuilder(stacks.Length);

			for (i = 0; i < stacks.Length; i++)
			{
				var first = stacks[i].First;

				if (first == null)
				{
					break;
				}

				result.Append(first.Value);
			}

			return result.ToString();
		}

		var part1 = GetResult(stacks1);
		var part2 = GetResult(stacks2);

		return (part1, part2);
	}
}
