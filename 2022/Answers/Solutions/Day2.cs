namespace AdventOfCode;

[Answer(2)]
public class Day2 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		static int Score(int a, int b)
		{
			var result = (3 + b - a) % 3;
			return b switch
			{
				2 => 1,
				1 => 2,
				0 => 3,
				_ => throw new System.NotImplementedException(),
			} + result switch
			{
				0 => 3,
				1 => 0,
				2 => 6,
				_ => throw new System.NotImplementedException(),
			};
		}

		for (var i = 0; i < input.Length; i += 4)
		{
			var a = 'C' - input[i];
			var b = 'Z' - input[i + 2];

			part1 += Score(a, b);

			b = input[i + 2] switch
			{
				'X' => (a + 1) % 3,
				'Y' => a,
				'Z' => (((a - 1) % 3) + 3) % 3,
				_ => throw new System.NotImplementedException(),
			};

			part2 += Score(a, b);
		}

		return (part1.ToString(), part2.ToString());
	}
}
