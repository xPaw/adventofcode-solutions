using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(17)]
public class Day17 : IAnswer
{
	public Solution Solve(string input)
	{
		var grid = new ReadOnlyGrid(input);

		var part1 = Solve(grid, 0, 3);
		var part2 = Solve(grid, 3, 10);

		return new(part1.ToString(), part2.ToString());
	}

	record struct Vector2i(int X, int Y);
	record struct NextTile(Vector2i Position, Vector2i Delta, int Forward, int Cost);

	private int Solve(ReadOnlyGrid grid, int minForward, int maxForward)
	{
		var visitedTiles = new HashSet<(int, int, int, int, int)>();
		var activeTiles = new PriorityQueue<NextTile, int>();

		activeTiles.Enqueue(new(new(0, 0), new(1, 0), 0, 0), 0);
		activeTiles.Enqueue(new(new(0, 0), new(0, 1), 0, 0), 0);

		void Enqueue(Vector2i tile, Vector2i delta, int forward, int tileCost)
		{
			tile.X += delta.X;
			tile.Y += delta.Y;

			var c = grid[tile.Y, tile.X];

			if (c == '\0')
			{
				return;
			}

			if (!visitedTiles.Add((tile.X, tile.Y, delta.X, delta.Y, forward)))
			{
				return;
			}

			var nextCost = tileCost + c - '0';

			activeTiles.Enqueue(new(tile, delta, forward, nextCost), nextCost);
		}

		while (true)
		{
			var checkTile = activeTiles.Dequeue();

			if (checkTile.Forward < maxForward)
			{
				Enqueue(checkTile.Position, checkTile.Delta, checkTile.Forward + 1, checkTile.Cost);
			}

			if (checkTile.Forward > minForward)
			{
				if (checkTile.Position.X == grid.Width - 1 && checkTile.Position.Y == grid.Height - 1)
				{
					return checkTile.Cost;
				}

				if (checkTile.Delta.X != 0)
				{
					Enqueue(checkTile.Position, new(0, 1), 1, checkTile.Cost);
					Enqueue(checkTile.Position, new(0, -1), 1, checkTile.Cost);
				}

				if (checkTile.Delta.Y != 0)
				{
					Enqueue(checkTile.Position, new(1, 0), 1, checkTile.Cost);
					Enqueue(checkTile.Position, new(-1, 0), 1, checkTile.Cost);
				}
			}
		}
	}
}
