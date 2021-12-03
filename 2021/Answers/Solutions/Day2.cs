using System;
using System.Linq;

namespace AdventOfCode2021;

[Answer(2)]
class Day2 : IAnswer
{
	enum Direction
	{
		Forward,
		Up,
		Down,
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input
			.Split('\n')
			.Select(l =>
			{
				var sp = l.Split(' ', 2);
				var dir = sp[0] switch
				{
					"forward" => Direction.Forward,
					"up" => Direction.Up,
					"down" => Direction.Down,
					_ => throw new NotImplementedException(),
				};

				return (Direction: dir, Units: int.Parse(sp[1]));
			});

		var horizontal = 0;
		var depth = 0; // part 2
		var aim = 0;

		foreach (var (direction, units) in lines)
		{
			switch (direction)
			{
				case Direction.Forward:
					horizontal += units;
					depth += units * aim;
					break;
				case Direction.Up: aim -= units; break;
				case Direction.Down: aim += units; break;
			}
		}

		var part1 = horizontal * aim;
		var part2 = horizontal * depth;

		return (part1.ToString(), part2.ToString());
	}
}
