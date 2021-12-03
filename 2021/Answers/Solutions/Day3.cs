using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2021;

[Answer(3)]
class Day3 : IAnswer
{
	private int BitLength;

	public (string Part1, string Part2) Solve(string input)
	{
		BitLength = input.IndexOf('\n');
		var lines = input
			.Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Select(l => Convert.ToInt32(l, 2))
			.ToArray();

		var (zeros, ones) = RecalculateBits(lines);
		var gamma = 0;
		var rate = 0;

		for (var i = 0; i < BitLength; i++)
		{
			var isOneCommon = ones[i] >= zeros[i];

			if (isOneCommon)
			{
				gamma |= 1 << i;
			}
			else
			{
				rate |= 1 << i;
			}
		}

		var generatorValue = FindValue((int[])lines.Clone(), 1);
		var scrubberValue = FindValue((int[])lines.Clone(), 0);

		var part1 = (gamma * rate).ToString();
		var part2 = (generatorValue * scrubberValue).ToString();

		return (part1.ToString(), part2.ToString());
	}

	private int FindValue(int[] input, int valueToFind)
	{
		var (zeros, ones) = RecalculateBits(input);

		for (var i = BitLength - 1; i >= 0; i--)
		{
			var isOneCommon = ones[i] >= zeros[i] ? (1 - valueToFind) : valueToFind;

			for (var l = 0; l < input.Length; l++)
			{
				var line = input[l];

				if (((line >> i) & 1) == isOneCommon)
				{
					input[l] = 0;
				}
			}

			input = input.Where(x => x > 0).ToArray();

			if (input.Length == 1)
			{
				return input[0];
			}

			(zeros, ones) = RecalculateBits(input);
		}

		return 0;
	}

	private (int[] zeros, int[] ones) RecalculateBits(int[] input)
	{
		var zeros = new int[BitLength];
		var ones = new int[BitLength];

		foreach (var line in input)
		{
			for (var y = BitLength - 1; y >= 0; y--)
			{
				if (((line >> y) & 1) > 0)
				{
					ones[y]++;
				}
				else
				{
					zeros[y]++;
				}
			}
		}

		return (zeros, ones);
	}
}
