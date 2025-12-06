using System;

namespace AdventOfCode;

[Answer(6)]
public class Day6 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;

		var lines = input.Split('\n');
		var rows = lines.Length - 1;
		var cols = lines[0].Length;

		var nums1 = new long[rows];
		var pow1 = new long[rows];
		var nums2 = new long[10];
		var numCount = 0;

		for (var c = cols - 1; c >= 0; c--)
		{
			for (var r = 0; r < rows; r++)
			{
				var ch = c < lines[r].Length ? lines[r][c] : ' ';
				if (ch is >= '0' and <= '9')
				{
					nums1[r] += (ch - '0') * (pow1[r] > 0 ? pow1[r] : 1);
					pow1[r] = (pow1[r] > 0 ? pow1[r] : 1) * 10;
					nums2[numCount] = nums2[numCount] * 10 + (ch - '0');
				}
			}

			numCount++;

			var op = c < lines[rows].Length ? lines[rows][c] : ' ';
			if (op is '+' or '*')
			{
				part1 += Calc(op, nums1.AsSpan(0, rows));
				part2 += Calc(op, nums2.AsSpan(0, numCount));

				Array.Clear(nums1);
				Array.Clear(nums2);
				Array.Clear(pow1);
				numCount = 0;
				c--;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	private static long Calc(char op, Span<long> nums)
	{
		var result = op == '+' ? 0L : 1L;

		foreach (var n in nums)
		{
			result = op == '+' ? result + n : result * n;
		}

		return result;
	}
}
