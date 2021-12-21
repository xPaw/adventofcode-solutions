using Xunit;
using System.Threading.Tasks;

namespace AdventOfCode2021.Tests;

public class SolutionTests
{
	[Theory]
	[InlineData(1, "1553", "1597")]
	[InlineData(2, "1728414", "1765720035")]
	[InlineData(3, "693486", "3379326")]
	[InlineData(4, "45031", "2568")]
	[InlineData(5, "6548", "19663")]
	[InlineData(6, "386536", "1732821262171")]
	[InlineData(7, "336721", "91638945")]
	[InlineData(8, "278", "986179")]
	[InlineData(9, "514", "1103130")]
	[InlineData(10, "411471", "3122628974")]
	[InlineData(11, "1713", "502")]
	[InlineData(12, "3679", "107395")]
	[InlineData(13, "770", "\n#### ###  #  # #### #    ###  ###  ###  \n#    #  # #  # #    #    #  # #  # #  # \n###  #  # #  # ###  #    #  # ###  #  # \n#    ###  #  # #    #    ###  #  # ###  \n#    #    #  # #    #    #    #  # # #  \n#### #     ##  #### #### #    ###  #  # \n")]
	[InlineData(14, "2937", "3390034818249")]
	[InlineData(15, "581", "2916")]
	[InlineData(16, "897", "9485076995911")]
	[InlineData(17, "13041", "1031")]
	[InlineData(18, "3699", "4735")]
	[InlineData(19, "381", "12201")]
	[InlineData(20, "5349", "15806")]
	[InlineData(21, "929625", "175731756652760")]
	public async Task TestDay(int day, string answer1, string answer2)
	{
		var data = await Solver.LoadData(day);
		var type = Solver.GetSolutionType(day);
		var (part1, part2) = Solver.Solve(type, data);

		Assert.Equal(answer1, part1);
		Assert.Equal(answer2, part2);

		var example = await Solver.SolveExample(day, type);
		Assert.Equal(example.Item1, example.Item2);
		Assert.Equal(example.Item3, example.Item4);
	}
}
