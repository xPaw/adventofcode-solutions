using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2021;

public class Solver
{
	public static async Task<string> LoadData(int day)
	{
		var data = await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/Data/day{day}.txt");
		return data.Trim();
	}

	public static (string Part1, string Part2) Solve(Type type, string data)
	{
		var solution = (IAnswer)Activator.CreateInstance(type)!;
		return solution.Solve(data);
	}

	public static async Task<(string, string, string, string)> SolveExample(int day, Type type)
	{
		var data = (await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/DataExamples/day{day}.txt")).Trim();
		var solution = (IAnswer)Activator.CreateInstance(type)!;

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
		var data = await File.ReadAllLinesAsync($"{AppDomain.CurrentDomain.BaseDirectory}/DataExamples/answers.txt");
		var answers = data[day - 1].Split(" | ");

		var GetAnswer = (int id) =>
		{
			return answers[id].Replace("\\n", "\n");
		};

		return (GetAnswer(0), GetAnswer(1));
	}

	public static Type GetSolutionType(int day)
	{
		foreach (Type type in typeof(AnswerAttribute).Assembly.GetTypes())
		{
			if (!typeof(IAnswer).IsAssignableFrom(type))
			{
				continue;
			}

			var attributes = type.GetCustomAttributes(typeof(AnswerAttribute), true);

			foreach (AnswerAttribute attribute in attributes)
			{
				if (attribute.Day == day)
				{
					return type;
				}
			}
		}

		throw new Exception($"Unable to find solution for day {day}");
	}
}
