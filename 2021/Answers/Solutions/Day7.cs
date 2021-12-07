using System;
using System.Linq;

namespace AdventOfCode2021;

[Answer(7)]
class Day7 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var numbers = Array.ConvertAll(input.Split(','), int.Parse);
		var max = numbers.Max();

		Array.Sort(numbers);

		var part1 = 0;
		var median = numbers[numbers.Length / 2];

		for (var i = 0; i < numbers.Length; i++)
		{
			part1 += Math.Abs(numbers[i] - median);
		}

		var part2 = BinarySearch(numbers, 0, max, (numbers, position) =>
		{
			var fuel = 0;

			for (var i = numbers.Length - 1; i >= 0; i--)
			{
				var diff = Math.Abs(numbers[i] - position);
				fuel += diff * (diff + 1) / 2;
			}

			return fuel;
		});

		return (part1.ToString(), part2.ToString());
	}

	private int BinarySearch(int[] numbers, int from, int to, Func<int[], int, int> function)
	{
		if (from == to)
		{
			return function(numbers, from);
		}

		var median = (from + to) / 2;

		if (function(numbers, median) < function(numbers, median + 1))
		{
			return BinarySearch(numbers, from, median, function);
		}
		else
		{
			return BinarySearch(numbers, median + 1, to, function);
		}
	}
}
