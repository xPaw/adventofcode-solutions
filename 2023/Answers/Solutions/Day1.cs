using System;

namespace AdventOfCode;

[Answer(1)]
public class Day1() : IAnswer
{
	private readonly static string[] Words =
	[
		"one",
		"two",
		"three",
		"four",
		"five",
		"six",
		"seven",
		"eight",
		"nine",
	];

	static bool IsDigitLetter(char c) => c is 'e' or 'f' or 'n' or 'o' or 's' or 't';

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var first = 0;
			var last = 0;
			var first2 = 0;
			var last2 = 0;

			for (var i = 0; i < line.Length; i++)
			{
				var c = line[i];

				if (char.IsAsciiDigit(c))
				{
					var num = c - '0';

					if (first == 0)
					{
						first = num;
					}

					if (first2 == 0)
					{
						first2 = num;
					}

					last = num;
					last2 = num;

					continue;
				}

				if (i <= line.Length - 3 && IsDigitLetter(c))
				{
					for (var counter = 0; counter < Words.Length; counter++)
					{
						var test = Words[counter];
						var end = i + test.Length;

						if (end <= line.Length && line[i..end].Equals(test, StringComparison.Ordinal))
						{
							i += test.Length - 2; // skip forward but allow matching overlapping word
							counter += 1;

							if (first2 == 0)
							{
								first2 = counter;
							}

							last2 = counter;

							break;
						}
					}
				}
			}

			part1 += first * 10 + last;
			part2 += first2 * 10 + last2;
		}

		return new(part1.ToString(), part2.ToString());
	}
}
