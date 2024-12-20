using System;

namespace AdventOfCode;

[Answer(13)]
public class Day13 : IAnswer
{
	record struct Coord(long X, long Y);

	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;
		var span = input.AsSpan();

		foreach (var groupRange in span.Split("\n\n"))
		{
			var group = span[groupRange];
			int offset;
			var i = 0;
			var a = new Coord();
			var b = new Coord();
			var prize = new Coord();

			while ((offset = group.IndexOfAny('X', 'Y')) > 0)
			{
				group = group[(offset + 2)..];

				var num = ParseInt(ref group);

				switch (i++)
				{
					case 0: a.X = num; break;
					case 1: a.Y = num; break;
					case 2: b.X = num; break;
					case 3: b.Y = num; break;
					case 4: prize.X = num; break;
					case 5: prize.Y = num; break;
				}
			}

			var (pressA, pressB) = SolveMachine(a, b, prize);
			if (pressA > 0 || pressB > 0)
			{
				part1 += 3L * pressA + pressB;
			}

			prize.X += 10_000_000_000_000L;
			prize.Y += 10_000_000_000_000L;

			(pressA, pressB) = SolveMachine(a, b, prize);
			if (pressA > 0 || pressB > 0)
			{
				part2 += 3L * pressA + pressB;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	private Coord SolveMachine(Coord a, Coord b, Coord prize)
	{
		var d = (a.X * b.Y) - (a.Y * b.X);

		var (aq, ar) = Math.DivRem((prize.X * b.Y) - (prize.Y * b.X), d);
		var (bq, br) = Math.DivRem((a.X * prize.Y) - (a.Y * prize.X), d);

		if (ar != 0 || br != 0)
		{
			return new(0, 0);
		}

		return new(aq, bq);
	}

	static int ParseInt(ref ReadOnlySpan<char> line)
	{
		var result = 0;

		while (true)
		{
			if (line.IsEmpty || !char.IsAsciiDigit(line[0]))
			{
				break;
			}

			result = 10 * result + line[0] - '0';
			line = line[1..];
		}

		return result;
	}
}
