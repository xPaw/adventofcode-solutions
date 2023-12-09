using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(9)]
public class Day9 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var sequence = new List<int>(32);

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var remainder = line;

			do
			{
				var result = 0;
				var i = 0;
				var sign = 1;

				if (remainder[0] == '-')
				{
					i++;
					sign = -1;
				}

				do
				{
					var t = remainder[i++];

					if (t == ' ')
					{
						break;
					}

					result = 10 * result + t - '0';
				}
				while (i < remainder.Length);

				result *= sign;

				sequence.Add(result);

				remainder = remainder[i..];
			}
			while (remainder.Length > 0);

			part2 += sequence[0];
			part1 += sequence[^1];
			var mul = -1;

			var length = sequence.Count;

			while (true)
			{
				for (var s = 0; s < length - 1; s++)
				{
					sequence[s] = sequence[s + 1] - sequence[s];
				}

				length -= 1;
				var start = sequence[0];
				var end = sequence[length - 1];

				if (start == 0 && end == 0)
				{
					break;
				}

				part1 += end;
				part2 += start * mul;
				mul *= -1;
			}

			sequence.Clear();
		}

		return new(part1.ToString(), part2.ToString());
	}
}
