using System;

namespace AdventOfCode;

public interface IAnswer
{
	public (string Part1, string Part2) Solve(string input);
}
