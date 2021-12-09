using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using AdventOfCode2021;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

var day = DateTime.Today.Day;
var runs = 1;

if (args.Length > 0)
{
	if (args[0] != "today" && !int.TryParse(args[0], out day))
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

Console.Write("Day: ");
Console.WriteLine(day);

string part1 = string.Empty;
string part2 = string.Empty;

var data = await Solver.LoadData(day);
var type = Solver.GetSolutionType(day);

await Solver.SolveExample(day, type);

Console.WriteLine();

double total = 0;
double max = 0;
double min = double.MaxValue;

var stopWatch = new Stopwatch();

for (var i = 0; i < runs; i++)
{
	stopWatch.Restart();
	(part1, part2) = Solver.Solve(type, data);
	stopWatch.Stop();

	var elapsed = stopWatch.Elapsed.TotalMilliseconds;
	total += elapsed;

	if (min > elapsed)
	{
		min = elapsed;
	}

	if (max < elapsed)
	{
		max = elapsed;
	}
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

Console.WriteLine();
Console.Write("Time: ");

if (runs > 1)
{
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:N6}", total / runs);
	Console.ResetColor();
	Console.Write("ms average for ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write(runs);
	Console.ResetColor();
	Console.WriteLine(" runs");


	Console.Write("Min : ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:N6}", min);
	Console.ResetColor();
	Console.WriteLine("ms");

	Console.Write("Max : ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:N6}", max);
	Console.ResetColor();
	Console.WriteLine("ms");
}
else
{
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:N6}", stopWatch.Elapsed.TotalMilliseconds);
	Console.ResetColor();
	Console.WriteLine("ms");
}

return 0;
