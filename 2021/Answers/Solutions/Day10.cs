using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(10)]
class Day10 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split();
		var scores = new List<ulong>();
		var part1 = 0;

		foreach (var line in lines)
		{
			var brackets = new Stack<char>();
			var good = true;

			for (var i = 0; i < line.Length; i++)
			{
				var bracket = line[i];

				switch (bracket)
				{
					case '(': brackets.Push(')'); break;
					case '[': brackets.Push(']'); break;
					case '{': brackets.Push('}'); break;
					case '<': brackets.Push('>'); break;

					case ')':
					case ']':
					case '}':
					case '>':
						var expected = brackets.Pop();

						if (expected != bracket)
						{
							good = false;
							part1 += bracket switch
							{
								')' => 3,
								']' => 57,
								'}' => 1197,
								'>' => 25137,
								_ => throw new NotImplementedException(),
							};
						}

						break;
				}

				if (!good)
				{
					break;
				}
			}

			if (!good)
			{
				continue;
			}

			ulong score = 0;

			while (brackets.Count > 0)
			{
				var bracket = brackets.Pop();

				score *= 5;
				score += bracket switch
				{
					')' => 1,
					']' => 2,
					'}' => 3,
					'>' => 4,
					_ => throw new NotImplementedException(),
				};
			}

			scores.Add(score);
		}

		scores.Sort();

		var part2 = scores[scores.Count / 2];

		return (part1.ToString(), part2.ToString());
	}
}
