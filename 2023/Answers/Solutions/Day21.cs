using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(21)]
public class Day21 : IAnswer
{
	public Solution Solve(string input)
	{
		var grid = new ReadOnlyGrid(input);
		var center = (grid.Height / 2, grid.Width / 2);

		var unique = new HashSet<(int Y, int X)>
		{
			center
		};

		HashSet<(int Y, int X)> Step(HashSet<(int Y, int X)> current)
		{
			var newUnique = new HashSet<(int Y, int X)>(current.Count);

			foreach (var (y, x) in current)
			{
				var p = (Y: y + 1, X: x);
				if (grid.InfiniteAt(p) != '#')
				{
					newUnique.Add(p);
				}

				p = (y - 1, x);
				if (grid.InfiniteAt(p) != '#')
				{
					newUnique.Add(p);
				}

				p = (y, x + 1);
				if (grid.InfiniteAt(p) != '#')
				{
					newUnique.Add(p);
				}

				p = (y, x - 1);
				if (grid.InfiniteAt(p) != '#')
				{
					newUnique.Add(p);
				}
			}

			return newUnique;
		}

		for (var i = 0; i < 64; i++)
		{
			unique = Step(unique);
		}

		var part1 = unique.Count;

		// p2
		var (x, remainder) = Math.DivRem(26501365L, grid.Width);

		var sequence = new int[3];
		var steps = 0;
		unique = [center];

		for (var i = 0; i < 3; i++)
		{
			var end = i * grid.Width + remainder;

			while (steps < end)
			{
				unique = Step(unique);
				steps++;
			}

			sequence[i] = unique.Count;
		}

		var c = sequence[0];
		var a = (sequence[2] - c - (sequence[1] - c) * 2) / 2;
		var b = sequence[1] - c - a;

		// ax^2 + bx + c
		var part2 = a * x * x + b * x + c;

		return new(part1.ToString(), part2.ToString());
	}
}
