using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(15)]
public class Day15 : IAnswer
{
	public Solution Solve(string input)
	{
		var inputSpan = input.AsSpan();
		var inputSplit = inputSpan.IndexOf("\n\n");
		var inputGrid = inputSpan[..inputSplit];
		var moves = inputSpan[(inputSplit + 2)..];

		var width = inputGrid.IndexOf('\n');
		var stride = width + 1;
		var height = (inputGrid.Length + 1) / stride;
		var grid = inputGrid.ToArray();
		var (guardY, guardX) = Math.DivRem(Array.IndexOf(grid, '@'), stride);
		grid[guardY * stride + guardX] = '.';

		var width2 = width * 2;
		var stride2 = width2 + 1;
		var grid2 = new char[height * stride2];
		var (guardX2, guardY2) = (guardX * 2, guardY);

		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				var c = grid[y * stride + x];
				var offset = y * stride2 + x * 2;

				if (c == 'O')
				{
					grid2[offset] = '[';
					grid2[offset + 1] = ']';
				}
				else
				{
					grid2[offset] = c;
					grid2[offset + 1] = c;
				}
			}

			grid2[y * stride2 + width2] = '\n';
		}

		var part1 = Move(grid, width, height, stride, guardX, guardY, moves, false);
		var part2 = Move(grid2, width2, height, stride2, guardX2, guardY2, moves, true);

		return new(part1.ToString(), part2.ToString());
	}

	private int Move(char[] grid, int width, int height, int stride, int guardX, int guardY, ReadOnlySpan<char> moves, bool isWide)
	{
		var queue = new Queue<(int X, int Y)>(16);
		var boxes = new List<(int X, int Y)>(16);

		foreach (var move in moves)
		{
			var newGuardX = guardX;
			var newGuardY = guardY;
			var deltaX = 0;
			var deltaY = 0;

			switch (move)
			{
				case '<':
					deltaX = -1;
					break;
				case '>':
					deltaX = 1;
					break;
				case '^':
					deltaY = -1;
					break;
				case 'v':
					deltaY = 1;
					break;
				default:
					continue;
			}

			newGuardX += deltaX;
			newGuardY += deltaY;

			var canMove = true;

			boxes.Clear();
			queue.Clear();
			queue.Enqueue((newGuardX, newGuardY));

			while (queue.TryDequeue(out var check))
			{
				var (checkX, checkY) = check;
				var c = grid[checkY * stride + checkX];

				if (c == '#')
				{
					canMove = false;
					break;
				}

				var nextY = checkY + deltaY;

				if (isWide)
				{
					if (c == '[' || c == ']')
					{
						boxes.Add((checkX + (c == '[' ? 0 : -1), checkY));

						if (deltaY == 0)
						{
							queue.Enqueue((checkX + deltaX * 2, checkY));
						}
						else
						{
							queue.Enqueue((checkX, nextY));
							queue.Enqueue((checkX + (c == '[' ? 1 : -1), nextY));
						}
					}
				}
				else if (c == 'O')
				{
					boxes.Add((checkX, checkY));
					queue.Enqueue((checkX + deltaX, nextY));
				}
			}

			if (!canMove)
			{
				continue;
			}

			for (var i = boxes.Count - 1; i >= 0; i--)
			{
				var (boxX, boxY) = boxes[i];
				grid[boxY * stride + boxX] = '.';

				if (isWide)
				{
					grid[boxY * stride + boxX + 1] = '.';

					boxX += deltaX;
					boxY += deltaY;

					grid[boxY * stride + boxX] = '[';
					grid[boxY * stride + boxX + 1] = ']';
				}
				else
				{
					grid[(boxY + deltaY) * stride + boxX + deltaX] = 'O';
				}
			}

			guardX = newGuardX;
			guardY = newGuardY;
		}

		var result = 0;

		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				var pos = y * stride + x;

				if (isWide)
				{
					if (grid[pos] == '[')
					{
						result += y * 100 + x;
						x++;
					}
				}
				else if (grid[pos] == 'O')
				{
					result += y * 100 + x;
				}
			}
		}

		return result;
	}
}
