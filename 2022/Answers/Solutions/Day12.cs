using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(12)]
public class Day12 : IAnswer
{
	record Tile(int X, int Y);

	public (string Part1, string Part2) Solve(string input)
	{
		var size = input.IndexOf('\n');
		var sizeHeight = input.Length / size;
		var map = new int[size * sizeHeight];
		var offset = 0;
		var start = 0;
		var finish = 0;
		var alternateStarts = new List<int>();

		foreach (var c in input)
		{
			if (c == '\n')
			{
				continue;
			}

			var letter = c;

			if (c == 'S')
			{
				start = offset;
				letter = 'a';
			}
			else if (c == 'E')
			{
				finish = offset;
				letter = 'z';
			}

			var height = letter - 'a';

			if (height == 0)
			{
				alternateStarts.Add(offset);
			}

			map[offset++] = height;
		}

		var part1 = Solve(map, size, start, finish, sizeHeight);
		var part2 = Solve(map, size, finish, finish, sizeHeight, true);

		return (part1.ToString(), part2.ToString());
	}

	private int Solve(int[] map, int size, int start, int finish, int height, bool backwards = false)
	{
		int hash;
		var visitedTiles = new HashSet<int>();
		var activeTiles = new PriorityQueue<Tile, int>();
		activeTiles.Enqueue(new Tile(start % size, start / size), 0);

		visitedTiles.Add(start);

		void Enqueue(int tileX, int tileY, int cost)
		{
			var tile1 = map[hash];
			var newHash = tileY * size + tileX;
			var tile2 = map[newHash];

			if (backwards)
			{
				if (tile1 - tile2 > 1)
				{
					return;
				}
			}
			else if (tile2 - tile1 > 1)
			{
				return;
			}

			if (!visitedTiles.Add(newHash))
			{
				return;
			}

			activeTiles.Enqueue(new Tile(tileX, tileY), cost + 1);
		}

		while (activeTiles.TryDequeue(out var checkTile, out var cost))
		{
			hash = checkTile.Y * size + checkTile.X;

			if (backwards)
			{
				if (map[hash] == 0)
				{
					return cost;
				}
			}
			else if (hash == finish)
			{
				return cost;
			}

			if (checkTile.Y > 0)
			{
				Enqueue(checkTile.X, checkTile.Y - 1, cost);
			}

			if (checkTile.Y + 1 < height)
			{
				Enqueue(checkTile.X, checkTile.Y + 1, cost);
			}

			if (checkTile.X > 0)
			{
				Enqueue(checkTile.X - 1, checkTile.Y, cost);
			}

			if (checkTile.X + 1 < size)
			{
				Enqueue(checkTile.X + 1, checkTile.Y, cost);
			}
		}

		return int.MaxValue;
	}
}
