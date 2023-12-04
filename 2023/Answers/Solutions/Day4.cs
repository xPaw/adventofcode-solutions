using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(4)]
public class Day4 : IAnswer
{
	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var cardId = 0;
		var offset = input.IndexOf(':') + 1;
		var winningNumbers = new HashSet<int>(10);
		var cards = Enumerable.Repeat(1, 200).ToArray();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var i = 0;
			var i2 = offset;
			var parsingWinningNumbers = true;
			var winners = 0;

			do
			{
				i += i2;

				var number = 0;

				do
				{
					i++;
				}
				while (line[i] == ' ');

				if (line[i] == '|')
				{
					parsingWinningNumbers = false;
					i += line[(i + 1)..].IndexOfAnyExcept(' ') + 1;
				}

				do
				{
					number = number * 10 + (line[i] - '0');
				}
				while (++i < line.Length && line[i] != ' ');

				if (parsingWinningNumbers)
				{
					winningNumbers.Add(number);
				}
				else if (winningNumbers.Contains(number))
				{
					winners++;
				}
			}
			while ((i2 = line[i..].IndexOf(' ')) != -1);

			winningNumbers.Clear();

			var previousCards = cards[cardId];

			if (winners > 0)
			{
				part1 += 1 << (winners - 1);

				for (var c = cardId + 1; c <= cardId + winners; c++)
				{
					cards[c] += previousCards;
				}
			}

			part2 += previousCards;

			cardId++;
		}

		return new(part1.ToString(), part2.ToString());
	}
}
