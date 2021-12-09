using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(9)]
class Day9 : IAnswer
{
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

		MaxY = locations.Length - 1;
		MaxX = locations[0].Length - 1;

		var part1 = 0;
		var basins = new List<int>();

		for (var y = 0; y <= MaxY; y++)
		{
			for (var x = 0; x <= MaxX; x++)
			{
				var n = locations[y][x];
				var good = true;

				foreach (var p in GetNeighbors(y, x))
				{
					if (locations[p.x][p.y] <= n)
					{
						good = false;
						break;
					}
				}

				if (good)
				{
					part1 += 1 + n;

					var basin = new HashSet<int>();

					foreach (var coord in GetBasin(basin, locations, y, x))
					{
						basin.Add(coord.x * MaxX + coord.y);
					}

					basins.Add(basin.Count);
				}
			}
		}

		basins.Sort((a, b) => b.CompareTo(a));

		var part2 = basins[0] * basins[1] * basins[2];

		return (part1.ToString(), part2.ToString());
	}

	IEnumerable<(int x, int y)> GetBasin(HashSet<int> basin, int[][] locations, int x, int y)
	{
		yield return (x, y);

		foreach (var (x2, y2) in GetNeighbors(x, y))
		{
			if (!basin.Contains(x2 * MaxX + y2) && locations[x][y] < locations[x2][y2] && locations[x2][y2] < 9)
			{
				foreach (var t in GetBasin(basin, locations, x2, y2))
				{
					yield return t;
				}
			}
		}
	}

	IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
	{
		if (y > 0)
		{
			yield return (x, y - 1);
		}

		if (y < MaxX)
		{
			yield return (x, y + 1);
		}

		if (x > 0)
		{
			yield return (x - 1, y);
		}

		if (x < MaxY)
		{
			yield return (x + 1, y);
		}
	}
}
