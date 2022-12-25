using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode;

[Answer(25)]
public class Day25 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		static long ParseSnafu(ReadOnlySpan<char> line)
		{
			var result = 0L;

			foreach (var c in line)
			{
				result = 5L * result + c switch
				{
					'2' => 2,
					'1' => 1,
					'0' => 0,
					'-' => -1,
					'=' => -2,
					_ => throw new NotImplementedException(),
				};
			}

			return result;
		}

		static string EncodeSnafu(long number)
		{
			var chars = new List<char>(20);

			while (number > 0)
			{
				(number, var remainder) = Math.DivRem(number, 5);

				chars.Add(remainder switch
				{
					0 => '0',
					2 => '2',
					1 => '1',
					3 => '=',
					4 => '-',
					_ => throw new NotImplementedException(),
				});

				if (remainder > 2)
				{
					number++;
				}
			}

			var str = new StringBuilder(chars.Count);

			for (var i = chars.Count - 1; i >= 0; i--)
			{
				str.Append(chars[i]);
			}

			return str.ToString();
		}

		var sum = 0L;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			sum += ParseSnafu(line);
		}

		return (EncodeSnafu(sum), "Woo!");
	}
}
