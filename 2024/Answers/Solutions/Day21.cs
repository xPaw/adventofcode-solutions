using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(21)]
public class Day21 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var pad = new ReadOnlyGrid("789\n456\n123\n 0A", ' ');
		var dir = new ReadOnlyGrid(" ^A\n<v>", ' ');

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var pressed = Move(pad, line.ToArray());
			var min = int.MaxValue;

			foreach (var t1 in pressed)
			{
				var t2 = Move(dir, t1);

				foreach (var t3 in t2)
				{
					var t4 = Move(dir, t3);

					foreach (var t5 in t4)
					{
						if (t5.Length < min)
						{
							min = t5.Length;
						}
					}
				}
			}

			var numericValue = int.Parse(line[..(line.Length - 1)]);
			part1 += min * numericValue;
		}

		return new(part1.ToString(), part2.ToString());
	}

	List<string> Move(ReadOnlyGrid grid, IEnumerable<char> line)
	{
		var queue = new Queue<(Coord c, string pressed)>();
		var paths = new List<string>();

		queue.Enqueue((grid.IndexOf('A'), ""));

		var i = 0;

		foreach (var c in line)
		{
			var end = ++i == line.Count();
			var target = grid.IndexOf(c);
			var queue2 = new Queue<(Coord c, string pressed)>();

			while (queue.TryDequeue(out var prev))
			{
				var pos = prev.c;

				if (target == pos)
				{
					if (end)
					{
						paths.Add(prev.pressed + 'A');
					}
					else
					{
						queue2.Enqueue((pos, prev.pressed + 'A'));
					}
				}

				if (target.X < pos.X && grid[pos.Y, pos.X - 1] != ' ')
				{
					var newPos = pos - (1, 0);
					queue.Enqueue((newPos, prev.pressed + '<'));
				}
				else if (target.X > pos.X && grid[pos.Y, pos.X + 1] != ' ')
				{
					var newPos = pos + (1, 0);
					queue.Enqueue((newPos, prev.pressed + '>'));
				}

				if (target.Y < pos.Y && grid[pos.Y - 1, pos.X] != ' ')
				{
					var newPos = pos - (0, 1);
					queue.Enqueue((newPos, prev.pressed + '^'));
				}
				else if (target.Y > pos.Y && grid[pos.Y + 1, pos.X] != ' ')
				{
					var newPos = pos + (0, 1);
					queue.Enqueue((newPos, prev.pressed + 'v'));
				}

			}

			queue = queue2;
		}

		return paths;
	}
}
