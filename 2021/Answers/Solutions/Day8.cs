using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(8)]
class Day8 : IAnswer
{
	//  aaaa
	// b    c
	// b    c
	//  dddd
	// e    f
	// e    f
	//  gggg
	[Flags]
	enum Segment
	{
		A = 1 << 1,
		B = 1 << 2,
		C = 1 << 3,
		D = 1 << 4,
		E = 1 << 5,
		F = 1 << 6,
		G = 1 << 7,
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input
			.Split('\n')
			.Select(l => l
				.Split(" | ")
				.Select(s => s.Split(' '))
				.ToArray()
			)
			.ToArray();

		var part1 = 0;
		var part2 = 0;

		foreach (var line in lines)
		{
			var signal = line[0];
			var output = line[1];
			Segment one = 0;
			Segment four = 0;

			foreach (var value in signal)
			{
				switch (value.Length)
				{
					case 2: one = Parse(value); break;
					case 4: four = Parse(value); break;
				}
			}

			var outputValue = 0;
			var power = output.Length - 1;

			foreach (var value in output)
			{
				var number = -1;

				switch (value.Length)
				{
					case 2: number = 1; part1++; break;
					case 4: number = 4; part1++; break;
					case 3: number = 7; part1++; break;
					case 7: number = 8; part1++; break;
					default:
						var parsed = Parse(value);
						var knownOne = parsed & one;
						var knownFour = parsed & four;

						number = (value.Length, GetBitCount(knownOne), GetBitCount(knownFour)) switch
						{
							(5, 1, 2) => 2,
							(5, 1, 3) => 5,
							(5, 2, 3) => 3,
							(6, 1, 3) => 6,
							(6, 2, 3) => 0,
							(6, 2, 4) => 9,
							_ => throw new NotImplementedException(),
						};
						break;
				}

				outputValue += number * (int)Math.Pow(10, power--);
			}

			part2 += outputValue;
		}

		return (part1.ToString(), part2.ToString());
	}

	Segment Parse(string signal)
	{
		Segment on = 0;

		for (var i = 0; i < signal.Length; i++)
		{
			on |= signal[i] switch
			{
				'a' => Segment.A,
				'b' => Segment.B,
				'c' => Segment.C,
				'd' => Segment.D,
				'e' => Segment.E,
				'f' => Segment.F,
				'g' => Segment.G,
				_ => throw new NotImplementedException(),
			};
		}

		return on;
	}

	int GetBitCount(Segment lValue)
	{
		int iCount = 0;

		while (lValue != 0)
		{
			lValue &= lValue - 1;
			iCount++;
		}

		return iCount;
	}
}
