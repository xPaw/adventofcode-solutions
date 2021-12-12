using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(12)]
class Day12 : IAnswer
{
	const string START = "start";
	const string END = "end";

	readonly Dictionary<string, HashSet<string>> Caves = new();

	public (string Part1, string Part2) Solve(string input)
	{
		foreach (var line in input.Split('\n'))
		{
			var path = line.Split('-', 2);

			if (!Caves.ContainsKey(path[0]))
			{
				Caves.Add(path[0], new HashSet<string>());
			}

			if (!Caves.ContainsKey(path[1]))
			{
				Caves.Add(path[1], new HashSet<string>());
			}

			Caves[path[0]].Add(path[1]);
			Caves[path[1]].Add(path[0]);
		}

		var part1 = Score(START, new HashSet<string>() { START });
		var part2 = Score(START, new HashSet<string>() { START }, true);

		return (part1.ToString(), part2.ToString());
	}

	int Score(string start, HashSet<string> visited, bool moreThanOnce = false)
	{
		var score = 0;

		foreach (var point in Caves[start])
		{
			if (point == END)
			{
				score++;
				continue;
			}

			if (visited.Contains(point))
			{
				if (moreThanOnce && point != START)
				{
					score += Score(point, new HashSet<string>(visited, visited.Comparer));
				}

				continue;
			}

			var visiting = new HashSet<string>(visited, visited.Comparer);

			if (point.All(char.IsLower))
			{
				visiting.Add(point);
			}

			score += Score(point, visiting, moreThanOnce);
		}

		return score;
	}
}
