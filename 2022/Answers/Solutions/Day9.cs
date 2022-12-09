using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(9)]
public class Day9 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var knots = new (int X, int Y)[10];
		var visited1 = new HashSet<int>();
		var visited2 = new HashSet<int>();

		for (var i = 0; i < input.Length; i++)
		{
			var dir = input[i++] - '0';
			var steps = 0;

			do
			{
				var t = input[++i];

				if (t == '\n')
				{
					break;
				}

				steps = 10 * steps + t - '0';
			}
			while (i < input.Length - 1);

			while (steps-- > 0)
			{
				switch (dir)
				{
					case 'U' - '0': knots[0].X++; break;
					case 'D' - '0': knots[0].X--; break;
					case 'R' - '0': knots[0].Y++; break;
					case 'L' - '0': knots[0].Y--; break;
					default: throw new Exception();
				}

				for (var knot = 1; knot < 10; knot++)
				{
					var dX = knots[knot - 1].X - knots[knot].X;
					var dY = knots[knot - 1].Y - knots[knot].Y;

					if (dX > 1 || dY > 1 || dX < -1 || dY < -1)
					{
						knots[knot].X += Math.Sign(dX);
						knots[knot].Y += Math.Sign(dY);
					}
				}

				visited1.Add(knots[1].X * 10000 + knots[1].Y);
				visited2.Add(knots[9].X * 10000 + knots[9].Y);
			}
		}

		var part1 = visited1.Count;
		var part2 = visited2.Count;

		return (part1.ToString(), part2.ToString());
	}
}
