using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(7)]
public class Day7 : IAnswer
{
	record struct Hand(int Bid, int Rank, int Score) : IComparable<Hand>
	{
		public readonly int CompareTo(Hand other)
		{
			var rank = Rank.CompareTo(other.Rank);

			if (rank != 0)
			{
				return rank;
			}

			return Score.CompareTo(other.Score);
		}
	}

	class Occurance(char label, int count) : IComparable<Occurance>
	{
		public char Label = label;
		public int Count = count;

		public int CompareTo(Occurance? other) => other!.Count.CompareTo(Count);
	}

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var scores = "23456789TJQKA".AsSpan();
		var hands1 = new List<Hand>(1000);
		var hands2 = new List<Hand>(1000);

		static int GetHandRank(Occurance[] occurances)
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

		var occurances = new Occurance[5] { new(' ', 1), new(' ', 1), new(' ', 1), new(' ', 1), new(' ', 1) };

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var hand = line[0..5];

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
				var c = hand[i];
				var s = scores.IndexOf(c);

				score1 += score1 * 100 + s;
				score2 += score2 * 100 + (c == 'J' ? 0 : s + 1);

				Occurance? existing = null;

				for (var j = 0; j < 5; j++)
				{
					if (occurances[j].Label == c)
					{
						existing = occurances[j];
						break;
					}
				}

				if (existing == null)
				{
					var o = occurances[unique++];
					o.Label = c;
					o.Count = 1;
				}
				else
				{
					existing.Count++;
				}
			}

			Array.Sort(occurances);

			var rank = GetHandRank(occurances);
			hands1.Add(new(bid, rank, score1));

			i = Array.FindIndex(occurances, static c => c.Label == 'J');

			if (i > -1)
			{
				if (unique == 1)
				{
					rank = 6; // JJJJJ
				}
				else
				{
					var jokers = occurances[i].Count;

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
				}
			}

			hands2.Add(new(bid, rank, score2));

			for (i = 0; i < 5; i++)
			{
				occurances[i].Label = ' ';
			}
		}

		hands1.Sort();
		hands2.Sort();

		for (var i = 0; i < hands1.Count; i++)
		{
			part1 += hands1[i].Bid * (i + 1);
			part2 += hands2[i].Bid * (i + 1);
		}

		return new(part1.ToString(), part2.ToString());
	}
}
