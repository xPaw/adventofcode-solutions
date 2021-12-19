using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021;

[Answer(19)]
class Day19 : IAnswer
{
	[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
	class Point
	{
		public int X;
		public int Y;
		public int Z;
		public int Index;
		public int[] Distances = new int[30];

		public override string ToString() => $"{X},{Y},{Z}";
	};

	public (string Part1, string Part2) Solve(string input)
	{
		var scanners = input.Split("\n\n")
			.Select(l => l
				.Split('\n')
				.Skip(1)
				.Select((n, i) =>
				{
					var p = n
						.Split(',')
						.Select(n => int.Parse(n))
						.ToArray();

					return new Point
					{
						Index = i,
						X = p[0],
						Y = p[1],
						Z = p[2]
					};
				})
				.ToArray()
			)
			.ToArray();

		for (var x = 0; x < scanners.Length; x++)
		{
			for (var y = 0; y < scanners[x].Length; y++)
			{
				var a = scanners[x][y];

				for (var z = 0; z < y; z++)
				{
					var b = scanners[x][z];

					var dx = Math.Abs(a.X - b.X);
					var dy = Math.Abs(a.Y - b.Y);
					var dz = Math.Abs(a.Z - b.Z);
					var d = dx * dx + dy * dy + dz * dz;

					a.Distances[b.Index] = d;
					b.Distances[a.Index] = d;
				}
			}
		}

		var toBeRotated = scanners.Length - 1;
		var scannerPositions = new Point[scanners.Length];
		scannerPositions[0] = new Point();

		while (toBeRotated > 0)
		{
			for (var x = 0; x < scanners.Length; x++)
			{
				for (var y = 0; y < scanners.Length; y++)
				{
					if (x == y || scannerPositions[x] == null || scannerPositions[y] != null)
					{
						continue;
					}

					var scanner1 = scanners[x];
					var scanner2 = scanners[y];

					var overlap = Intersect(scanner2, scanner1);

					if (!overlap.HasValue)
					{
						continue;
					}

					var a1 = scanner1[overlap.Value.Item1];
					var a2 = scanner1[overlap.Value.Item2];
					var b1 = scanner2[overlap.Value.Item3];
					var b2 = scanner2[overlap.Value.Item4];

					var dx1 = a1.X - a2.X;
					var dy1 = a1.Y - a2.Y;
					var dz1 = a1.Z - a2.Z;

					var dx2 = b2.X - b1.X;
					var dy2 = b2.Y - b1.Y;
					var dz2 = b2.Z - b1.Z;

					var transformX = new Point();
					var transformY = new Point();
					var transformZ = new Point();

					if (dx1 == dx2) transformX.X = 1;
					if (dx1 == -dx2) transformX.X = -1;
					if (dx1 == dy2) transformX.Y = 1;
					if (dx1 == -dy2) transformX.Y = -1;
					if (dx1 == dz2) transformX.Z = 1;
					if (dx1 == -dz2) transformX.Z = -1;

					if (dy1 == dx2) transformY.X = 1;
					if (dy1 == -dx2) transformY.X = -1;
					if (dy1 == dy2) transformY.Y = 1;
					if (dy1 == -dy2) transformY.Y = -1;
					if (dy1 == dz2) transformY.Z = 1;
					if (dy1 == -dz2) transformY.Z = -1;

					if (dz1 == dx2) transformZ.X = 1;
					if (dz1 == -dx2) transformZ.X = -1;
					if (dz1 == dy2) transformZ.Y = 1;
					if (dz1 == -dy2) transformZ.Y = -1;
					if (dz1 == dz2) transformZ.Z = 1;
					if (dz1 == -dz2) transformZ.Z = -1;

					foreach (var signal in scanner2)
					{
						var prevX = signal.X;
						var prevY = signal.Y;
						var prevZ = signal.Z;

						signal.X = prevX * transformX.X + prevY * transformX.Y + prevZ * transformX.Z;
						signal.Y = prevX * transformY.X + prevY * transformY.Y + prevZ * transformY.Z;
						signal.Z = prevX * transformZ.X + prevY * transformZ.Y + prevZ * transformZ.Z;
					}

					var scanner = new Point
					{
						X = a1.X - b2.X,
						Y = a1.Y - b2.Y,
						Z = a1.Z - b2.Z
					};

					foreach (var signal in scanner2)
					{
						signal.X += scanner.X;
						signal.Y += scanner.Y;
						signal.Z += scanner.Z;
					}

					scannerPositions[y] = scanner;
					toBeRotated--;
				}
			}
		}

		var trueBeacons = new HashSet<int>();

		foreach (var beacons in scanners)
		{
			foreach (var beacon in beacons)
			{
				trueBeacons.Add(beacon.X * 100000 + beacon.Y * 10000 + beacon.Z);
			}
		}

		var part1 = trueBeacons.Count;
		var part2 = 0;

		foreach (var a in scannerPositions)
		{
			foreach (var b in scannerPositions)
			{
				var distance = Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);

				if (part2 < distance)
				{
					part2 = distance;
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}

	(int, int, int, int)? Intersect(Point[] scanner1, Point[] scanner2)
	{
		for (var p1 = 0; p1 < scanner1.Length; p1++)
		{
			var a = scanner1[p1];

			for (var p2 = 0; p2 < scanner2.Length; p2++)
			{
				var overlaps = 0;
				var b = scanner2[p2];

				for (var di = 0; di < a.Distances.Length; di++)
				{
					var aDist = a.Distances[di];

					if (aDist > 0)
					{
						var otherIndex = Array.IndexOf(b.Distances, aDist);

						if (otherIndex > -1 && ++overlaps > 10)
						{
							return (p2, otherIndex, di, p1);
						}
					}
				}
			}
		}

		return null;
	}
}
