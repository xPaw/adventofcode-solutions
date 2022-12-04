namespace AdventOfCode;

[Answer(4)]
public class Day4 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var i = 0;
		var length = input.Length;
		var part1 = 0;
		var part2 = 0;

		int ParseIntUntil(char c)
		{
			var result = 0;

			do
			{
				var t = input[i++];

				if (t == c)
				{
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < length);

			return result;
		}

		while (i < length)
		{
			var a = ParseIntUntil('-');
			var b = ParseIntUntil(',');
			var c = ParseIntUntil('-');
			var d = ParseIntUntil('\n');

			if ((a >= c && b <= d) || (c >= a && d <= b))
			{
				part1++;
			}

			if (b >= c && a <= d)
			{
				part2++;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
