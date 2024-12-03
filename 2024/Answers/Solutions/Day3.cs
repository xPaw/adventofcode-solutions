using System;
using System.Text.RegularExpressions;

namespace AdventOfCode;

[Answer(3)]
public partial class Day3 : IAnswer
{
	[GeneratedRegex(@"do\(\)|don't\(\)|mul\((?<a>[0-9]{1,3}),(?<b>[0-9]{1,3})\)")]
	private static partial Regex MulRegex();

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var doing = true;

		foreach (Match a in MulRegex().Matches(input))
		{
			if (a.ValueSpan[0] == 'd')
			{
				doing = a.ValueSpan.Length == 4;
				continue;
			}

			var val = ParseInt(a.Groups[1].ValueSpan) * ParseInt(a.Groups[2].ValueSpan);
			part1 += val;

			if (doing)
			{
				part2 += val;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	static int ParseInt(ReadOnlySpan<char> line)
	{
		var result = 0;
		var i = 0;

		do
		{
			result = 10 * result + line[i++] - '0';
		}
		while (i < line.Length);

		return result;
	}
}
