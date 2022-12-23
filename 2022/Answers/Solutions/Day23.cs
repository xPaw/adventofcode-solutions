using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode;

[Answer(23)]
public class Day23 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var map = new HashSet<Vector2>();
		var x = 0;
		var y = 0;

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == '#')
			{
				map.Add(new(x, y));
			}

			if (input[i] == '\n')
			{
				y++;
				x = 0;
			}
			else
			{
				x++;
			}
		}

		var parallels = new[]
		{
			new Vector2(0, -1),
			new Vector2(0, 1),
			new Vector2(-1, 0),
			new Vector2(1, 0),
		};
		var diagonals = new[]
		{
			new Vector2(-1, -1),
			new Vector2(1, -1),

			new Vector2(-1, 1),
			new Vector2(1, 1),

			new Vector2(-1, -1),
			new Vector2(-1, 1),

			new Vector2(1, -1),
			new Vector2(1, 1),
		};

		var part1 = 0f;
		var part2 = 0;
		var round = 1;
		var direction = 0;
		var proposedMoves = new Dictionary<Vector2, Vector2?>();

		while (true)
		{
			proposedMoves.Clear();

			foreach (var elf in map)
			{
				if (!parallels.Any(move => map.Contains(elf + move)) && !diagonals.Any(move => map.Contains(elf + move)))
				{
					continue;
				}

				for (var i = 0; i < 4; i++)
				{
					var dir = (direction + i) % 4;
					var to = elf + parallels[dir];

					if (!map.Contains(to)
					&& !map.Contains(elf + diagonals[dir * 2])
					&& !map.Contains(elf + diagonals[(dir * 2) + 1]))
					{
						if (proposedMoves.ContainsKey(to))
						{
							proposedMoves[to] = null;
						}
						else
						{
							proposedMoves[to] = elf;
						}

						break;
					}
				}
			}

			var moved = false;

			foreach (var (to, from) in proposedMoves)
			{
				if (from != null)
				{
					map.Remove(from.Value);
					map.Add(to);
					moved = true;
				}
			}

			if (round == 10)
			{
				var minX = float.MaxValue;
				var minY = float.MaxValue;
				var maxX = float.MinValue;
				var maxY = float.MinValue;

				foreach (var elf in map)
				{
					if (minX > elf.X) minX = elf.X;
					if (minY > elf.Y) minY = elf.Y;
					if (maxX < elf.X) maxX = elf.X;
					if (maxY < elf.Y) maxY = elf.Y;
				}

				part1 = (maxX - minX + 1) * (maxY - minY + 1) - map.Count;
			}

			if (!moved)
			{
				part2 = round;
				break;
			}

			direction++;
			round++;
		}

		return (part1.ToString(), part2.ToString());
	}
}
