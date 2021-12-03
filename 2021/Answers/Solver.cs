using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode2021;

public class Solver
{
	public static async Task<string> LoadData(int day)
	{
		var data = await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/day{day}.txt");
		return data.Trim();
	}

	public static (string Part1, string Part2) Solve(Type type, string data)
	{
		var solution = (IAnswer)Activator.CreateInstance(type)!;
		return solution.Solve(data);
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
