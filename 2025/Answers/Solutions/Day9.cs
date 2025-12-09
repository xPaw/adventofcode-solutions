using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(9)]
public class Day9 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;
		var coords = new List<(int X, int Y)>();

		foreach (var line in input.EnumerateLines())
		{
			var comma = line.IndexOf(',');
			var x = line[..comma].ParseInt();
			var y = line[(comma + 1)..].ParseInt();

			coords.Add((x, y));
		}

		var horizontalEdges = new List<(int Y, int X1, int X2)>();
		var verticalEdges = new List<(int X, int Y1, int Y2)>();

		for (var i = 0; i < coords.Count; i++)
		{
			var current = coords[i];
			var next = coords[(i + 1) % coords.Count];

			if (current.Y == next.Y)
			{
				var minX = Math.Min(current.X, next.X);
				var maxX = Math.Max(current.X, next.X);
				horizontalEdges.Add((current.Y, minX, maxX));
			}
			else
			{
				var minY = Math.Min(current.Y, next.Y);
				var maxY = Math.Max(current.Y, next.Y);
				verticalEdges.Add((current.X, minY, maxY));
			}
		}

		for (var i = 0; i < coords.Count; i++)
		{
			var left = coords[i];

			for (var j = i + 1; j < coords.Count; j++)
			{
				var right = coords[j];
				var area = (Math.Abs(left.X - right.X) + 1L) * (Math.Abs(left.Y - right.Y) + 1L);

				if (part1 < area)
				{
					part1 = area;
				}

				var minX = Math.Min(left.X, right.X);
				var maxX = Math.Max(left.X, right.X);
				var minY = Math.Min(left.Y, right.Y);
				var maxY = Math.Max(left.Y, right.Y);

				var valid = true;

				foreach (var (y, x1, x2) in horizontalEdges)
				{
					if (y > minY && y < maxY && x1 < maxX && x2 > minX)
					{
						valid = false;
						break;
					}
				}

				if (valid)
				{
					foreach (var (x, y1, y2) in verticalEdges)
					{
						if (x > minX && x < maxX && y1 < maxY && y2 > minY)
						{
							valid = false;
							break;
						}
					}
				}

				if (valid)
				{
					area = (maxX - minX + 1L) * (maxY - minY + 1L);

					if (part2 < area)
					{
						part2 = area;
					}
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
