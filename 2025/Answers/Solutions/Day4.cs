using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(4)]
public class Day4 : IAnswer
{
	public readonly static Coord[] Directions =
	[
		new Coord(0, 1), // down
		new Coord(1, 0), // right
		new Coord(-1, 0), // left
		new Coord(0, -1), // up

		new Coord(1, 1),
		new Coord(-1, -1),
		new Coord(1, -1),
		new Coord(-1, 1),
	];

	public Solution Solve(string input)
	{
		var rawGrid = input.ToCharArray();
		var counts = new byte[rawGrid.Length];
		var queue = new Queue<Coord>(2048);
		var grid = new ReadOnlyGrid(rawGrid, '.');

		for (var x = 0; x < grid.Width; x++)
		{
			for (var y = 0; y < grid.Height; y++)
			{
				var c = new Coord(x, y);

				if (grid[c] == '.')
				{
					continue;
				}

				var adj = (byte)0;

				foreach (var dir in Directions)
				{
					if (grid[c + dir] == '@')
					{
						adj++;
					}
				}

				counts[grid.PositionOf(c)] = adj;

				if (adj < 4)
				{
					queue.Enqueue(c);
				}
			}
		}

		var part1 = queue.Count;
		var part2 = 0;

		while (queue.Count > 0)
		{
			var c = queue.Dequeue();
			var pos = grid.PositionOf(c);

			if (rawGrid[pos] == '.')
			{
				continue;
			}

			rawGrid[pos] = '.';
			part2++;

			foreach (var dir in Directions)
			{
				var neighbor = c + dir;

				if (grid[neighbor] != '@')
				{
					continue;
				}

				pos = grid.PositionOf(neighbor);

				if (--counts[pos] < 4)
				{
					queue.Enqueue(neighbor);
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
