using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(16)]
public class Day16 : IAnswer
{
	record struct Vector2i(int X, int Y);
	record struct NextVector2i(Vector2i Position, Vector2i Delta);

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var grid = input.Split('\n').Select(l => l.ToCharArray()).ToArray();
		var size = grid.Length;

		int Solve(Vector2i pos, Vector2i delta)
		{
			var visited = new HashSet<NextVector2i>();
			var stack = new Stack<NextVector2i>();
			stack.Push(new(pos, delta));

			while (stack.TryPop(out var data))
			{
				pos = data.Position;
				delta = data.Delta;

				if (!visited.Add(data))
				{
					continue;
				}

				var posNext = new Vector2i(pos.X + delta.X, pos.Y + delta.Y);

				if (posNext.X < 0 || posNext.Y < 0 || posNext.X >= size || posNext.Y >= size)
				{
					continue;
				}

				switch (grid[posNext.Y][posNext.X])
				{
					case '\\':
						stack.Push(new(posNext, new(delta.Y, delta.X)));
						break;

					case '/':
						stack.Push(new(posNext, new(-delta.Y, -delta.X)));
						break;

					case '|' when delta.X != 0:
						stack.Push(new(posNext, new(0, 1)));
						stack.Push(new(posNext, new(0, -1)));
						break;

					case '-' when delta.Y != 0:
						stack.Push(new(posNext, new(1, 0)));
						stack.Push(new(posNext, new(-1, 0)));
						break;

					default:
						stack.Push(new(posNext, delta));
						break;
				}
			}

			return visited.Select(x => x.Position).Distinct().Count() - 1;
		}

		part1 = Solve(new(-1, 0), new(1, 0));
		part2 = Enumerable.Range(0, size).AsParallel().Select(i =>
		{
			var max = Solve(new(i, -1), new(0, 1)); // top
			max = Math.Max(max, Solve(new(-1, i), new(1, 0))); // left
			max = Math.Max(max, Solve(new(size, i), new(-1, 0))); // right
			max = Math.Max(max, Solve(new(i, size), new(0, -1))); // bottom
			return max;
		}).Max();

		return new(part1.ToString(), part2.ToString());
	}
}
