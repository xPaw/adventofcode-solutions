using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(5)]
public class Day5 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var span = input.AsSpan();
		var split = span.IndexOf("\n\n");
		var dependents = new List<int>[100];

		foreach (var line in span[..split].EnumerateLines())
		{
			var b = ParseTwoInt(line[0..]);
			var a = ParseTwoInt(line[3..]);
			var deps = dependents[b];

			if (deps == null)
			{
				dependents[b] = [a];
			}
			else
			{
				deps.Add(a);
			}
		}

		var pages = new List<int>(32);

		foreach (var line in span[(split + 2)..].EnumerateLines())
		{
			pages.Clear();

			for (var i = 0; i < line.Length; i += 3)
			{
				var number = ParseTwoInt(line[i..]);
				pages.Add(number);
			}

			bool valid;
			var initiallyValid = true;

			do
			{
				valid = true;

				for (var i = 0; i < pages.Count; i++)
				{
					var deps = dependents[pages[i]];

					if (deps == null)
					{
						continue;
					}

					foreach (var dep in deps)
					{
						var pos = pages.IndexOf(dep, 0, i);

						if (pos > -1)
						{
							(pages[i], pages[pos]) = (pages[pos], pages[i]);
							valid = false;
						}
					}
				}

				if (valid && initiallyValid)
				{
					break;
				}

				initiallyValid = false;
			}
			while (!valid);

			var mid = pages[pages.Count / 2];

			if (initiallyValid)
			{
				part1 += mid;
			}
			else
			{
				part2 += mid;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}

	static int ParseTwoInt(ReadOnlySpan<char> line) => 10 * (line[0] - '0') + (line[1] - '0');
}
