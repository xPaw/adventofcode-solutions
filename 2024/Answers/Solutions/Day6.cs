using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(6)]
public class Day6 : IAnswer
{
	readonly static (int x, int y)[] Directions =
	[
		(0, -1), // up
		(1, 0), // right
		(0, 1), // down
		(-1, 0), // left
	];

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var grid = new ReadOnlyGrid(input);
		var visited = new HashSet<int>(1024 * 7);
		var simulatedPath = new List<(int X, int Y, int Facing)>(visited.Capacity);
		var (startY, startX) = grid.IndexOf('^');

		{
			var (y, x) = (startY, startX);
			var facing = 0;

			while (part1 == 0)
			{
				if (visited.Add(x * 100_000 + y))
				{
					simulatedPath.Add((x, y, facing));
				}

				var (dx, dy) = Directions[facing];

				switch (grid[y + dy, x + dx])
				{
					case '\0':
						part1 = visited.Count;
						break;
					case '#':
						facing = (facing + 1) % 4;
						break;
					default:
						y += dy;
						x += dx;
						continue;
				}
			}
		}

		for (var i = 0; i < simulatedPath.Count - 1; i++)
		{
			visited.Clear();

			var running = true;
			var (obstacleX, obstacleY, _) = simulatedPath[i + 1];
			var (x, y, facing) = simulatedPath[i];

#if false
			for (var j = 0; j < i; j++)
			{
				var simulated = simulatedPath[j];
				visited.Add(simulated.Facing * 10_000_000 + simulated.X * 10_000 + simulated.Y);
			}
#endif

			do
			{
				if (!visited.Add(facing * 10_000_000 + x * 10_000 + y))
				{
					part2++;
					break;
				}

				var (dx, dy) = Directions[facing];
				var newX = x + dx;
				var newY = y + dy;

				if (newY == obstacleY && newX == obstacleX)
				{
					facing = (facing + 1) % 4;
					continue;
				}

				switch (grid[newY, newX])
				{
					case '\0':
						running = false;
						break;
					case '#':
						facing = (facing + 1) % 4;
						break;
					default:
						y = newY;
						x = newX;
						continue;
				}
			}
			while (running);
		}

		return new(part1.ToString(), part2.ToString());
	}
}
