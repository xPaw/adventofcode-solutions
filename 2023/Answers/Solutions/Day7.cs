using System;

namespace AdventOfCode;

[Answer(7)]
public class Day7 : IAnswer
{
	record struct Hand(int Bid, int Rank, int Score) : IComparable<Hand>
	{
		public readonly int CompareTo(Hand other)
		{
			var rank = Rank - other.Rank;
			return rank != 0 ? rank : Score - other.Score;
		}
	}

	class Occurrence(char label, int count) : IComparable<Occurrence>
	{
		public char Label = label;
		public int Count = count;

		public int CompareTo(Occurrence? other) => other!.Count - Count;
	}
	static int GetHandRank(Occurrence[] occurances)
	{
		return occurances[0].Count switch
		{
			5 => 6, // Five of a kind
			4 => 5, // Four of a kind
			3 when occurances[1].Count == 2 => 4, // Full house
			3 => 3, // Three of a kind
			2 when occurances[1].Count == 2 => 2, // Two pair
			2 => 1, // One pair
			_ => 0, // High card
		};
	}

	public Solution Solve(string inputStr)
	{
		var part1 = 0;
		var part2 = 0;

		var input = inputStr.AsSpan();
		var totalHands = input.Count('\n') + 1;
		var hands1 = new Hand[totalHands];
		var hands2 = new Hand[totalHands];
		var hand = 0;

		var occurances = new Occurrence[5] { new(' ', 1), new(' ', 1), new(' ', 1), new(' ', 1), new(' ', 1) };

		foreach (var line in input.EnumerateLines())
		{
			var bid = 0;
			var i = 6;

			do
			{
				bid = bid * 10 + (line[i] - '0');
			}
			while (++i < line.Length);

			var score1 = 0; // AAAAA = 1156111055
			var score2 = 0;
			var unique = 0;

			for (i = 0; i < 5; i++)
			{
				var c = line[i];
				var s = c switch
				{
					'A' => 14,
					'K' => 13,
					'Q' => 12,
					'J' => 11,
					'T' => 10,
					_ => c - '0'
				};

				score1 += score1 * 100 + s;
				score2 += score2 * 100 + (c == 'J' ? 0 : s + 1);

				var notFound = true;

				for (var j = 0; j < unique; j++)
				{
					var o = occurances[j];

					if (o.Label == c)
					{
						o.Count++;
						notFound = false;
						break;
					}
				}

				if (notFound)
				{
					var o = occurances[unique++];
					o.Label = c;
					o.Count = 1;
				}
			}

			Array.Sort(occurances);

			var rank = GetHandRank(occurances);
			hands1[hand] = new(bid, rank, score1);

			if (unique == 1)
			{
				rank = 6; // JJJJJ
			}
			else
			{
				for (i = 0; i < 5; i++)
				{
					var joker = occurances[i];

					if (joker.Label == 'J')
					{
						var jokers = joker.Count;

						// Remove joker if most common
						if (i == 0)
						{
							occurances[0].Count = occurances[1].Count;
							occurances[1].Count = occurances[2].Count;
						}
						// Remove joker if it was second common
						else if (i == 1)
						{
							occurances[1].Count = occurances[2].Count;
						}

						// Add jokers to most common card
						occurances[0].Count += jokers;

						rank = GetHandRank(occurances);

						break;
					}
				}
			}

			hands2[hand] = new(bid, rank, score2);

			for (i = 0; i < unique; i++)
			{
				occurances[i].Label = ' ';
			}

			hand++;
		}

		Array.Sort(hands1);
		Array.Sort(hands2);

		for (var i = 0; i < totalHands; i++)
		{
			part1 += hands1[i].Bid * (i + 1);
			part2 += hands2[i].Bid * (i + 1);
		}

		return new(part1.ToString(), part2.ToString());
	}
}
