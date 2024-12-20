using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(6)]
public class Day6 : IAnswer
{
	record struct Position(int X, int Y, int Facing);

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
		var simulatedPath = new List<Position>(visited.Capacity);
		var (startX, startY) = grid.IndexOf('^');

		{
			var (y, x) = (startY, startX);
			var facing = 0;

			while (part1 == 0)
			{
				if (visited.Add(x * 100_000 + y))
				{
					simulatedPath.Add(new(x, y, facing));
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
			var (x, y, facing) = simulatedPath[i];
			var (obstacleX, obstacleY, _) = simulatedPath[i + 1];

			do
			{
				if (!visited.Add(facing * 10_000_000 + x * 10_000 + y))
				{
					part2++;
					break;
				}

				var (dx, dy) = Directions[facing];
				var turned = false;

				do
				{
					var newX = x + dx;
					var newY = y + dy;

					if (newY == obstacleY && newX == obstacleX)
					{
						facing = (facing + 1) % 4;
						break;
					}

					switch (grid[newY, newX])
					{
						case '\0':
							running = false;
							turned = true;
							break;
						case '#':
							facing = (facing + 1) % 4;
							turned = true;
							break;
						default:
							y = newY;
							x = newX;
							break;
					}
				}
				while (!turned);
			}
			while (running);
		}

		return new(part1.ToString(), part2.ToString());
	}
}
