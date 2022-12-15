using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(15)]
public class Day15 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var i = 0;
		var length = input.Length;
		var sensors = new List<(int X, int Y, int Manhattan)>();
		var acoeffs = new HashSet<int>();
		var bcoeffs = new HashSet<int>();

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
				minX = Math.Min(minX, sensorX - (manhattan - dY));
				maxX = Math.Max(maxX, sensorX + (manhattan - dY));
			}

			acoeffs.Add(sensorY - sensorX + manhattan + 1);
			acoeffs.Add(sensorY - sensorX - manhattan - 1);
			bcoeffs.Add(sensorX + sensorY + manhattan + 1);
			bcoeffs.Add(sensorX + sensorY - manhattan - 1);
		}

		var part1 = Math.Abs(maxX - minX);
		var part2 = 0ul;

		foreach (var a in acoeffs)
		{
			foreach (var b in bcoeffs)
			{
				if ((b - a) % 2 == 1)
				{
					continue;
				}

				var x = (b - a) / 2;
				var y = (b + a) / 2;

				if (x < 0 || y < 0 || x > max2 || y > max2)
				{
					continue;
				}

				var noOverlaps = true;

				foreach (var (sensorX, sensorY, manhattan) in sensors)
				{
					var manhattanPoint = Math.Abs(sensorX - x) + Math.Abs(sensorY - y);

					if (manhattanPoint <= manhattan)
					{
						noOverlaps = false;
						break;
					}
				}

				if (noOverlaps)
				{
					part2 = (ulong)x * 4_000_000ul + (ulong)y;

					if (y == wantedY)
					{
						part1--;
					}

					goto stop;
				}
			}
		}

	stop:
		return (part1.ToString(), part2.ToString());
	}
}
