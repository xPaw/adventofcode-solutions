using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(9)]
class Day9 : IAnswer
{
	readonly List<(int x, int y)> Directions = new()
	{
		(0, -1),
		(0, 1),
		(-1, 0),
		(1, 0),
	};
	int MaxY;
	int MaxX;

	public (string Part1, string Part2) Solve(string input)
	{
		var locations = input
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

		MaxX = locations.Length - 1;
		MaxY = locations[0].Length - 1;

		var part1 = 0;
		var basins = new List<int>();
		var basinTemp = new bool[MaxX * MaxY + MaxY + 1];

		for (var x = 0; x <= MaxX; x++)
		{
			for (var y = 0; y <= MaxY; y++)
			{
				var n = locations[x][y];
				var good = true;

				foreach (var (dx, dy) in Directions)
				{
					var x2 = x + dx;
					var y2 = y + dy;

					if (y2 < 0 || x2 < 0 || y2 > MaxY || x2 > MaxX)
					{
						continue;
					}

					if (locations[x2][y2] <= n)
					{
						good = false;
						break;
					}
				}

				if (good)
				{
					part1 += 1 + n;

					basins.Add(GetBasin(basinTemp, locations, x, y));
					Array.Clear(basinTemp, 0, basinTemp.Length);
				}
			}
		}

		basins.Sort((a, b) => b.CompareTo(a));

		var part2 = basins[0] * basins[1] * basins[2];

		return (part1.ToString(), part2.ToString());
	}

	int GetBasin(bool[] basin, int[][] locations, int x, int y)
	{
		var count = 1;
		basin[x * MaxY + y] = true;

		var queue = new Queue<(int x, int y)>();
		queue.Enqueue((x, y));

		while (queue.Count > 0)
		{
			var p = queue.Dequeue();

			foreach (var (dx, dy) in Directions)
			{
				var x2 = p.x + dx;
				var y2 = p.y + dy;

				if (y2 < 0 || x2 < 0 || y2 > MaxY || x2 > MaxX)
				{
					continue;
				}

				if (!basin[x2 * MaxY + y2] && locations[x][y] < locations[x2][y2] && locations[x2][y2] < 9)
				{
					queue.Enqueue((x2, y2));
					basin[x2 * MaxY + y2] = true;
					count++;
				}
			}
		}

		return count;
	}
}
