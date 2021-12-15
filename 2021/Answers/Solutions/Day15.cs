using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(15)]
class Day15 : IAnswer
{

	record Tile(int X, int Y, int Cost);

	public (string Part1, string Part2) Solve(string input)
	{
		var size = input.IndexOf('\n');
		var map = new int[size * size];
		var offset = 0;

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == '\n')
			{
				continue;
			}

			map[offset++] = input[i] - '0';
		}

		var part1 = Solve(map, size, size - 1);
		var part2 = Solve(map, size, size * 5 - 1);

		return (part1.ToString(), part2.ToString());
	}

	private int Solve(int[] map, int size, int finish)
	{
		var start = new Tile(0, 0, 0);
		var visitedTiles = new HashSet<int>();
		var activeTiles = new PriorityQueue<Tile, int>();
		activeTiles.Enqueue(start, 0);

		var Enqueue = (int tileX, int tileY, int tileCost) =>
		{
			var hash = tileY * 10000 + tileX;

			if (!visitedTiles.Add(hash))
			{
				return;
			}

			var y = Math.DivRem(tileY, size);
			var x = Math.DivRem(tileX, size);

			var cost = (map[y.Remainder * size + x.Remainder] + y.Quotient + x.Quotient - 1) % 9 + 1;
			cost += tileCost;

			activeTiles.Enqueue(new Tile(tileX, tileY, cost), cost);
		};

		while (true)
		{
			var checkTile = activeTiles.Dequeue();

			if (checkTile.X == finish && checkTile.Y == finish)
			{
				return checkTile.Cost;
			}

			if (checkTile.Y > 0)
			{
				Enqueue(checkTile.X, checkTile.Y - 1, checkTile.Cost);
			}

			if (checkTile.Y < finish)
			{
				Enqueue(checkTile.X, checkTile.Y + 1, checkTile.Cost);
			}

			if (checkTile.X > 0)
			{
				Enqueue(checkTile.X - 1, checkTile.Y, checkTile.Cost);
			}

			if (checkTile.X < finish)
			{
				Enqueue(checkTile.X + 1, checkTile.Y, checkTile.Cost);
			}
		}
	}
}
