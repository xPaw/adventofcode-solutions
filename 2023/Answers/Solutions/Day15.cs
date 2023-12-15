using System.Collections.Generic;

namespace AdventOfCode;

[Answer(15)]
public class Day15 : IAnswer
{
	record struct Lens(string Label, int Value);

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var init = input.Split(',');
		var boxes = new List<Lens>[256];

		for (var i = 0; i < boxes.Length; i++)
		{
			boxes[i] = [];
		}

		foreach (var line in init)
		{
			var currentValue = 0;

			foreach (var c in line)
			{
				currentValue += (byte)c;
				currentValue *= 17;
				currentValue %= 256;
			}

			part1 += currentValue;

			currentValue = 0;
			var label = line;

			for (var i = 0; i < line.Length; i++)
			{
				var c = line[i];

				if (c is '-' or '=')
				{
					label = line[..i];
					break;
				}

				currentValue += (byte)c;
				currentValue *= 17;
				currentValue %= 256;
			}

			var box = boxes[currentValue];
			var index = box.FindIndex(c => c.Label == label);

			if (index == -1)
			{
				if (line[^1] == '-')
				{
					continue;
				}

				box.Add(new(label, line[^1] - '0'));
			}
			else if (line[^1] == '-')
			{
				box.RemoveAt(index);
			}
			else
			{
				box[index] = new(label, line[^1] - '0');
			}
		}

		for (var boxId = 0; boxId < boxes.Length; boxId++)
		{
			var box = boxes[boxId];

			for (var slot = 0; slot < box.Count; slot++)
			{
				part2 += (boxId + 1) * (slot + 1) * box[slot].Value;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
