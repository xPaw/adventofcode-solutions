using System;
using System.Collections.Generic;
using System.Linq;

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
			.ToList();

		var answers = RecalculateBits(lines);
		var gamma = 0;
		var epsilon = 0;

		for (var i = 0; i < BitLength; i++)
		{
			if (answers[i])
			{
				gamma |= 1 << i;
			}
			else
			{
				epsilon |= 1 << i;
			}
		}

		var generatorValue = FindValue(lines.ToList(), 1);
		var scrubberValue = FindValue(lines.ToList(), 0);

		var part1 = gamma * epsilon;
		var part2 = generatorValue * scrubberValue;

		return (part1.ToString(), part2.ToString());
	}

	private int FindValue(List<int> input, int valueToFind)
	{
		for (var i = BitLength - 1; i >= 0; i--)
		{
			var isOneCommon = RecalculateSingleBit(input, i) ? valueToFind : (1 - valueToFind);
			var newArray = new List<int>();

			for (var l = 0; l < input.Count; l++)
			{
				var line = input[l];

				if (((line >> i) & 1) == isOneCommon)
				{
					newArray.Add(line);
				}
			}

			if (newArray.Count == 1)
			{
				return newArray[0];
			}

			input = newArray;
		}

		return 0;
	}

	private bool[] RecalculateBits(List<int> input)
	{
		var ones = new int[BitLength];

		foreach (var line in input)
		{
			for (var y = BitLength - 1; y >= 0; y--)
			{
				if (((line >> y) & 1) > 0)
				{
					ones[y]++;
				}
			}
		}

		return ones.Select(x => x >= input.Count / 2.0).ToArray();
	}

	private bool RecalculateSingleBit(List<int> input, int position)
	{
		var ones = 0;
		var half = input.Count / 2.0;

		foreach (var line in input)
		{
			if (((line >> position) & 1) > 0)
			{
				if (++ones >= half)
				{
					return true;
				}
			}
		}

		return false;
	}
}
