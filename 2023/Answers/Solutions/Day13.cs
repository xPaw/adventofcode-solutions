namespace AdventOfCode;

[Answer(13)]
public class Day13 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var mazes = input.Split("\n\n");

		foreach (var lines in mazes)
		{
			var maze = lines.Split('\n');
			var height = maze.Length;
			var width = maze[0].Length;

			for (var x = 0; x < width - 1; x++)
			{
				var diff = 0;

				for (var y = 0; y < height; y++)
				{
					for (var d = 0; x >= d && x + d + 1 < width; d++)
					{
						if (maze[y][x - d] != maze[y][x + d + 1] && ++diff > 0)
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
						if (maze[y - d][x] != maze[y + d + 1][x] && ++diff > 0)
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

		return new(part1.ToString(), part2.ToString());
	}
}
