using System;
using System.Linq;

namespace AdventOfCode;

[Answer(4)]
public class Day4 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var grid = new ReadOnlyGrid(input);
		var XMAS = "XMAS".AsSpan();

		for (var y = 0; y < grid.Height; y++)
		{
			for (var x = 0; x < grid.Width; x++)
			{
				var current = grid[y, x];

				if (current == 'A')
				{
					var a = grid[y - 1, x - 1] == 'M' && grid[y + 1, x + 1] == 'S' ? 1 : 0;
					var c = grid[y - 1, x + 1] == 'M' && grid[y + 1, x - 1] == 'S' ? 1 : 0;
					var b = grid[y + 1, x - 1] == 'M' && grid[y - 1, x + 1] == 'S' ? 1 : 0;
					var d = grid[y + 1, x + 1] == 'M' && grid[y - 1, x - 1] == 'S' ? 1 : 0;

					if ((a + b + c + d) > 1)
					{
						part2++;
					}

					continue;
				}

				if (current != 'X')
				{
					continue;
				}

				var found = true;

				// horizontal
				for (var dx = 0; dx < 4; dx++)
				{
					if (grid[y, x + dx] != XMAS[dx])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// written backwards
				for (var dx = 0; dx < 4; dx++)
				{
					if (grid[y, x - dx] != XMAS[dx])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// vertical down
				for (var dy = 0; dy < 4; dy++)
				{
					if (grid[y + dy, x] != XMAS[dy])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// vertical up
				for (var dy = 0; dy < 4; dy++)
				{
					if (grid[y - dy, x] != XMAS[dy])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// diagonal down right
				for (var dd = 0; dd < 4; dd++)
				{
					if (grid[y + dd, x + dd] != XMAS[dd])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// diagonal up left
				for (var dd = 0; dd < 4; dd++)
				{
					if (grid[y - dd, x - dd] != XMAS[dd])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// diagonal down left
				for (var dd = 0; dd < 4; dd++)
				{
					if (grid[y + dd, x - dd] != XMAS[dd])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
				found = true;

				// diagonal down right
				for (var dd = 0; dd < 4; dd++)
				{
					if (grid[y - dd, x + dd] != XMAS[dd])
					{
						found = false;
						break;
					}
				}

				if (found) part1++;
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
