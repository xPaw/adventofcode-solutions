using Xunit;
using System.Collections.Generic;

namespace AdventOfCode.Tests;

public class SolutionTests
{
	public static IEnumerable<object[]> GetData()
	{
		for (var day = 1; day <= Solver.AvailableDays; day++)
		{
			yield return new object[] { day };
		}
	}

	[Theory]
	[MemberData(nameof(GetData))]
	public void TestDay(int day)
	{
		var correct = Solver.Answers[day];
		var solution = Solver.Solve(day, Solver.Data[day]);

		Assert.Equal(correct.Part1, solution.Part1);
		Assert.Equal(correct.Part2, solution.Part2);

		var example = Solver.SolveExample(day);
		Assert.Equal(example.Item1, example.Item2);
		Assert.Equal(example.Item3, example.Item4);
	}
}
