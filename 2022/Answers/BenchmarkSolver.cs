#if !NO_BENCHMARK
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
public class BenchmarkSolver
{
	[Params(15)]
	public int Day { get; set; }

	private string Data = string.Empty;

	[GlobalSetup]
	public async Task Setup()
	{
		//Day = System.DateTime.Today.Day;
		Data = await Solver.LoadData(Day);
	}

	[Benchmark]
	public (string Part1, string Part2) Solve()
	{
		return Solver.Solve(Day, Data);
	}
}
#endif
