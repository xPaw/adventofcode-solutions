using System;
using System.Linq;

namespace AdventOfCode;

#pragma warning disable CS8618
[Answer(20)]
public class Day20 : IAnswer
{
	class Node
	{
		public long Value;
		public Node Prev;
		public Node Next;
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var numbers = input.Split('\n').Select(int.Parse).ToArray();
		var list1 = new Node[numbers.Length];
		var list2 = new Node[numbers.Length];

		for (var i = 0; i < numbers.Length; i++)
		{
			list1[i] = new Node { Value = numbers[i] };
			list2[i] = new Node { Value = numbers[i] * 811589153L };
		}

		for (var i = 0; i < numbers.Length; i++)
		{
			var prev = Mod(i - 1, numbers.Length);
			var next = (i + 1) % numbers.Length;
			list1[i].Prev = list1[prev];
			list1[i].Next = list1[next];
			list2[i].Prev = list2[prev];
			list2[i].Next = list2[next];
		}

		var part1 = Solve(list1, 1);
		var part2 = Solve(list2, 10);

		return (part1.ToString(), part2.ToString());
	}

	static long Mod(long x, long m)
	{
		var r = x % m;
		return r < 0 ? r + m : r;
	}

	static long Solve(Node[] list, int rounds)
	{
		var length = list.Length - 1;
		var half = length / 2;

		for (var round = 0; round < rounds; round++)
		{
			foreach (var node in list)
			{
				var move = node.Value % length;

				if (move < 0)
				{
					move += length;
				}

				node.Prev.Next = node.Next;
				node.Next.Prev = node.Prev;

				var prev = node.Prev;
				var next = node.Next;

				if (move >= half)
				{
					move = length - move;

					while (move-- > 0)
					{
						prev = prev.Prev;
						next = next.Prev;
					}
				}
				else
				{
					while (move-- > 0)
					{
						prev = prev.Next;
						next = next.Next;
					}
				}

				prev.Next = node;
				next.Prev = node;
				node.Prev = prev;
				node.Next = next;
			}
		}

		var result = 0L;

		foreach (var node in list)
		{
			if (node.Value == 0)
			{
				var start = node;

				for (var i = 0; i < 3; i++)
				{
					for (var move = 0; move < 1000; move++)
					{
						start = start.Next;
					}

					result += start.Value;
				}

				break;
			}
		}

		return result;
	}
}
