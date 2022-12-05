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

		var stacks1 = new Stack<char>[10];
		var stacks2 = new Stack<char>[10];
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
					stacks1[stack].Push(c);
					stacks2[stack].Push(c);
				}

				stack++;
			}
		}

		// reverse the stacks
		for (i = 0; i < stacks1.Length; i++)
		{
			stacks1[i] = new(stacks1[i]);
			stacks2[i] = new(stacks2[i]);
		}

		i = input.IndexOf('m', length);
		length = input.Length;

		var temp = new char[48];

		for (; i < length; i++)
		{
			var move = ParseIntUntilFromSpace(' ');
			var moves = move;
			var from = ParseIntUntilFromSpace(' ') - 1;
			var to = ParseIntUntilFromSpace('\n') - 1;

			var fromStack1 = stacks1[from];
			var toStack1 = stacks1[to];
			var fromStack2 = stacks2[from];
			var toStack2 = stacks2[to];
			//var temp = new char[move];

			while (move-- > 0)
			{
				toStack1.Push(fromStack1.Pop());
				temp[move] = fromStack2.Pop();
			}

			for (var j = 0; j < moves; j++)
			{
				toStack2.Push(temp[j]);
			};
		}

		string GetResult(Stack<char>[] stacks)
		{
			var result = new StringBuilder(stacks.Length);

			for (i = 0; i < stacks.Length && stacks[i].Count > 0; i++)
			{
				result.Append(stacks[i].Peek());
			}

			return result.ToString();
		}

		var part1 = GetResult(stacks1);
		var part2 = GetResult(stacks2);

		return (part1, part2);
	}
}
