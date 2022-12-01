namespace AdventOfCode;

[Answer(1)]
public class Day1 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var length = input.Length;
		var current = 0;
		var num = 0;
		var a = 0;
		var b = 0;
		var c = 0;

		void Swap(int x)
		{
			if (a < x)
			{
				c = b;
				b = a;
				a = x;
			}
			else if (b < x)
			{
				c = b;
				b = x;
			}
			else if (c < x)
			{
				c = x;
			}
		}

		for (var i = 0; i < length; i++)
		{
			if (input[i] == '\n')
			{
				if (num > 0)
				{
					current += num;
					num = 0;
				}
				else
				{
					Swap(current);
					current = 0;
				}
			}
			else
			{
				num = 10 * num + input[i] - '0';
			}
		}

		Swap(current + num);

		var part1 = a;
		var part2 = a + b + c;

		return (part1.ToString(), part2.ToString());
	}
}
