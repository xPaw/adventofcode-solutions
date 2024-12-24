using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(24)]
public class Day24 : IAnswer
{
	public Solution Solve(string input)
	{
		var part2 = 0;
		var inputSpan = input.AsSpan();
		var inputSplit = inputSpan.IndexOf("\n\n");
		var wiresInput = inputSpan[..inputSplit];
		var connectionsInput = inputSpan[(inputSplit + 2)..];
		var wires = new Dictionary<string, int>();

		foreach (var line in wiresInput.EnumerateLines())
		{
			var colon = line.IndexOf(':');
			var wire = line[..colon].ToString();
			var number = int.Parse(line[(colon + 2)..]);

			wires.Add(wire, number);
		}

		var queue = new Queue<string[]>();

		foreach (var line in connectionsInput.EnumerateLines())
		{
			var parts = line.ToString().Split(' ');
			queue.Enqueue(parts);
		}

		while (queue.TryDequeue(out var parts))
		{
			if (!wires.TryGetValue(parts[0], out var left) || !wires.TryGetValue(parts[2], out var right))
			{
				queue.Enqueue(parts);
				continue;
			}

			var op = parts[1];
			var target = parts[4];

			wires[target] = op switch
			{
				"AND" => left & right,
				"XOR" => left ^ right,
				"OR" => left | right,
				_ => throw new NotImplementedException(),
			};
		}

		var bits = wires.Keys.Where(static k => k[0] == 'z').OrderDescending();
		var part1 = 0L;

		foreach (var wireName in bits)
		{
			part1 = (part1 << 1) | (long)wires[wireName];
		}

		return new(part1.ToString(), part2.ToString());
	}
}
