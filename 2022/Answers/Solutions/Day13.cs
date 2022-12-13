using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace AdventOfCode;

[Answer(13)]
public class Day13 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		JsonNode? left = null;
		JsonNode? right = null;
		var nodes = new List<JsonNode>();
		var i = 0;
		var pair = 1;
		var part1 = 0;

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			if (i == 2)
			{
				if (ComparePackets(left!, right!) < 0)
				{
					part1 += pair;
				}

				i = 0;
				pair++;
				continue;
			}

			var node = JsonNode.Parse(line.ToString());
			nodes.Add(node!);

			if (i++ == 0)
			{
				left = node;
			}
			else
			{
				right = node;
			}
		}

		if (ComparePackets(left!, right!) < 0)
		{
			part1 += pair;
		}

		var div1 = JsonNode.Parse("[[2]]")!;
		var div2 = JsonNode.Parse("[[6]]")!;

		nodes.Add(div1);
		nodes.Add(div2);
		nodes.Sort(ComparePackets);

		var part2 = (nodes.IndexOf(div1) + 1) * (nodes.IndexOf(div2) + 1);

		return (part1.ToString(), part2.ToString());
	}

	int ComparePackets(JsonNode left, JsonNode right)
	{
		if (left is JsonValue && right is JsonValue)
		{
			return (int)left - (int)right;
		}

		if (left is not JsonArray leftArray)
		{
			leftArray = new JsonArray((int)left);
		}

		if (right is not JsonArray rightArray)
		{
			rightArray = new JsonArray((int)right);
		}

		foreach (var (leftZip, rightZip) in leftArray.Zip(rightArray))
		{
			var result = ComparePackets(leftZip!, rightZip!);

			if (result != 0)
			{
				return result;
			}
		}

		if (leftArray.Count != rightArray.Count)
		{
			return leftArray.Count - rightArray.Count;
		}

		return 0;
	}
}
