using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(14)]
public class Day14 : IAnswer
{
	public Solution Solve(string input)
	{
		var grid = input.Split('\n').Select(l => l.ToArray()).ToArray();
		var size = grid.Length;

		int Score()
		{
			var score = 0;

			for (var y = 0; y < size; y++)
			{
				for (var x = 0; x < size; x++)
				{
					if (grid[y][x] == 'O')
					{
						score += size - y;
					}
				}
			}

			return score;
		}

		void Tilt(int dx, int dy)
		{
			var yStart = dy == -1 ? 1 : 0;
			var xStart = dx == -1 ? 1 : 0;
			var yEnd = dy == 1 ? size - 1 : size;
			var xEnd = dx == 1 ? size - 1 : size;
			bool moved;

			do
			{
				moved = false;

				for (var y = yStart; y < yEnd; y++)
				{
					for (var x = xStart; x < xEnd; x++)
					{
						if (grid[y][x] == 'O' && grid[y + dy][x + dx] == '.')
						{
							grid[y][x] = '.';
							grid[y + dy][x + dx] = 'O';
							moved = true;
						}
					}
				}
			}
			while (moved);
		}

		void Cycle()
		{
			Tilt(0, -1);
			Tilt(-1, 0);
			Tilt(0, 1);
			Tilt(1, 0);
		}

		long Hash()
		{
			const long m = 1_000_000_009;
			const long p = 3;

			long hash = 0;
			long power = 1;

			for (int y = 0; y < size; y++)
			{
				for (int x = 0; x < size; x++)
				{
					if (grid[y][x] == 'O')
					{
						hash = (hash + power) % m;
					}

					power = power * p % m;
				}
			}

			return hash;
		}

		// Part 1
		Tilt(0, -1);

		var part1 = Score();

		// Part 2
		var cache = new Dictionary<long, int>();
		var position = 0;
		long hash = 0;

		while (!cache.TryGetValue(hash, out position))
		{
			Cycle();

			cache[hash] = cache.Count;
			hash = Hash();
		}

		var cycle = cache.Count - position;
		var remaining = (1_000_000_000 - position) % cycle;

		for (var i = 0; i < remaining; i++)
		{
			Cycle();
		}

		var part2 = Score();

		return new(part1.ToString(), part2.ToString());
	}
}
