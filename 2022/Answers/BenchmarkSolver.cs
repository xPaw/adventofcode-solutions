using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
public class BenchmarkSolver
{
	[Params(1, 2)]
	public int Day;

	private string Data = string.Empty;

	[GlobalSetup]
	public async Task Setup()
	{
		Data = await Solver.LoadData(Day);
	}

	[Benchmark]
	public (string Part1, string Part2) Solve()
	{
		return Solver.Solve(Day, Data);
	}
}
