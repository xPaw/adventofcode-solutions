using System;

namespace AdventOfCode2021;

interface IAnswer
{
	public (string Part1, string Part2) Solve(string input);
}
