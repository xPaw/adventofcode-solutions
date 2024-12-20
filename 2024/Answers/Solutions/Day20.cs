using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(20)]
public class Day20 : IAnswer
{
	record struct Step(int X, int Y, int Cost);

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var grid = new ReadOnlyGrid(input, '#');
		var finish = grid.IndexOf('E');
		var position = grid.IndexOf('S');
		var last = position;
		var cost = 1;
		var path = new List<Step>(1024 * 10)
		{
			new (position.X, position.Y, 0)
		};

		while (position != finish)
		{
			foreach (var delta in Coord.Directions)
			{
				var pos = position + delta;

				if (pos != last && grid[pos] != '#')
				{
					last = position;
					position = pos;
					path.Add(new Step(pos.X, pos.Y, cost++));
					break;
				}
			}
		}

		for (var i = 0; i < path.Count; i++)
		{
			var step = path[i];

			for (var j = i + 1; j < path.Count; j++)
			{
				var stepEnd = path[j];
				var distance = Math.Abs(stepEnd.Y - step.Y) + Math.Abs(stepEnd.X - step.X);

				if (distance <= 20 && stepEnd.Cost - step.Cost - distance >= 100)
				{
					part2++;

					if (distance == 2)
					{
						part1++;
					}
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
