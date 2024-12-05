using System;
using System.Collections.Generic;
using System.Diagnostics;

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
		var dependents = new Dictionary<int, List<int>>();

		foreach (var line in span[..split].EnumerateLines())
		{
			var b = ParseTwoInt(line[0..]);
			var a = ParseTwoInt(line[3..]);

			if (!dependents.TryGetValue(b, out var list))
			{
				list = [];
				dependents[b] = list;
			}

			list.Add(a);
		}

		var pages = new List<int>(32);

		foreach (var line in span[(split + 2)..].EnumerateLines())
		{
			pages.Clear();

			foreach (var page in line.Split(','))
			{
				var number = ParseTwoInt(line[page].ToString());
				pages.Add(number);
			}

			var valid = true;

			for (var i = 0; i < pages.Count; i++)
			{
				if (!dependents.TryGetValue(pages[i], out var deps))
				{
					continue;
				}

				foreach (var dep in deps)
				{
					var pos = pages.IndexOf(dep, 0, i);

					if (pos > -1)
					{
						valid = false;
					}
				}
			}

			if (valid)
			{
				part1 += pages[pages.Count / 2];
				continue;
			}

			// part 2
			do
			{
				valid = true;

				for (var i = 0; i < pages.Count; i++)
				{
					if (!dependents.TryGetValue(pages[i], out var deps))
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
			}
			while (!valid);

			part2 += pages[pages.Count / 2];
		}

		return new(part1.ToString(), part2.ToString());
	}


	static int ParseTwoInt(ReadOnlySpan<char> line) => 10 * (line[0] - '0') + (line[1] - '0');
}
