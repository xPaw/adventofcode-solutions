using System;

namespace AdventOfCode;

[Answer(4)]
public class Day4 : IAnswer
{
	public Solution Solve(string inputStr)
	{
		var input = inputStr.AsSpan();
		var part1 = 0;
		var part2 = 0;

		var offset = input.IndexOf(':') + 2;
		var offsetSplit = input.IndexOf('|') + 2;
		var offsetLine = input.IndexOf('\n') + 1;

		var winnersCount = (offsetSplit - offset) / 3;
		var guessesCount = (offsetLine - (offsetSplit - 2)) / 3;
		var winningNumbers = new int[winnersCount].AsSpan();

		var cardCount = input.Length / offsetLine;
		var cards = new int[cardCount + winnersCount];
		cards.AsSpan().Fill(1);

		static int GetNumber(ReadOnlySpan<char> line)
		{
			var a = line[0] - '0';
			var b = line[1] - '0';

			return a < 0 ? b : a * 10 + b;
		}

		for (int cardId = 0; cardId <= cardCount; cardId++)
		{
			var winners = 0;
			var off = offsetLine * cardId + offset;

			for (var i = 0; i < winnersCount; i++)
			{
				winningNumbers[i] = GetNumber(input[off..]);
				off += 3;
			}

			off += 2;

			for (var i = 0; i < guessesCount; i++)
			{
				var number = GetNumber(input[off..]);
				off += 3;

				if (winningNumbers.Contains(number))
				{
					winners++;
				}
			}

			var previousCards = cards[cardId];
			part2 += previousCards;

			if (winners > 0)
			{
				part1 += 1 << (winners - 1);

				for (var c = cardId + 1; c <= cardId + winners; c++)
				{
					cards[c] += previousCards;
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
