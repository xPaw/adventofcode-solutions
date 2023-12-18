using System;
using System.Globalization;

namespace AdventOfCode;

[Answer(18)]
public class Day18 : IAnswer
{
	record struct Vector2l(long X, long Y)
	{
		public static Vector2l operator +(Vector2l left, Vector2l right)
			=> new(left.X + right.X, left.Y + right.Y);
	}

	public Solution Solve(string input)
	{
		var pos1 = new Vector2l(0, 0);
		var pos2 = new Vector2l(0, 0);

		var wall1 = 0;
		var wall2 = 0;

		var area1 = 0.0;
		var area2 = 0.0;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			// p1
			var direction = line[0];
			var number = line[2] - '0';

			if (line[3] != ' ')
			{
				number = 10 * number + line[3] - '0';
			}

			var posNew = pos1 + direction switch
			{
				'U' => new(-number, 0),
				'D' => new(number, 0),
				'L' => new(0, -number),
				'R' => new(0, number),
				_ => throw new Exception()
			};

			wall1 += number;
			area1 += pos1.X * posNew.Y - posNew.X * pos1.Y;
			pos1 = posNew;

			// p2
			var hash = line.IndexOf('#') + 1;
			direction = line[hash + 5];
			number = int.Parse(line[hash..(hash + 5)], NumberStyles.HexNumber);

			posNew = pos2 + direction switch
			{
				'3' => new(-number, 0),
				'1' => new(number, 0),
				'2' => new(0, -number),
				'0' => new(0, number),
				_ => throw new Exception()
			};

			wall2 += number;
			area2 += pos2.X * posNew.Y - posNew.X * pos2.Y;
			pos2 = posNew;
		}

		var part1 = Math.Abs(area1) / 2 + wall1 / 2 + 1;
		var part2 = Math.Abs(area2) / 2 + wall2 / 2 + 1;

		return new(part1.ToString("0"), part2.ToString("0"));
	}
}
