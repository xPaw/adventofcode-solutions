using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(8)]
public class Day8 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;
		var junctions = new List<(int X, int Y, int Z)>();
		var connections = new List<(long Distance, int Left, int Right)>();

		foreach (var line in input.EnumerateLines())
		{
			var coords = new int[3];
			var i = 0;

			foreach (var number in line.Split(','))
			{
				coords[i++] = line[number].ParseInt();
			}

			junctions.Add((coords[0], coords[1], coords[2]));
		}

		for (var i = 0; i < junctions.Count; i++)
		{
			var left = junctions[i];

			for (var j = i + 1; j < junctions.Count; j++)
			{
				var right = junctions[j];
				long dx = left.X - right.X;
				long dy = left.Y - right.Y;
				long dz = left.Z - right.Z;
				connections.Add((dx * dx + dy * dy + dz * dz, i, j));
			}
		}

		connections.Sort((a, b) => a.Distance.CompareTo(b.Distance));

		var numCircuits = junctions.Count;
		var maxConnections = numCircuits == 20 ? 10 : 1000;
		var circuit = new int[numCircuits];
		var circuitSize = new int[numCircuits];

		for (var i = 0; i < numCircuits; i++)
		{
			circuit[i] = i;
			circuitSize[i] = 1;
		}

		for (var i = 0; i < connections.Count; i++)
		{
			var (_, left, right) = connections[i];
			var circuit1 = circuit[left];
			var circuit2 = circuit[right];

			if (circuit1 != circuit2)
			{
				for (var j = 0; j < junctions.Count; j++)
				{
					if (circuit[j] == circuit2)
					{
						circuit[j] = circuit1;
					}
				}

				circuitSize[circuit1] += circuitSize[circuit2];
				circuitSize[circuit2] = 0;

				if (--numCircuits == 1)
				{
					part2 = (long)junctions[left].X * junctions[right].X;
					break;
				}
			}

			if (i + 1 == maxConnections)
			{
				var sizes = new List<int>(junctions.Count);

				for (var j = 0; j < junctions.Count; j++)
				{
					if (circuitSize[j] > 0)
					{
						sizes.Add(circuitSize[j]);
					}
				}

				sizes.Sort();
				part1 = (long)sizes[^3] * sizes[^2] * sizes[^1];
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
