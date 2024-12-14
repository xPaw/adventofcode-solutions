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

			robots.Add(new Robot
			{
				Position = new(px, py),
				Velocity = new(vx, vy),
			});

			px += vx * seconds;
			px = Mod(px, gridSize.X);

			py += vy * seconds;
			py = Mod(py, gridSize.Y);

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
		var bestX = FindBestVariance(robots, gridSize.X, isX: true);
		var bestY = FindBestVariance(robots, gridSize.Y, isX: false);
		var modInverseX = Mod(ExtendedGcd(gridSize.X, gridSize.Y).x, gridSize.Y);
		var diff = Mod(bestY - bestX, gridSize.Y);
		var part2 = Mod(bestX + modInverseX * diff * gridSize.X, gridSize.X * gridSize.Y);

		return new(part1.ToString(), part2.ToString());
	}

	static int FindBestVariance(List<Robot> robots, int size, bool isX)
	{
		var bestT = 0;
		var minVariance = long.MaxValue;

		for (var time = 0; time < size; time++)
		{
			var sum = 0L;
			var sumOfSquares = 0L;

			foreach (var robot in robots)
			{
				int position;

				if (isX)
				{
					position = Mod(robot.Position.X + time * robot.Velocity.X, size);
				}
				else
				{
					position = Mod(robot.Position.Y + time * robot.Velocity.Y, size);
				}

				sum += position;
				sumOfSquares += position * position;
			}

			var variance = robots.Count * sumOfSquares - sum * sum;

			if (variance < minVariance)
			{
				minVariance = variance;
				bestT = time;
			}
		}

		return bestT;
	}

	static (int gcd, int x, int y) ExtendedGcd(int a, int b)
	{
		if (b == 0)
		{
			return (a, 1, 0);
		}

		var (gcd, x1, y1) = ExtendedGcd(b, a % b);
		return (gcd, y1, x1 - (a / b) * y1);
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
