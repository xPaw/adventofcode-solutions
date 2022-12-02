using Xunit;
using System.Threading.Tasks;

namespace AdventOfCode.Tests;

public class SolutionTests
{
	[Theory]
	[InlineData(1, "70698", "206643")]
	[InlineData(2, "10310", "14859")]
	public async Task TestDay(int day, string answer1, string answer2)
	{
		var data = await Solver.LoadData(day);
		var (part1, part2) = Solver.Solve(day, data);

		Assert.Equal(answer1, part1);
		Assert.Equal(answer2, part2);

		var example = await Solver.SolveExample(day);
		Assert.Equal(example.Item1, example.Item2);
		Assert.Equal(example.Item3, example.Item4);
	}
}