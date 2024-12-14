using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(14)]
public class Day14 : IAnswer
{
	record struct Coord(int X, int Y);

	class Robot
	{
		public Coord Position;
		public Coord Velocity;
	}

	public Solution Solve(string input)
	{
		var span = input.AsSpan();
		var gridSize = input.Length < 1000 ? new Coord(11, 7) : new Coord(101, 103);
		var seconds = 100;
		var robots1 = new Dictionary<int, int>();
		var robots = new List<Robot>();

		foreach (var line in span.EnumerateLines())
		{
			var l = line[2..];
			var px = ParseInt(ref l);
			var py = ParseInt(ref l);
			l = l[2..];
			var vx = ParseInt(ref l);
			var vy = ParseInt(ref l);

			px += vx * seconds;
			px = Mod(px, gridSize.X);

			py += vy * seconds;
			py = Mod(py, gridSize.Y);

			// The tree isn't going to appear under 100 anyway
			robots.Add(new Robot
			{
				Position = new(px, py),
				Velocity = new(vx, vy),
			});


			var hash = py * gridSize.X + px;
			if (robots1.TryGetValue(hash, out var count))
			{
				robots1[hash] = count + 1;
			}
			else
			{
				robots1[hash] = 1;
			}
		}

		// part 1
		var halfX = gridSize.X / 2;
		var halfY = gridSize.Y / 2;
		var a = CountQuadrant(0, halfX, 0, halfY);
		var b = CountQuadrant(halfX + 1, gridSize.X, 0, halfY);
		var c = CountQuadrant(0, halfX, halfY + 1, gridSize.Y);
		var d = CountQuadrant(halfX + 1, gridSize.X, halfY + 1, gridSize.Y);

		var part1 = a * b * c * d;

		int CountQuadrant(int x1, int x2, int y1, int y2)
		{
			var count = 0;

			for (var y = y1; y < y2; y++)
			{
				for (var x = x1; x < x2; x++)
				{
					var hash = y * gridSize.X + x;

					if (robots1.TryGetValue(hash, out var countr))
					{
						count += countr;
					}
				}
			}

			return count;
		}

		if (gridSize.X == 11) // example
		{
			return new(part1.ToString(), "none");
		}

		// part 2
		var part2 = 0;
		var iterations = 100;
		var minTotalDistance = long.MaxValue;
		var maxIterations = gridSize.X * gridSize.Y;

		while (iterations++ <= maxIterations)
		{
			var totalDistance = 0L;

			foreach (var robot in robots)
			{
				robot.Position.X = Mod(robot.Position.X + robot.Velocity.X, gridSize.X);
				robot.Position.Y = Mod(robot.Position.Y + robot.Velocity.Y, gridSize.Y);

				if (minTotalDistance > totalDistance)
				{
					totalDistance += Math.Abs(robot.Position.X - halfX) * Math.Abs(robot.Position.Y - halfY);
				}
			}

			if (minTotalDistance > totalDistance)
			{
				minTotalDistance = totalDistance;
				part2 = iterations;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	static int Mod(int x, int m)
	{
		int r = x % m;
		return r < 0 ? r + m : r;
	}

	static int ParseInt(ref ReadOnlySpan<char> line)
	{
		var result = 0;
		var sign = 1;

		if (line[0] == '-')
		{
			sign = -1;
			line = line[1..];
		}

		while (!line.IsEmpty)
		{
			if (!char.IsAsciiDigit(line[0]))
			{
				line = line[1..];
				break;
			}

			result = 10 * result + line[0] - '0';
			line = line[1..];
		}

		return result * sign;
	}
}
