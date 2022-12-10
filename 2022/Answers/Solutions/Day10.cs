using System;
using System.Text;

namespace AdventOfCode;

[Answer(10)]
public class Day10 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		const int WIDTH = 40;
		const int HEIGHT = 6;

		var X = 1;
		var cycle = 0;
		var part1 = 0;
		var part2 = new StringBuilder(WIDTH * HEIGHT + HEIGHT + 1);
		part2.Append('\n');

		void CheckCycle()
		{
			var stride = cycle % WIDTH;

			if (stride == X - 1 || stride == X || stride == X + 1)
			{
				part2.Append('#');
			}
			else
			{
				part2.Append('.');
			}

			if (stride == WIDTH - 1)
			{
				part2.Append('\n');
			}

			cycle++;

			if ((cycle + 20) % 40 == 0)
			{
				part1 += X * cycle;
			}
		}

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			CheckCycle();

			if (line[0] == 'a')
			{
				CheckCycle();
				X += int.Parse(line[5..]);
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
