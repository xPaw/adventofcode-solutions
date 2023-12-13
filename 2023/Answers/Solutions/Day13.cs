using System;

namespace AdventOfCode;

[Answer(13)]
public class Day13 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var remainder = input.AsSpan();

		do
		{
			var maze = remainder;
			var nl = remainder.IndexOf("\n\n");

			if (nl == -1)
			{
				remainder = null;
			}
			else
			{
				maze = remainder[0..nl];
				remainder = remainder[(nl + 2)..];
			}

			var width = maze.IndexOf('\n');
			var stride = width + 1;
			var height = (maze.Length + 1) / stride;

			for (var x = 0; x < width - 1; x++)
			{
				var diff = 0;

				for (var y = 0; y < height; y++)
				{
					for (var d = 0; x >= d && x + d + 1 < width; d++)
					{
						if (maze[y * stride + x - d] != maze[y * stride + x + d + 1] && ++diff > 0)
						{
							break;
						}
					}
				}

				if (diff == 0)
				{
					part1 += x + 1;
				}
				else if (diff == 1)
				{
					part2 += x + 1;
				}
			}

			for (var y = 0; y < height - 1; y++)
			{
				var diff = 0;

				for (var x = 0; x < width; x++)
				{
					for (var d = 0; y >= d && y + d + 1 < height; d++)
					{
						if (maze[(y - d) * stride + x] != maze[(y + d + 1) * stride + x] && ++diff > 0)
						{
							break;
						}
					}
				}

				if (diff == 0)
				{
					part1 += (y + 1) * 100;
				}
				else if (diff == 1)
				{
					part2 += (y + 1) * 100;
				}
			}
		}
		while (remainder != null);

		return new(part1.ToString(), part2.ToString());
	}
}
