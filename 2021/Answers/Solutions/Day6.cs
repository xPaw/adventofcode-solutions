using System;
using System.Linq;

namespace AdventOfCode2021;

[Answer(6)]
class Day6 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var timers = new long[9];

		for (var i = input.Length - 1; i >= 0; i -= 2)
		{
			timers[input[i] - '0']++;
		}

		for (var day = 1; day <= 80; day++) Update(timers);
		var part1 = timers.Sum();

		for (var day = 81; day <= 256; day++) Update(timers);
		var part2 = timers.Sum();

		return (part1.ToString(), part2.ToString());
	}

	private void Update(long[] timers)
	{
		var newFish = timers[0];

		for (var timer = 0; timer < 8; timer++)
		{
			timers[timer] = timers[timer + 1];
		}

		timers[8] = newFish;
		timers[6] += newFish;
	}
}
