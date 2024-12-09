using System.Collections.Generic;

namespace AdventOfCode;

[Answer(9)]
public class Day9 : IAnswer
{
	record struct File(int ID, int Blocks);
	public Solution Solve(string input)
	{
		var part1 = 0L;
		var part2 = 0L;
		var id = 0;
		var blocks = new List<int>(input.Length);

		for (var i = 0; i < input.Length; i++)
		{
			var number = input[i] - '0';
			var w = i % 2 == 0 ? id++ : -1;

			while (number-- > 0)
			{
				blocks.Add(w);
			}
		}

		var blocks2 = new List<int>(blocks);

		// p1
		for (var i = blocks.Count - 1; i >= 0; i--)
		{
			if (blocks[i] == -1)
			{
				continue;
			}

			var offset = blocks.IndexOf(-1, 0, i);

			if (offset < 0)
			{
				break;
			}

			blocks[offset] = blocks[i];
			blocks[i] = -1;
		}

		for (var i = 0; i < blocks.Count; i++)
		{
			if (blocks[i] == -1)
			{
				break;
			}

			part1 += i * blocks[i];
		}

		// p2
		for (var i = blocks2.Count - 1; i >= 0; i--)
		{
			var num = blocks2[i];
			if (num == -1)
			{
				continue;
			}

			var length = 1;
			while (i > 0 && num == blocks2[i - 1])
			{
				length++;
				i--;
			}

			var offset = 0;

			while (true)
			{
				offset = blocks2.IndexOf(-1, offset + 1, i - offset);

				if (offset < 0)
				{
					break;
				}

				var freeSpaceNeeded = length;

				while (--freeSpaceNeeded > 0)
				{
					if (blocks2[++offset] > -1)
					{
						break;
					}
				}

				if (freeSpaceNeeded == 0)
				{
					break;
				}
			}

			if (offset < 0)
			{
				continue;
			}

			var k = i;

			for (var j = offset - length + 1; j <= offset; j++)
			{
				blocks2[j] = blocks2[k];
				blocks2[k++] = -1;
			}
		}

		for (var i = 0; i < blocks2.Count; i++)
		{
			if (blocks2[i] == -1)
			{
				continue;
			}

			part2 += i * blocks2[i];
		}

		return new(part1.ToString(), part2.ToString());
	}
}
