using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System;

namespace AdventOfCode;

[Answer(17)]
public class Day17 : IAnswer
{
	readonly Vector2[][] Rocks = new[]
	{
		new[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) },
		new[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 2), new Vector2(2, 1) },
		new[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, 1), new Vector2(2, 2) },
		new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(0, 3) },
		new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) },
	};

	public (string Part1, string Part2) Solve(string input)
	{
		var chamber = new HashSet<int>(20480);
		var cache = new Dictionary<int, (int Iteration, double Height)>(5120);
		var left = new Vector2(-1, 0);
		var right = new Vector2(1, 0);
		var down = new Vector2(0, -1);

		var part1 = 0;
		var part2 = 0L;
		var jetIndex = 0;
		var rockIndex = 0;
		var height = 0f;

		static int HashPoint(Vector2 point) => (int)(point.Y * 10 + point.X);

		bool CanMove(Vector2 point)
		{
			if (point.X >= 7 || point.X < 0)
			{
				return false;
			}

			if (point.Y < 1)
			{
				return false;
			}

			return !chamber.Contains(HashPoint(point));
		}

		var iteration = 0;

		do
		{
			var start = new Vector2(2f, height + 4f);
			var rock = Rocks[rockIndex++];
			rockIndex %= Rocks.Length;

			while (true)
			{
				var jet = input[jetIndex++] == '<' ? left : right;
				jetIndex %= input.Length;

				if (rock.All(point => CanMove(start + point + jet)))
				{
					start += jet;
				}

				if (!rock.All(point => CanMove(start + point + down)))
				{
					break;
				}

				start += down;
			}

			foreach (var point in rock)
			{
				var newPoint = start + point;

				chamber.Add(HashPoint(newPoint));

				if (height < newPoint.Y)
				{
					height = newPoint.Y;
				}
			}

			iteration++;

			if (iteration == 2022)
			{
				part1 = (int)height;

				if (part2 > 0)
				{
					break;
				}
			}

			var hash = rockIndex * 100_000 + jetIndex;

			if (cache.TryGetValue(hash, out var cachedValue))
			{
				var (repetitions, remainder) = Math.DivRem(
					1_000_000_000_000 - iteration,
					cachedValue.Iteration - iteration
				);

				if (remainder == 0)
				{
					part2 = (long)(height + (cachedValue.Height - height) * repetitions);

					if (part1 > 0)
					{
						break;
					}
				}
			}

			cache[hash] = (iteration, height);
		}
		while (true);

		return (part1.ToString(), part2.ToString());
	}
}
