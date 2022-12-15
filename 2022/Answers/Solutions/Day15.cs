using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(15)]
public class Day15 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var i = 0;
		var length = input.Length;
		var sensors = new List<(int X, int Y, int Manhattan)>();

		int ParseInt()
		{
			var result = 0;
			var sign = 1;

			while (true)
			{
				var t = input[++i];

				if (char.IsAsciiDigit(t))
				{
					break;
				}

				if (t == '-')
				{
					sign = -1;
					i++;
					break;
				}
			}

			do
			{
				var t = input[i++];

				if (!char.IsAsciiDigit(t))
				{
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < length);

			result *= sign;

			return result;
		}

		var wantedY = input.Length > 1000 ? 2_000_000 : 10;
		var max2 = input.Length > 1000 ? 4_000_000 : 20;
		var minX = int.MaxValue;
		var maxX = int.MinValue;

		while (i < length)
		{
			var sensorX = ParseInt();
			var sensorY = ParseInt();
			var beaconX = ParseInt();
			var beaconY = ParseInt();

			var dY = Math.Abs(sensorY - wantedY);
			var mX = Math.Abs(sensorX - beaconX);
			var mY = Math.Abs(sensorY - beaconY);
			var manhattan = mX + mY;

			sensors.Add((sensorX, sensorY, manhattan));

			if (manhattan > dY)
			{
				manhattan -= dY;
				minX = Math.Min(minX, sensorX - manhattan);
				maxX = Math.Max(maxX, sensorX + manhattan);
			}
		}

		var part2 = 0ul;

		for (var id = 0; id < sensors.Count; id++)
		{
			var (sensorX, sensorY, manhattan) = sensors[id];
			var sortedSensors = sensors
				.Where((s, i) => i != id)
				.OrderBy(s => Math.Abs(sensorX - s.X) + Math.Abs(sensorY - s.Y))
				.ToList();

			for (var signX = -1; signX <= 1; signX += 2)
			{
				for (var signY = -1; signY <= 1; signY += 2)
				{
					for (var width = 0; width <= manhattan + 1; width++)
					{
						var height = manhattan + 1 - width;
						var x = sensorX + width * signX;
						var y = sensorY + height * signY;

						if (x < 0 || y < 0 || x > max2 || y > max2)
						{
							break;
						}

						var noOverlaps = true;

						foreach (var (sensorX2, sensorY2, manhattan2) in sortedSensors)
						{
							var manhattanPoint = Math.Abs(sensorX2 - x) + Math.Abs(sensorY2 - y);

							if (manhattanPoint <= manhattan2)
							{
								noOverlaps = false;
								break;
							}
						}

						if (noOverlaps)
						{
							part2 = (ulong)x * 4_000_000ul + (ulong)y;
							goto stop;
						}
					}
				}
			}
		}

	stop:
		var part1 = Math.Abs(maxX - minX);

		return (part1.ToString(), part2.ToString());
	}
}
