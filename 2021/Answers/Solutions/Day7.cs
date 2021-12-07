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
		var part1 = int.MaxValue;
		var part2 = int.MaxValue;

		for (var position = 1; position < max; position++)
		{
			var fuel = 0;
			var fuel2 = 0;

			for (var i = 0; i < numbers.Length; i++)
			{
				var diff = Math.Abs(numbers[i] - position);
				fuel += diff;
				fuel2 += diff * (diff + 1) / 2;
			}

			if (part1 > fuel)
			{
				part1 = fuel;
			}

			if (part2 > fuel2)
			{
				part2 = fuel2;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
