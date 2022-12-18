using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode;

[Answer(18)]
public class Day18 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var i = 0;
		var length = input.Length;
		var part1 = 0;
		var part2 = 0;

		int ParseIntUntil(char c)
		{
			var result = 0;

			do
			{
				var t = input[i++];

				if (t == c)
				{
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < length);

			return result;
		}

		static IEnumerable<Vector3> GetAdjecentCubes(Vector3 cube)
		{
			yield return cube + Vector3.UnitX;
			yield return cube + Vector3.UnitY;
			yield return cube + Vector3.UnitZ;
			yield return cube - Vector3.UnitX;
			yield return cube - Vector3.UnitY;
			yield return cube - Vector3.UnitZ;
		}

		var cubes = new HashSet<Vector3>(3072);
		var seen = new HashSet<Vector3>(10240);
		var queue = new Queue<Vector3>(64);
		var max = int.MinValue;

		while (i < length)
		{
			var a = ParseIntUntil(',');
			var b = ParseIntUntil(',');
			var c = ParseIntUntil('\n');

			if (max < a) max = a;
			if (max < b) max = b;
			if (max < c) max = c;

			cubes.Add(new Vector3(a, b, c));
		}

		max++;

		bool ValidCube(Vector3 cube)
		{
			if (cube.X > max || cube.X < -1) return false;
			if (cube.Y > max || cube.Y < -1) return false;
			if (cube.Z > max || cube.Z < -1) return false;

			return true;
		}

		queue.Enqueue(new Vector3(max, max, max));

		while (queue.TryDequeue(out var cube))
		{
			foreach (var adjecentCube in GetAdjecentCubes(cube))
			{
				if (!seen.Contains(adjecentCube) && !cubes.Contains(adjecentCube) && ValidCube(adjecentCube))
				{
					seen.Add(adjecentCube);
					queue.Enqueue(adjecentCube);
				}
			}
		}

		foreach (var cube in cubes)
		{
			foreach (var adjecentCube in GetAdjecentCubes(cube))
			{
				if (!cubes.Contains(adjecentCube))
				{
					part1++;
				}

				if (seen.Contains(adjecentCube))
				{
					part2++;
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
