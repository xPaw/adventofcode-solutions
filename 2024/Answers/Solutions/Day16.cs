using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(16)]
public class Day16 : IAnswer
{
	readonly static (int x, int y)[] Directions =
	[
		(1, 0), // right
		(0, 1), // down
		(-1, 0), // left
		(0, -1), // up
	];

	record struct Tile(int X, int Y, int Facing, HashSet<int> Path);

	public Solution Solve(string input)
	{
		var part1 = int.MaxValue;
		var grid = new ReadOnlyGrid(input, '#');
		var start = grid.IndexOf('S');
		var finish = grid.IndexOf('E');

		var activeTiles = new PriorityQueue<Tile, int>(1024);
		var visitedTiles = new Dictionary<int, int>(1024 * 40);
		var bestPaths = new HashSet<int>(1024);

		Enqueue(grid, start.X, start.Y, 0, [], 0);

		void Enqueue(ReadOnlyGrid grid, int tileX, int tileY, int facing, HashSet<int> path, int cost)
		{
			var hash = tileY * grid.Width + tileX;
			var visitedHash = facing * 10000000 + hash;

			if (visitedTiles.GetValueOrDefault(visitedHash, int.MaxValue) < cost)
			{
				return;
			}

			visitedTiles[visitedHash] = cost;

			if (grid[tileY, tileX] == '#')
			{
				return;
			}

			if (path.Contains(hash))
			{
				return;
			}

			var newPath = new HashSet<int>(path)
			{
				hash
			};

			activeTiles.Enqueue(new Tile(tileX, tileY, facing, newPath), cost);
		}

		while (activeTiles.TryDequeue(out var checkTile, out var cost))
		{
			if (cost > part1)
			{
				break;
			}

			if (checkTile.X == finish.X && checkTile.Y == finish.Y)
			{
				if (part1 == int.MaxValue)
				{
					part1 = cost;
				}

				bestPaths.UnionWith(checkTile.Path);

				continue;
			}

			var facing = checkTile.Facing;
			var (dx, dy) = Directions[facing];

			Enqueue(grid, checkTile.X + dx, checkTile.Y + dy, facing, checkTile.Path, cost + 1);

			facing = (checkTile.Facing + 1) % 4;
			(dx, dy) = Directions[facing];
			Enqueue(grid, checkTile.X + dx, checkTile.Y + dy, facing, checkTile.Path, cost + 1001);

			facing = checkTile.Facing == 0 ? 3 : checkTile.Facing - 1;
			(dx, dy) = Directions[facing];
			Enqueue(grid, checkTile.X + dx, checkTile.Y + dy, facing, checkTile.Path, cost + 1001);
		}

		var part2 = bestPaths.Count;

		return new(part1.ToString(), part2.ToString());
	}
}
