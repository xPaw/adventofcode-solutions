using System;

namespace AdventOfCode2021;

[Answer(1)]
class Day1 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
		var numbers = Array.ConvertAll(lines, int.Parse);
		var part1 = 0;
		var part2 = 0;

		for (var i = 1; i < numbers.Length; i++)
		{
			var number = numbers[i];

			if (number > numbers[i - 1])
			{
				part1++;
			}

			if (i > 2 && number > numbers[i - 3])
			{
				part2++;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
