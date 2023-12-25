using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(25)]
public class Day25 : IAnswer
{
	record struct Edge(string Left, string Right);

	public Solution Solve(string input)
	{
		var nodes = new HashSet<string>();
		var edges = new HashSet<Edge>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var colon = line.IndexOf(':');
			var name = line[..colon].ToString();
			var destinations = line[(colon + 2)..].ToString().Split(' ').ToHashSet();

			nodes.Add(name);

			foreach (var node in destinations)
			{
				nodes.Add(node);

				if (!edges.Contains(new(name, node)) && !edges.Contains(new(node, name)))
				{
					edges.Add(new(name, node));
				}
			}
		}

		List<HashSet<string>> groups = [];
		var wires = 0;

		while (wires != 3)
		{
			wires = 0;
			groups = nodes.Select(n => new HashSet<string>([n])).ToList();

			var randomEdges = edges.ToArray();
			Random.Shared.Shuffle(randomEdges);

			for (var i = 0; i < edges.Count && groups.Count > 2; i++)
			{
				var (left, right) = randomEdges[i];
				var group1 = groups.First(s => s.Contains(left));
				var group2 = groups.First(s => s.Contains(right));

				if (group1 != group2)
				{
					foreach (var node in group2)
					{
						group1.Add(node);
					}

					groups.Remove(group2);
				}
			}

			foreach (var (left, right) in edges)
			{
				var group1 = groups.First(s => s.Contains(left));
				var group2 = groups.First(s => s.Contains(right));

				if (group1 != group2)
				{
					wires++;
				}
			}
		}

		var part1 = groups[0].Count * groups[1].Count;
		var part2 = 0;

		return new(part1.ToString(), part2.ToString());
	}
}
