using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(23)]
public class Day23 : IAnswer
{
	public Solution Solve(string input)
	{
		var connections = new Dictionary<string, HashSet<string>>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var a = line[..2].ToString();
			var b = line[3..].ToString();

			if (!connections.TryGetValue(a, out var set))
			{
				set = [];
				connections[a] = set;
			}
			set.Add(b);

			if (!connections.TryGetValue(b, out set))
			{
				set = [];
				connections[b] = set;
			}
			set.Add(a);
		}

		var part1 = Part1(connections);
		var part2 = Part2(connections);

		return new(part1.ToString(), part2);
	}

	private static int Part1(Dictionary<string, HashSet<string>> connections)
	{
		var threes = new HashSet<string>();

		foreach (var (a, other) in connections)
		{
			foreach (var b in other)
			{
				foreach (var c in connections[b])
				{
					if (c != a && connections[c].Contains(a))
					{
						if (a[0] == 't' || b[0] == 't' || c[0] == 't')
						{
							threes.Add(string.Join(',', new[] { a, b, c }.OrderBy(x => x)));
						}
					}
				}
			}
		}

		return threes.Count;
	}

	private static string Part2(Dictionary<string, HashSet<string>> connections)
	{
		var largestGroup = new HashSet<string>();

		foreach (var a in connections.Keys)
		{
			var group = new HashSet<string> { a };
			var candidates = connections[a].ToHashSet();

			while (candidates.Count > 0)
			{
				var nextComputer = candidates.FirstOrDefault(c => group.All(g => connections[c].Contains(g)));

				if (nextComputer == null)
				{
					break;
				}

				group.Add(nextComputer);
				candidates.Remove(nextComputer);
			}

			if (group.Count > largestGroup.Count)
			{
				largestGroup = group;
			}
		}

		return string.Join(',', largestGroup.OrderBy(x => x));
	}
}
