using System;
using System.Diagnostics;

namespace AdventOfCode;

[Answer(2)]
public class Day2 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var gameId = 0;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			gameId++;

			var possible = true;
			var maxR = 0;
			var maxG = 0;
			var maxB = 0;
			int limit;

			for (var i = line.IndexOf(':') + 2; i < line.Length; i++)
			{
				var num = line[i] - '0';
				var next = line[i + 1];

				if (next != ' ')
				{
					i++;
					num = 10 * num + next - '0';
				}

				switch (line[i + 2])
				{
					case 'r':
						i += 6;
						limit = 12;
						if (maxR < num) maxR = num;
						break;

					case 'g':
						i += 8;
						limit = 13;
						if (maxG < num) maxG = num;
						break;

					case 'b':
						i += 7;
						limit = 14;
						if (maxB < num) maxB = num;
						break;

					default:
						throw new UnreachableException();
				}

				if (num > limit)
				{
					possible = false;
				}
			}

			part2 += maxR * maxG * maxB;

			if (possible)
			{
				part1 += gameId;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
