using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(25)]
public class Day25 : IAnswer
{
	public Solution Solve(string input)
	{
		var graph = new Dictionary<string, HashSet<string>>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var colon = line.IndexOf(':');
			var name = line[..colon].ToString();
			var destinations = line[(colon + 2)..].ToString().Split(' ').ToHashSet();

			if (!graph.TryGetValue(name, out var list1))
			{
				list1 = [];
				graph[name] = list1;
			}

			foreach (var node in destinations)
			{
				list1.Add(node);

				if (!graph.TryGetValue(node, out var list2))
				{
					list2 = [];
					graph[node] = list2;
				}

				list2.Add(name);
			}
		}

		var edges = new HashSet<string>(graph.Keys);

		int Count(string node) => graph[node].Count(n => !edges.Contains(n));

		while (edges.Sum(Count) != 3)
		{
			edges.Remove(edges.MaxBy(Count)!);
		}

		var part1 = edges.Count * graph.Keys.Except(edges).Count();
		var part2 = 0;

		return new(part1.ToString(), part2.ToString());
	}
}
