using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(15)]
class Day15 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var size = input.IndexOf('\n');
		var map = input
			.Split('\n')
			.Select(l =>
			{
				var numbers = new int[l.Length];

				for (var i = 0; i < numbers.Length; i++)
				{
					numbers[i] = l[i] - '0';
				}

				return numbers;
			})
			.ToArray();


		var part1 = Solve(map, size);

		var bigMap = new int[size * 5][];

		for (var y = 0; y < size * 5; y++)
		{
			bigMap[y] = new int[size * 5];
		}

		for (var y = 0; y < size; y++)
		{
			for (var x = 0; x < size; x++)
			{
				bigMap[y][x] = map[y][x];
			}
		}

		for (var i = 1; i < 5; i++)
		{
			for (var y = 0; y < size; y++)
			{
				for (var x = i * size; x < size * 5; x++)
				{
					var n = bigMap[y][x - size] + 1;

					if (n > 9)
					{
						n = 1;
					}

					bigMap[y][x] = n;
				}
			}

			for (var y = i * size; y < size * 5; y++)
			{
				for (var x = 0; x < size * 5; x++)
				{
					var n = bigMap[y - size][x] + 1;

					if (n > 9)
					{
						n = 1;
					}

					bigMap[y][x] = n;
				}
			}
		}

		var part2 = Solve(bigMap, size * 5);

		return (part1.ToString(), part2.ToString());
	}

	private int Solve(int[][] map, int size)
	{
		var answer = 0;
		var start = new Tile
		{
			Y = 0,
			X = 0,
		};
		var visitedTiles = new HashSet<int>();
		var activeTiles = new Dictionary<int, Tile>
		{
			{ 0, start },
		};

		while (true)
		{
			var checkTile = start;
			var cost = int.MaxValue;

			foreach (var tile in activeTiles.Values)
			{
				if (cost > tile.Cost)
				{
					checkTile = tile;
					cost = tile.Cost;
				}
			}

			if (checkTile.X == size - 1 && checkTile.Y == size - 1)
			{
				while (checkTile.Y > 0 || checkTile.X > 0)
				{
					answer += map[checkTile.Y][checkTile.X];
					checkTile = checkTile.Parent!;
				}

				break;
			}

			var hash = checkTile.Y * size + checkTile.X;
			visitedTiles.Add(hash);
			activeTiles.Remove(hash);

			var walkableTiles = new List<Tile>();

			if (checkTile.Y > 0)
			{
				walkableTiles.Add(new Tile { X = checkTile.X, Y = checkTile.Y - 1, Parent = checkTile, Cost = checkTile.Cost });
			}

			if (checkTile.Y < size - 1)
			{
				walkableTiles.Add(new Tile { X = checkTile.X, Y = checkTile.Y + 1, Parent = checkTile, Cost = checkTile.Cost });
			}

			if (checkTile.X > 0)
			{
				walkableTiles.Add(new Tile { X = checkTile.X - 1, Y = checkTile.Y, Parent = checkTile, Cost = checkTile.Cost });
			}

			if (checkTile.X < size - 1)
			{
				walkableTiles.Add(new Tile { X = checkTile.X + 1, Y = checkTile.Y, Parent = checkTile, Cost = checkTile.Cost });
			}

			foreach (var walkableTile in walkableTiles)
			{
				hash = walkableTile.Y * size + walkableTile.X;

				if (visitedTiles.Contains(hash))
				{
					continue;
				}

				walkableTile.Cost += map[walkableTile.Y][walkableTile.X];

				if (!activeTiles.ContainsKey(hash))
				{
					activeTiles[hash] = walkableTile;
				}
			}
		}

		return answer;
	}

	class Tile
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Cost { get; set; }
		public Tile? Parent { get; set; }
	}
}
