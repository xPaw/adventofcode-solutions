using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(12)]
public class Day12 : IAnswer
{
	record Tile(int X, int Y, int Cost);

	public (string Part1, string Part2) Solve(string input)
	{
		var width = input.IndexOf('\n');
		var height = input.Length / width;
		var map = new byte[width * height];
		var offset = 0;
		var start = 0;
		var finish = 0;

		foreach (var c in input)
		{
			if (c == '\n')
			{
				continue;
			}

			var letter = c;

			if (c == 'S')
			{
				finish = offset;
				letter = 'a';
			}
			else if (c == 'E')
			{
				start = offset;
				letter = 'z';
			}

			map[offset++] = (byte)(letter - 'a');
		}

		var part1 = 0;
		var part2 = 0;

		int hash;
		var visitedTiles = new byte[map.Length];
		var activeTiles = new Queue<Tile>();
		activeTiles.Enqueue(new Tile(start % width, start / width, 0));

		visitedTiles[start] = 1;

		void Enqueue(int tileX, int tileY, int cost)
		{
			var tile1 = map[hash];
			var newHash = tileY * width + tileX;
			var tile2 = map[newHash];

			if (tile1 - tile2 > 1)
			{
				return;
			}

			if (visitedTiles[newHash] == 1)
			{
				return;
			}

			visitedTiles[newHash] = 1;

			activeTiles.Enqueue(new Tile(tileX, tileY, cost + 1));
		}

		while (activeTiles.TryDequeue(out var checkTile))
		{
			hash = checkTile.Y * width + checkTile.X;

			if (hash == finish)
			{
				part1 = checkTile.Cost;
				break;
			}

			if (part2 == 0 && map[hash] == 0)
			{
				part2 = checkTile.Cost;
			}

			if (checkTile.Y > 0)
			{
				Enqueue(checkTile.X, checkTile.Y - 1, checkTile.Cost);
			}

			if (checkTile.Y + 1 < height)
			{
				Enqueue(checkTile.X, checkTile.Y + 1, checkTile.Cost);
			}

			if (checkTile.X > 0)
			{
				Enqueue(checkTile.X - 1, checkTile.Y, checkTile.Cost);
			}

			if (checkTile.X + 1 < width)
			{
				Enqueue(checkTile.X + 1, checkTile.Y, checkTile.Cost);
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
