using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(24)]
public class Day24 : IAnswer
{
	record struct Vector3l(long X, long Y, long Z);
	record struct Hailstone(Vector3l Position, Vector3l Velocity);

	public Solution Solve(string input)
	{
		var part1 = 0;
		var stones = new List<Hailstone>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var coords = line.ToString().Replace(" ", "").Split('@');
			var start = coords[0].Split(',').Select(long.Parse).ToArray();
			var end = coords[1].Split(',').Select(long.Parse).ToArray();

			stones.Add(new Hailstone(
				new Vector3l(start[0], start[1], start[2]),
				new Vector3l(end[0], end[1], end[2])
			));
		}

		var min = 200_000_000_000_000;
		var max = 400_000_000_000_000;
		var maxVelocity = 0L;

		if (input.Length < 200)
		{
			min = 7;
			max = 27;
		}

		for (var i = 0; i < stones.Count; i++)
		{
			if (i <= 3)
			{
				maxVelocity = Math.Max(maxVelocity, Math.Abs(stones[i].Velocity.X));
				maxVelocity = Math.Max(maxVelocity, Math.Abs(stones[i].Velocity.Y));
				maxVelocity = Math.Max(maxVelocity, Math.Abs(stones[i].Velocity.Z));
			}

			for (var j = i + 1; j < stones.Count; j++)
			{
				var a = stones[i];
				var b = stones[j];

				var x1 = a.Position.X;
				var x2 = a.Velocity.X + a.Position.X;
				var x3 = b.Position.X;
				var x4 = b.Velocity.X + b.Position.X;

				var y1 = a.Position.Y;
				var y2 = a.Velocity.Y + a.Position.Y;
				var y3 = b.Position.Y;
				var y4 = b.Velocity.Y + b.Position.Y;

				var m1 = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
				var m2 = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / (double)m1;

				var x = x1 + m2 * (x2 - x1);
				var y = y1 + m2 * (y2 - y1);

				if (
					x >= min &&
					x <= max &&
					y >= min &&
					y <= max &&
					x > x1 == a.Velocity.X > 0 &&
					x > x3 == b.Velocity.X > 0 &&
					y > y1 == a.Velocity.Y > 0 &&
					y > y3 == b.Velocity.Y > 0
				)
				{
					part1++;
				}
			}
		}

		var part2 = SolvePart2(stones, maxVelocity);

		return new(part1.ToString(), part2.ToString());
	}

	// Taken from https://pastebin.com/B0LijzNb
	private long SolvePart2(List<Hailstone> stones, long range)
	{
		foreach (var x in Range(range))
		{
			foreach (var y in Range(range))
			{
				var intersect1 = TryIntersectPos(stones[1], stones[0], new(x, y, 0));
				var intersect2 = TryIntersectPos(stones[2], stones[0], new(x, y, 0));

				if (!intersect1.Intersects || intersect1.Position != intersect2.Position)
				{
					continue;
				}

				foreach (var z in Range(range))
				{
					var intersectZ1 = stones[1].Position.Z + intersect1.Time * (stones[1].Velocity.Z + z);
					var intersectZ2 = stones[2].Position.Z + intersect2.Time * (stones[2].Velocity.Z + z);

					if (intersectZ1 != intersectZ2)
					{
						continue;
					}

					return intersect1.Position.X + intersect1.Position.Y + intersectZ1;
				}
			}
		}

		return 0;
	}

	private (bool Intersects, Vector3l Position, long Time) TryIntersectPos(Hailstone one, Hailstone two, Vector3l offset)
	{
		var a = new Hailstone(new(one.Position.X, one.Position.Y, 0), Velocity: new(one.Velocity.X + offset.X, one.Velocity.Y + offset.Y, 0));
		var c = new Hailstone(new(two.Position.X, two.Position.Y, 0), Velocity: new(two.Velocity.X + offset.X, two.Velocity.Y + offset.Y, 0));
		var D = (a.Velocity.X * -1 * c.Velocity.Y) - (a.Velocity.Y * -1 * c.Velocity.X);

		if (D == 0)
		{
			return (false, new(0, 0, 0), 0);
		}

		var Qx = (-1 * c.Velocity.Y * (c.Position.X - a.Position.X)) - (-1 * c.Velocity.X * (c.Position.Y - a.Position.Y));
		var t = Qx / D;

		var Px = a.Position.X + t * a.Velocity.X;
		var Py = a.Position.Y + t * a.Velocity.Y;

		return (true, new(Px, Py, 0), t);
	}

	private IEnumerable<long> Range(long max)
	{
		var i = 0L;
		yield return i;

		while (i < max)
		{
			if (i >= 0)
			{
				i++;
			}

			i *= -1;

			yield return i;
		}
	}
}
