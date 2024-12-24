using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(24)]
public class Day24 : IAnswer
{
	record struct Instruction(string Left, string Right, string Target, string Op);

	public Solution Solve(string input)
	{
		var inputSpan = input.AsSpan();
		var inputSplit = inputSpan.IndexOf("\n\n");
		var wiresInput = inputSpan[..inputSplit];
		var connectionsInput = inputSpan[(inputSplit + 2)..];
		var wires = new Dictionary<string, int>();
		var bitCount = 0;

		foreach (var line in wiresInput.EnumerateLines())
		{
			var colon = line.IndexOf(':');
			var wire = line[..colon].ToString();
			var number = int.Parse(line[(colon + 2)..]);

			if (wire[0] == 'x')
			{
				bitCount++;
			}

			wires.Add(wire, number);
		}

		var instructions = new List<Instruction>();
		var xyAdds = new List<Instruction>();
		var xyCarries = new List<Instruction>();
		var zOuts = new List<Instruction>();
		var ands = new List<Instruction>();
		var carries = new List<Instruction>();
		var wrongs = new List<string>();

		foreach (var line in connectionsInput.EnumerateLines())
		{
			var parts = line.ToString().Split(' ');
			var inst = new Instruction
			{
				Left = parts[0],
				Op = parts[1],
				Right = parts[2],
				Target = parts[4]
			};

			instructions.Add(inst);

			if (inst.Left[0] is 'x' or 'y' || inst.Right[0] is 'x' or 'y')
			{
				switch (inst.Op)
				{
					case "XOR":
						if (inst.Left is not "x00" and not "y00")
						{
							xyAdds.Add(inst);
						}
						break;
					case "AND":
						if (inst.Left is not "x00" and not "y00")
						{
							xyCarries.Add(inst);
						}
						break;
				}
			}
			else
			{
				switch (inst.Op)
				{
					case "AND":
						ands.Add(inst);
						break;
					case "OR":
						carries.Add(inst);
						break;
					case "XOR":
						zOuts.Add(inst);
						break;
				}
			}
		}

		var part1Instructions = new Queue<Instruction>(instructions);

		while (part1Instructions.TryDequeue(out var inst))
		{
			if (!wires.TryGetValue(inst.Left, out var left) || !wires.TryGetValue(inst.Right, out var right))
			{
				part1Instructions.Enqueue(inst);
				continue;
			}

			wires[inst.Target] = inst.Op switch
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

		foreach (var xy in xyAdds)
		{
			if (xy.Target[0] == 'z' || !zOuts.Any(z => z.Left == xy.Target || z.Right == xy.Target))
			{
				wrongs.Add(xy.Target);
			}
		}

		foreach (var xy in xyCarries)
		{
			if (xy.Target[0] == 'z' || !carries.Any(c => c.Left == xy.Target || c.Right == xy.Target))
			{
				wrongs.Add(xy.Target);
			}
		}

		foreach (var z in zOuts)
		{
			if (z.Target[0] != 'z')
			{
				wrongs.Add(z.Target);
			}
		}

		var lastZ = $"z{bitCount}";

		foreach (var c in carries)
		{
			if (c.Target[0] == 'z' && c.Target != lastZ)
			{
				wrongs.Add(c.Target);
			}
		}

		foreach (var a in ands)
		{
			if (a.Target[0] == 'z')
			{
				wrongs.Add(a.Target);
			}
		}

		var part2 = string.Join(",", wrongs.Order());

		return new(part1.ToString(), part2.ToString());
	}
}
