using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(22, slow: true)]
class Day22 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n');
		var grid = new HashSet<int>();
		var cubes = new Dictionary<(int, int, int, int, int, int), int>();

		foreach (var line in lines)
		{
			var turnOn = line[1] == 'n';
			var split = line.Split(new char[] {
				'.',
				'=',
				',',
			});
			var x1 = int.Parse(split[1]);
			var x2 = int.Parse(split[3]);
			var y1 = int.Parse(split[5]);
			var y2 = int.Parse(split[7]);
			var z1 = int.Parse(split[9]);
			var z2 = int.Parse(split[11]);

			for (var x = Math.Max(x1, -50); x <= x2; x++)
			{
				if (x < -50 || x > 50) continue;

				for (var y = Math.Max(y1, -50); y <= y2; y++)
				{
					if (y < -50 || y > 50) continue;

					for (var z = Math.Max(z1, -50); z <= z2; z++)
					{
						if (z < -50 || z > 50) continue;

						var hash = x * 10000000 + y * 100000 + z;

						if (turnOn)
						{
							grid.Add(hash);
						}
						else
						{
							grid.Remove(hash);
						}
					}
				}
			}

			//
			var add = new Dictionary<(int, int, int, int, int, int), int>();

			foreach (var (key, count) in cubes)
			{
				var x3 = Math.Max(x1, key.Item1);
				var x4 = Math.Min(x2, key.Item2);
				var y3 = Math.Max(y1, key.Item3);
				var y4 = Math.Min(y2, key.Item4);
				var z3 = Math.Max(z1, key.Item5);
				var z4 = Math.Min(z2, key.Item6);

				if (x3 <= x4 && y3 <= y4 && z3 <= z4)
				{
					var overlapKey = (x3, x4, y3, y4, z3, z4);
					add[overlapKey] = add.GetValueOrDefault(overlapKey) - count;
				}
			}

			if (turnOn)
			{
				var key = (x1, x2, y1, y2, z1, z2);
				add[key] = add.GetValueOrDefault(key) + 1;
			}

			foreach (var (key, count) in add)
			{
				var value = cubes.GetValueOrDefault(key) + count;

				if (value == 0)
				{
					cubes.Remove(key);
					continue;
				}

				cubes[key] = value;
			}
		}

		var part1 = grid.Count;
		long part2 = 0;

		foreach (var (key, count) in cubes)
		{
			part2 += (key.Item2 - key.Item1 + 1L) * (key.Item4 - key.Item3 + 1L) * (key.Item6 - key.Item5 + 1L) * count;
		}

		return (part1.ToString(), part2.ToString());
	}
}
