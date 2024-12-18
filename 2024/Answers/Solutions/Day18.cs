using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(18)]
public class Day18 : IAnswer
{
	readonly static (int x, int y)[] Directions =
	[
		(0, 1), // down
		(1, 0), // right
		(-1, 0), // left
		(0, -1), // up
	];

	record struct Coord(int X, int Y);

	public Solution Solve(string input)
	{
		var finish = input.Length < 500 ? 6 : 70;
		var size = finish + 1;
		var steps = new Dictionary<int, int>(1024 * 5);
		var stepsList = new List<Coord>(1024 * 5);
		var i = 0;

		foreach (var lineSpan in input.AsSpan().EnumerateLines())
		{
			var comma = lineSpan.IndexOf(',');

			var x = ParseInt(lineSpan[..comma]);
			var y = ParseInt(lineSpan[(comma + 1)..]);

			steps.Add(x * size + y, i++);
			stepsList.Add(new Coord(x, y));
		}

		var activeTiles = new PriorityQueue<Coord, int>(256);
		var visitedTiles = new Dictionary<int, int>(1024 * 10);

		void Enqueue(int tileX, int tileY, int cost, int maxCorruptedSteps)
		{
			if (tileX < 0 || tileY < 0 || tileX >= size || tileY >= size)
			{
				return;
			}

			var hash = tileX * size + tileY;

			if (visitedTiles.GetValueOrDefault(hash, int.MaxValue) <= cost)
			{
				return;
			}

			visitedTiles[hash] = cost;

			if (steps.TryGetValue(hash, out var step) && step < maxCorruptedSteps)
			{
				return;
			}

			activeTiles.Enqueue(new Coord(tileX, tileY), cost);
		}

		int Simulate(int maxCorruptedSteps)
		{
			while (activeTiles.TryDequeue(out var checkTile, out var cost))
			{
				if (checkTile.X == finish && checkTile.Y == finish)
				{
					return cost;
				}

				foreach (var (dx, dy) in Directions)
				{
					Enqueue(checkTile.X + dx, checkTile.Y + dy, cost + 1, maxCorruptedSteps);
				}
			}

			return -1;
		}

		Enqueue(0, 0, 0, int.MaxValue);

		var part1 = Simulate(size == 7 ? 12 : 1024);

		// part 2
		var left = part1 - 1;
		var right = stepsList.Count - 1;

		while (left <= right)
		{
			var mid = (left + right) / 2;

			activeTiles.Clear();
			visitedTiles.Clear();

			Enqueue(0, 0, 0, int.MaxValue);

			if (Simulate(mid) > -1)
			{
				left = mid + 1;
			}
			else
			{
				right = mid - 1;
			}
		}

		var s = stepsList[right];
		var part2 = $"{s.X},{s.Y}";

		return new(part1.ToString(), part2);
	}

	static int ParseInt(ReadOnlySpan<char> line)
	{
		var result = 0;
		var i = 0;

		do
		{
			result = 10 * result + line[i++] - '0';
		}
		while (i < line.Length);

		return result;
	}
}
