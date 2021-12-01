using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using AdventOfCode2021;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

var day = DateTime.Today.Day;
var runs = 1;

if (args.Length > 0)
{
	if (!int.TryParse(args[0], out day))
	{
		Console.Error.WriteLine("Usage: [day [runs]]");
		return 1;
	}

	if (args.Length > 1)
	{
		if (!int.TryParse(args[1], out runs))
		{
			Console.Error.WriteLine("Usage: <day> [runs]");
			return 1;
		}
	}
}

string part1 = string.Empty;
string part2 = string.Empty;
var data = await File.ReadAllTextAsync($"Data/day{day}.txt");

var solution = GetSolutionClass(day);

var stopWatch = new Stopwatch();
stopWatch.Start();

for (var i = 0; i < runs; i++)
{
	(part1, part2) = solution.Solve(data);
}

stopWatch.Stop();

Console.Write("Part 1: ");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(part1);
Console.ResetColor();

Console.Write("Part 2: ");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(part2);
Console.ResetColor();

Console.Write("Time  : ");

if (runs > 1)
{
	var average = new TimeSpan(stopWatch.Elapsed.Ticks / runs);

	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write(average.Duration());
	Console.ResetColor();
	Console.Write(" average for ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write(runs);
	Console.ResetColor();
	Console.WriteLine(" runs");
}
else
{
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.WriteLine(stopWatch.Elapsed.Duration());
	Console.ResetColor();
}

return 0;

static IAnswer GetSolutionClass(int day)
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
				return (IAnswer)Activator.CreateInstance(type)!;
			}
		}
	}

	throw new Exception($"Unable to find solution for day {day}");
}
