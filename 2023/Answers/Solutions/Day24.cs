using System;
using System.Collections.Generic;
using System.Linq;
using Z3 = Microsoft.Z3;

namespace AdventOfCode;

[Answer(24)]
public class Day24 : IAnswer
{
	record struct Vector3l(long X, long Y, long Z);
	record class Hailstone(Vector3l Position, Vector3l Velocity);

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

		if (input.Length < 200)
		{
			min = 7;
			max = 27;
		}

		for (var i = 0; i < stones.Count; i++)
		{
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

		var part2 = SolveZ3(stones);

		return new(part1.ToString(), part2.ToString());
	}

	private long SolveZ3(List<Hailstone> stones)
	{
		using var ctx = new Z3.Context();
		using var solver = ctx.MkSolver();

		var x = ctx.MkRealConst("x");
		var y = ctx.MkRealConst("y");
		var z = ctx.MkRealConst("z");

		var vx = ctx.MkRealConst("vx");
		var vy = ctx.MkRealConst("vy");
		var vz = ctx.MkRealConst("vz");

		for (var i = 0; i < stones.Count; i++)
		{
			var hail = stones[i];
			var t = ctx.MkRealConst($"t{i}");

			solver.Assert(ctx.MkEq(x + vx * t, ctx.MkReal(hail.Position.X) + ctx.MkReal(hail.Velocity.X) * t));
			solver.Assert(ctx.MkEq(y + vy * t, ctx.MkReal(hail.Position.Y) + ctx.MkReal(hail.Velocity.Y) * t));
			solver.Assert(ctx.MkEq(z + vz * t, ctx.MkReal(hail.Position.Z) + ctx.MkReal(hail.Velocity.Z) * t));
		}

		solver.Check();

		var xl = (long)(solver.Model.Evaluate(x) as Z3.RatNum)!.BigIntNumerator;
		var yl = (long)(solver.Model.Evaluate(y) as Z3.RatNum)!.BigIntNumerator;
		var zl = (long)(solver.Model.Evaluate(z) as Z3.RatNum)!.BigIntNumerator;

		return xl + yl + zl;
	}
}
