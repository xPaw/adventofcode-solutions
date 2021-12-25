using System;
using System.Linq;

namespace AdventOfCode2021;

[Answer(25, slow: true)]
class Day25 : IAnswer
{
	enum Type : byte
	{
		Empty,
		East,
		South,
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input
			.Split('\n')
			.Select(l =>
			{
				var numbers = new Type[l.Length];

				for (var i = 0; i < numbers.Length; i++)
				{
					numbers[i] = l[i] switch
					{
						'.' => Type.Empty,
						'>' => Type.East,
						'v' => Type.South,
						_ => throw new NotImplementedException(),
					};
				}

				return numbers;
			})
			.ToArray();

		var part1 = 0;
		var part2 = 0;
		var moving = true;

		do
		{
			part1++;
			moving = false;

			for (var y = 0; y < lines.Length; y++)
			{
				var wasZeroOccupied = false;

				for (var x = 0; x < lines[y].Length; x++)
				{
					var cell = lines[y][x];

					if (x == 0)
					{
						wasZeroOccupied = cell != Type.Empty;
					}

					if (cell == Type.East)
					{
						var newX = (x + 1) % lines[y].Length;

						if (lines[y][newX] == Type.Empty && (newX > 0 || !wasZeroOccupied))
						{
							lines[y][newX] = cell;
							lines[y][x] = Type.Empty;
							x++;
							moving = true;
						}
					}
				}
			}

			for (var x = 0; x < lines[0].Length; x++)
			{
				var wasZeroOccupied = false;

				for (var y = 0; y < lines.Length; y++)
				{
					var cell = lines[y][x];

					if (y == 0)
					{
						wasZeroOccupied = cell != Type.Empty;
					}

					if (cell == Type.South)
					{
						var newY = (y + 1) % lines.Length;

						if (lines[newY][x] == Type.Empty && (newY > 0 || !wasZeroOccupied))
						{
							lines[newY][x] = cell;
							lines[y][x] = Type.Empty;
							y++;
							moving = true;
						}
					}
				}
			}
		}
		while (moving);

		return (part1.ToString(), part2.ToString());
	}
}
