using System;
using System.Linq;

namespace AdventOfCode;

[Answer(6)]
public class Day6 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 1L;
		var part2 = 0L;

		var lines = input.Split('\n');
		var times = lines[0]["Time: ".Length..].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToArray();
		var dists = lines[1]["Distance: ".Length..].Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToArray();

		static long Solve(long time, long dist)
		{
			var firstWinningTime = (long)Math.Ceiling((time - Math.Sqrt(time * time - 4 * (dist + 1))) / 2);
			return time - firstWinningTime * 2 + 1;
		}

		for (var i = 0; i < times.Length; i++)
		{
			var time = times[i];
			var dist = dists[i];
			part1 *= Solve(time, dist);
		}

		{
			var time = long.Parse(string.Concat(times));
			var dist = long.Parse(string.Concat(dists));
			part2 = Solve(time, dist);
		}

		return new(part1.ToString(), part2.ToString());
	}
}
