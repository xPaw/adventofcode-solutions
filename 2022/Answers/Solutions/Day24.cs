using System.Collections.Generic;

namespace AdventOfCode;

[Answer(24)]
public class Day24 : IAnswer
{
	enum State : byte
	{
		Wall,
		Up,
		Down,
		Left,
		Right,
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var width = input.IndexOf('\n');
		var height = (input.Length + 1) / (width + 1);

		var map = new List<(int X, int Y, State)>();

		var x = 0;
		var y = 0;

		var startY = 0;
		var startX = 0;
		var finishX = 0;
		var finishY = height - 1;

		for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == '\n')
			{
				x = 0;
				y++;
				continue;
			}

			switch (input[i])
			{
				case '#': map.Add((x, y, State.Wall)); break;
				case '>': map.Add((x, y, State.Right)); break;
				case '<': map.Add((x, y, State.Left)); break;
				case 'v': map.Add((x, y, State.Down)); break;
				case '^': map.Add((x, y, State.Up)); break;
			}

			if (y == 0 && input[i] == '.')
			{
				startX = x;
			}
			else if (y == height - 1 && input[i] == '.')
			{
				finishX = x;
			}

			x++;
		}

		int Mod(int x, int m)
		{
			int r = x % m;
			return r < 0 ? r + m : r;
		}

		HashSet<int> BlizzardsAtTime(int time)
		{
			var occupied = new HashSet<int>(map.Count);

			foreach (var (x, y, state) in map)
			{
				if (state == State.Right)
				{
					var currentX = ((x - 1 + time) % (width - 2)) + 1;
					occupied.Add(y * 100_000_000 + currentX);
				}
				else if (state == State.Left)
				{
					var currentX = Mod(x - 1 - time, width - 2) + 1;
					occupied.Add(y * 100_000_000 + currentX);
				}
				else if (state == State.Down)
				{
					var currentY = ((y - 1 + time) % (height - 2)) + 1;
					occupied.Add(currentY * 100_000_000 + x);
				}
				else if (state == State.Up)
				{
					var currentY = Mod(y - 1 - time, height - 2) + 1;
					occupied.Add(currentY * 100_000_000 + x);
				}
				else
				{
					occupied.Add(y * 100_000_000 + x);
				}
			}

			return occupied;
		}

		var possibleMoves = new (int X, int Y)[]
		{
			(0, 0),
			(0, -1),
			(0, 1),
			(-1, 0),
			(1, 0),
		};

		int Simulate(int startX, int startY, int finishX, int finishY, int time)
		{
			var current = new HashSet<(int X, int Y)>
			{
				(startX, startY)
			};
			var next = new HashSet<(int X, int Y)>();

			while (true)
			{
				var occupied = BlizzardsAtTime(time);

				foreach (var (x, y) in current)
				{
					if (x == finishX && y == finishY)
					{
						return time - 1;
					}

					foreach (var (moveX, moveY) in possibleMoves)
					{
						var newX = x + moveX;
						var newY = y + moveY;

						if (newY >= 0 && newY < height && !occupied.Contains(newY * 100_000_000 + newX))
						{
							next.Add((newX, newY));
						}
					}
				}

				time++;
				current = next;
				next = new HashSet<(int X, int Y)>();
			}
		}

		var part1 = Simulate(startX, startY, finishX, finishY, 1);
		var back = Simulate(finishX, finishY, startX, startY, part1);
		var part2 = Simulate(startX, startY, finishX, finishY, back);

		return (part1.ToString(), part2.ToString());
	}
}
