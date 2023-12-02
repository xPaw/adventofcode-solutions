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
		var i = 0;
		var length = input.Length;
		var gameId = 0;
		var currentBag = new int[3];
		var possible = true;

		int ParseInt()
		{
			var result = 0;

			do
			{
				result = 10 * result + input[i++] - '0';
			}
			while (char.IsAsciiDigit(input[i]));

			return result;
		}

		void Score()
		{
			part2 += currentBag[0] * currentBag[1] * currentBag[2];

			if (possible)
			{
				part1 += gameId;
			}
		}

		while (i < length)
		{
			var t = input[i];

			if (t == '\n')
			{
				Score();
				i++;
				gameId = 0;
				continue;
			}

			if (!char.IsAsciiDigit(t))
			{
				i++;
				continue;
			}

			var num = ParseInt();

			if (gameId == 0)
			{
				gameId = num;
				possible = true;
				currentBag[0] = 0;
				currentBag[1] = 0;
				currentBag[2] = 0;
				continue;
			}

			var color = input[++i] switch
			{
				'r' => 0,
				'g' => 1,
				'b' => 2,
				_ => throw new UnreachableException(),
			};

			if (num > (12 + color))
			{
				possible = false;
			}

			if (currentBag[color] < num)
			{
				currentBag[color] = num;
			}
		}

		Score();

		return new(part1.ToString(), part2.ToString());
	}
}
