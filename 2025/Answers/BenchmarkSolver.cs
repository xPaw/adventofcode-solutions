#if !NO_BENCHMARK
using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
public class BenchmarkSolver
{
	//[Params(1)]
	public int Day { get; set; }

	private string Data = string.Empty;

	[GlobalSetup]
	public void Setup()
	{
		Day = System.DateTime.Today.Day;
		Data = Solver.Data[Day];
	}

	[Benchmark]
	public Solution Solve()
	{
		return Solver.Solve(Day, Data);
	}
}
#endif
