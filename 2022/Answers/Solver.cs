using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode;

public partial class Solver
{
	private static string[]? Answers;
	private static string[]? AnswersExample;

	public static int AvailableDays => Answers?.Length ?? 0;

	public static async Task<string> LoadData(int day)
	{
		var data = await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/Data/day{day}.txt");
		return data.TrimEnd();
	}

	private static partial IAnswer CreateSolutionInstance(int day);

	public static (string Part1, string Part2) Solve(int day, string data)
	{
		var solution = CreateSolutionInstance(day);
		return solution.Solve(data);
	}

	public static async Task<(string, string, string, string)> SolveExample(int day)
	{
		var data = (await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/DataExamples/day{day}.txt")).TrimEnd();
		var solution = CreateSolutionInstance(day);

		var (part1, part2) = solution.Solve(data);

		Console.ForegroundColor = ConsoleColor.DarkBlue;
		Console.Write("Example solution: ");
		Console.Write(part1);
		Console.Write(" | ");
		Console.WriteLine(part2);
		Console.ResetColor();

		var (correctPart1, correctPart2) = await GetExampleAnswers(day);

		if (correctPart1 != part1)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Part 1 example answer is wrong, expected: ");
			Console.WriteLine(correctPart1);
			Console.ResetColor();
		}

		if (correctPart2 != part2)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Part 2 example answer is wrong, expected: ");
			Console.WriteLine(correctPart2);
			Console.ResetColor();
		}

		return (correctPart1, part1, correctPart2, part2);
	}

	public static async Task<(string Part1, string Part2)> GetExampleAnswers(int day)
	{
		AnswersExample ??= await File.ReadAllLinesAsync($"{AppDomain.CurrentDomain.BaseDirectory}/DataExamples/answers.txt");

		return GetAnswers(day, AnswersExample);
	}

	public static async Task<(string Part1, string Part2)> GetAnswers(int day)
	{
		Answers ??= await File.ReadAllLinesAsync($"{AppDomain.CurrentDomain.BaseDirectory}/Data/answers.txt");

		return GetAnswers(day, Answers);
	}

	private static (string Part1, string Part2) GetAnswers(int day, string[] days)
	{
		var answers = days[day - 1].Split(" | ");

		var GetAnswer = (int id) =>
		{
			return answers[id].Replace("\\n", "\n");
		};

		return (GetAnswer(0), GetAnswer(1));
	}
}
