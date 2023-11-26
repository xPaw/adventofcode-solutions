using System;
using System.Diagnostics;

namespace AdventOfCode;

public partial class Solver
{
	public static int AvailableDays => Answers?.Length ?? 0;

	private static partial IAnswer CreateSolutionInstance(int day);

	public static Solution Solve(int day, string data)
	{
		var solution = CreateSolutionInstance(day);
		return solution.Solve(data);
	}

	public static (string, string, string, string) SolveExample(int day)
	{
		var daySolver = CreateSolutionInstance(day);

		var stopWatch = new Stopwatch();
		stopWatch.Start();
		var solution = daySolver.Solve(DataExamples[day]);
		stopWatch.Stop();

		Console.Write("Example solution: ");
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		Console.Write(solution.Part1);
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write(" | ");
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		Console.Write(solution.Part2);
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.WriteLine($" ({stopWatch.Elapsed})");
		Console.ResetColor();

		var correctSolution = AnswersExample[day];

		if (correctSolution.Part1 != solution.Part1)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Part 1 example answer is wrong, expected: ");
			Console.WriteLine(correctSolution.Part1);
			Console.ResetColor();
		}

		if (correctSolution.Part2 != solution.Part2)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Part 2 example answer is wrong, expected: ");
			Console.WriteLine(correctSolution.Part2);
			Console.ResetColor();
		}

		return (correctSolution.Part1, solution.Part1, correctSolution.Part2, solution.Part2);
	}
}
