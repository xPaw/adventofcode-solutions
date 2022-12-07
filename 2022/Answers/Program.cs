using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode;

#if !NO_BENCHMARK
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
#endif

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

var day = DateTime.Today.Day;
var runs = 1;

if (args.Length > 0)
{
	if (args[0] == "combined")
	{
		await BenchmarkAllDays();
		return 0;
	}

#if !NO_BENCHMARK
	if (args[0] == "benchmark")
	{
		var config = ManualConfig.Create(DefaultConfig.Instance)
			.WithSummaryStyle(SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond));
		BenchmarkRunner.Run<BenchmarkSolver>(config);
		return 0;
	}
#endif

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

var data = await Solver.LoadData(day);

await Solver.SolveExample(day);

Console.WriteLine();

double total = 0;
double max = 0;
double min = double.MaxValue;

var stopWatch = new Stopwatch();

if (runs > 0)
{
	stopWatch.Restart();
	var (part1, part2) = Solver.Solve(day, data);
	stopWatch.Stop();
	total += stopWatch.Elapsed.TotalMilliseconds;

	Console.Write("Part 1: ");
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine(part1);
	Console.ResetColor();

	Console.Write("Part 2: ");
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine(part2);
	Console.ResetColor();

	if (part1 == part2)
	{
		Console.WriteLine("This should never happen, just here so compiler doesn't optimize away");
	}

	var (correctPart1, correctPart2) = await Solver.GetAnswers(day);

	if (correctPart1 != part1)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write("Part 1 answer is wrong, expected: ");
		Console.WriteLine(correctPart1);
		Console.ResetColor();
	}

	if (correctPart2 != part2)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write("Part 2 answer is wrong, expected: ");
		Console.WriteLine(correctPart2);
		Console.ResetColor();
	}
}

for (var i = 1; i < runs; i++)
{
	stopWatch.Restart();
	var (part1, part2) = Solver.Solve(day, data);
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

static async Task BenchmarkAllDays()
{
	double combined = 0d;

	Console.WriteLine($"{"Day",-10} {"Runs",-10} Time");

	for (var day = 1; day <= 25; day++)
	{
		var data = await Solver.LoadData(day);
		//var type = Solver.GetSolutionType(day);
		//var attribute = (AnswerAttribute)type.GetCustomAttributes(typeof(AnswerAttribute), true)[0];
		//var runs = attribute.Slow ? 100 : 5000;
		var runs = 100;

		Console.Write($"{day,-10} {runs,-10} ");

		var stopWatch = new Stopwatch();
		double total = 0d;

		for (var i = 1; i < runs; i++)
		{
			stopWatch.Restart();
			Solver.Solve(day, data);
			stopWatch.Stop();

			var elapsed = stopWatch.Elapsed.TotalMilliseconds;
			total += elapsed;
		}

		var time = total / runs;
		combined += time;

		if (time >= 1.0d)
		{
			Console.ForegroundColor = ConsoleColor.Red;
		}
		else if (time <= 0.1d)
		{
			Console.ForegroundColor = ConsoleColor.Green;
		}

		Console.WriteLine($"{time:N6}");
		Console.ResetColor();
	}

	Console.Write($"{"Total",-10} {"-",-10} ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write($"{combined:N6}");
	Console.ResetColor();
	Console.WriteLine("ms");
}
