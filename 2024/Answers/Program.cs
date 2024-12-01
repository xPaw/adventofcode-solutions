using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
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
		BenchmarkAllDays();
		return 0;
	}

	if (args[0] == "test")
	{
		return TestAllDays();
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
		Console.Error.WriteLine("       today 0   - Run only the today's example");
		Console.Error.WriteLine("       20 100    - Run day 20 for 100 times");
		Console.Error.WriteLine("       combined  - Benchmark all days");
		Console.Error.WriteLine("       benchmark - Benchmark today");
		Console.Error.WriteLine("       test      - Test all days");
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
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(day);
Console.ResetColor();

if (day < 1 || day > Solver.AvailableDays)
{
	Console.Error.WriteLine("Wrong day");
	return 1;
}

var data = Solver.Data[day];

if (runs >= 0)
{
	Solver.SolveExample(day);
}
else
{
	runs = 1;
}

Console.WriteLine();

var runTimes = new double[runs];
var min = double.MaxValue;
var max = 0d;

var stopWatch = new Stopwatch();

if (runs > 0)
{
	stopWatch.Restart();
	var solution = Solver.Solve(day, data);
	stopWatch.Stop();

	max = stopWatch.Elapsed.TotalMilliseconds;
	min = max;
	runTimes[0] = max;

	Console.Write("Part 1: ");
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine(solution.Part1);
	Console.ResetColor();

	Console.Write("Part 2: ");
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine(solution.Part2);
	Console.ResetColor();

	if (solution.Part1 == solution.Part2)
	{
		Console.WriteLine("This should never happen, just here so compiler doesn't optimize away");
	}

	var correct = Solver.Answers[day];

	if (correct.Part1 != solution.Part1)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write("Part 1 answer is wrong, expected: ");
		Console.WriteLine(correct.Part1);
		Console.ResetColor();
	}

	if (correct.Part2 != solution.Part2)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write("Part 2 answer is wrong, expected: ");
		Console.WriteLine(correct.Part2);
		Console.ResetColor();
	}
}

for (var i = 1; i < runs; i++)
{
	stopWatch.Restart();
	var (part1, part2) = Solver.Solve(day, data);
	stopWatch.Stop();

	var elapsed = stopWatch.Elapsed.TotalMilliseconds;
	runTimes[i] = elapsed;

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
	Array.Sort(runTimes);

	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:F6}", runTimes[runTimes.Length / 2]);
	Console.ResetColor();
	Console.Write("ms mean for ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write(runs);
	Console.ResetColor();
	Console.WriteLine(" runs");

	Console.Write("Min : ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:F6}", min);
	Console.ResetColor();
	Console.WriteLine("ms");

	Console.Write("Max : ");
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:F6}", max);
	Console.ResetColor();
	Console.WriteLine("ms");
}
else
{
	Console.ForegroundColor = ConsoleColor.Blue;
	Console.Write("{0:F6}", stopWatch.Elapsed.TotalMilliseconds);
	Console.ResetColor();
	Console.Write("ms");
	Console.ForegroundColor = ConsoleColor.DarkGray;
	Console.WriteLine($" ({stopWatch.Elapsed})");
	Console.ResetColor();
}

return 0;

static int TestAllDays()
{
	static void Assert(string expected, string actual)
	{
		if (expected != actual)
		{
			throw new Exception($"Expected '{expected}' but got '{actual}'");
		}
	}

	var exit = 0;
	var stopWatch = Stopwatch.StartNew();

	for (var day = 1; day <= Solver.AvailableDays; day++)
	{
		try
		{
			var correct = Solver.Answers[day];
			var solution = Solver.Solve(day, Solver.Data[day]);

			Assert(correct.Part1, solution.Part1);
			Assert(correct.Part2, solution.Part2);

			correct = Solver.AnswersExample[day];
			solution = Solver.Solve(day, Solver.DataExamples[day]);

			Assert(correct.Part1, solution.Part1);
			Assert(correct.Part2, solution.Part2);
		}
		catch (Exception e)
		{
			exit = 1;

			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine($"Day {day}: {e.Message}");
			Console.ResetColor();
		}
	}

	stopWatch.Stop();

	Console.WriteLine($"Tested in {stopWatch.Elapsed.TotalMilliseconds:F6}ms");

	return exit;
}

static void BenchmarkAllDays()
{
	double combined = 0d;

	Console.WriteLine($"{"Day",-10} {"Runs",-4} {"Time",14}");

	for (var day = 1; day <= Solver.AvailableDays; day++)
	{
		var data = Solver.Data[day];
		var runs = 5000;

		Console.Write($"{day,-10} ");

		var stopWatch = new Stopwatch();
		double total = 0d;

		Solver.Solve(day, data); // warmup

		for (var i = 1; i < runs; i++)
		{
			stopWatch.Restart();
			Solver.Solve(day, data);
			stopWatch.Stop();

			var elapsed = stopWatch.Elapsed.TotalMilliseconds;
			total += elapsed;

			if (elapsed >= 100)
			{
				runs = (int)(runs / 1.5);
			}
			else if (elapsed >= 10)
			{
				runs = (int)(runs / 1.1);
			}
		}

		Console.Write($"{runs,-4} ");

		var time = total / runs;
		combined += time;

		Console.ForegroundColor = time switch
		{
			<= 0.1d => ConsoleColor.Green,
			>= 10d => ConsoleColor.Red,
			>= 1d => ConsoleColor.Yellow,
			_ => ConsoleColor.Blue,
		};

		Console.WriteLine($"{time,14:F6}");
		Console.ResetColor();
	}

	Console.Write($"{"Total",-10} {" ",-4} ");
	Console.ForegroundColor = ConsoleColor.Magenta;
	Console.Write($"{combined,14:F6}");
	Console.ResetColor();
	Console.WriteLine("ms");
}
