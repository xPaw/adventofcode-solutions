using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(12)]
class Day12 : IAnswer
{
	const string START = "start";
	const string END = "end";

	readonly Dictionary<string, List<string>> Caves = new();

	public (string Part1, string Part2) Solve(string input)
	{
		foreach (var line in input.Split('\n'))
		{
			var path = line.Split('-', 2);

			if (!Caves.ContainsKey(path[0]))
			{
				Caves.Add(path[0], new List<string>());
			}

			if (!Caves.ContainsKey(path[1]))
			{
				Caves.Add(path[1], new List<string>());
			}

			Caves[path[0]].Add(path[1]);
			Caves[path[1]].Add(path[0]);
		}

		var part1 = Score(START, new Stack<string>(), true);
		var part2 = Score(START, new Stack<string>(), false);

		return (part1.ToString(), part2.ToString());
	}

	int Score(string start, Stack<string> visited, bool moreThanOnce)
	{
		if (start == END)
		{
			return 1;
		}

		var isSmall = start.All(char.IsLower);

		if (isSmall)
		{
			visited.Push(start);
		}

		var score = 0;

		foreach (var point in Caves[start])
		{
			var newMoreThanOnce = moreThanOnce;
			if (visited.Contains(point))
			{
				if (moreThanOnce || point == START)
				{
					continue;
				}

				newMoreThanOnce = true;
			}

			score += Score(point, visited, newMoreThanOnce);
		}

		if (isSmall)
		{
			visited.Pop();
		}

		return score;
	}
}
