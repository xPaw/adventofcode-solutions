using System;

namespace AdventOfCode;

[Answer(6)]
public class Day6 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		int SlideThatWindow(int size)
		{
			var start = 0;

			for (var end = 0; end < input.Length; end++)
			{
				var c = input[end];

				for (var j = start; j < end; j++)
				{
					if (c == input[j])
					{
						start = j + 1;
						break;
					}
				}

				if (1 + end - start == size)
				{
					return end + 1;
				}
			}

			return 0;
		}

		var part1 = SlideThatWindow(4);
		var part2 = SlideThatWindow(14);

		return (part1.ToString(), part2.ToString());
	}
}
