using System;
using System.Text;

namespace AdventOfCode;

[Answer(1)]
public class Day1() : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			part1 += Score(line);
		}

		var sb = new StringBuilder(input)
			.Replace("one", "o1e")
			.Replace("two", "t2o")
			.Replace("three", "t3e")
			.Replace("four", "f4r")
			.Replace("five", "f5e")
			.Replace("six", "s6x")
			.Replace("seven", "s7n")
			.Replace("eight", "e8t")
			.Replace("nine", "n9e");

		foreach (var line in sb.ToString().AsSpan().EnumerateLines())
		{
			part2 += Score(line);
		}

		return new(part1.ToString(), part2.ToString());
	}

	private static int Score(ReadOnlySpan<char> line)
	{
		var first = line[line.IndexOfAnyInRange('0', '9')] - '0';
		var last = line[line.LastIndexOfAnyInRange('0', '9')] - '0';

		return first * 10 + last;
	}
}
