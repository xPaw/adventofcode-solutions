using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AdventOfCode.Tests;

public class SolutionTests
{
	public static IEnumerable<object[]> GetData()
	{
		Solver.GetAnswers(1).GetAwaiter().GetResult(); // Load answers so Solver.AvailableDays is set

		for (var day = 1; day <= Solver.AvailableDays; day++)
		{
			yield return new object[] { day };
		}
	}

	[Theory]
	[MemberData(nameof(GetData))]
	public async Task TestDay(int day)
	{
		var data = await Solver.LoadData(day);
		var (part1, part2) = Solver.Solve(day, data);

		var (part1answer, part2answer) = await Solver.GetAnswers(day);

		Assert.Equal(part1answer, part1);
		Assert.Equal(part2answer, part2);

		var example = await Solver.SolveExample(day);
		Assert.Equal(example.Item1, example.Item2);
		Assert.Equal(example.Item3, example.Item4);
	}
}
