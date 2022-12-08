namespace AdventOfCode;

[Answer(8)]
public class Day8 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var width = 0;
		var height = 1;

		for (var a = 0; a < input.Length; a++)
		{
			if (input[a] == '\n')
			{
				height++;

				if (width == 0)
				{
					width = a;
				}
			}
		}

		var part1 = width + width + height + height - 4;
		var part2 = 0;

		char Tree(int x, int y) => input[y * (width + 1) + x];

		for (var x = 1; x < width - 1; x++)
		{
			for (var y = 1; y < height - 1; y++)
			{
				var tree = Tree(x, y);
				var visible = 4;
				var score1 = 0;
				var score2 = 0;
				var score3 = 0;
				var score4 = 0;

				for (var a = x - 1; a >= 0; a--)
				{
					score1++;

					if (Tree(a, y) >= tree)
					{
						visible--;
						break;
					}
				}

				for (var a = x + 1; a < width; a++)
				{
					score2++;

					if (Tree(a, y) >= tree)
					{
						visible--;
						break;
					}
				}

				for (var a = y - 1; a >= 0; a--)
				{
					score3++;

					if (Tree(x, a) >= tree)
					{
						visible--;
						break;
					}
				}

				for (var a = y + 1; a < height; a++)
				{
					score4++;

					if (Tree(x, a) >= tree)
					{
						visible--;
						break;
					}
				}

				if (visible > 0)
				{
					part1++;
				}

				var totalScore = score1 * score2 * score3 * score4;

				if (part2 < totalScore)
				{
					part2 = totalScore;
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
