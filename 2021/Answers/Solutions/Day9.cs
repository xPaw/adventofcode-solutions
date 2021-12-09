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

		MaxX = locations.Length - 1;
		MaxY = locations[0].Length - 1;

		var part1 = 0;
		var basins = new List<int>();

		for (var x = 0; x <= MaxX; x++)
		{
			for (var y = 0; y <= MaxY; y++)
			{
				var n = locations[x][y];
				var good = true;

				foreach (var p in GetNeighbors(x, y))
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

					basins.Add(GetBasin(locations, x, y));
				}
			}
		}

		basins.Sort((a, b) => b.CompareTo(a));

		var part2 = basins[0] * basins[1] * basins[2];

		return (part1.ToString(), part2.ToString());
	}

	int GetBasin(int[][] locations, int x, int y)
	{
		var basin = new HashSet<int>
		{
			x * MaxY + y
		};
		var queue = new Queue<(int x, int y)>();
		queue.Enqueue((x, y));

		while (queue.Count > 0)
		{
			var p = queue.Dequeue();

			foreach (var (x2, y2) in GetNeighbors(p.x, p.y))
			{
				if (!basin.Contains(x2 * MaxY + y2) && locations[x][y] < locations[x2][y2] && locations[x2][y2] < 9)
				{
					queue.Enqueue((x2, y2));
					basin.Add(x2 * MaxY + y2);
				}
			}
		}

		return basin.Count;
	}

	IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
	{
		if (y > 0)
		{
			yield return (x, y - 1);
		}

		if (y < MaxY)
		{
			yield return (x, y + 1);
		}

		if (x > 0)
		{
			yield return (x - 1, y);
		}

		if (x < MaxX)
		{
			yield return (x + 1, y);
		}
	}
}
