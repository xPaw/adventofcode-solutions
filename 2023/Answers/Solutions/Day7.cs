using System;
using System.Collections.Generic;
using System.Linq;

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

	record struct Occurance(char Label, int Count);

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;

		var scores = "23456789TJQKA".AsSpan();
		var hands1 = new List<Hand>();
		var hands2 = new List<Hand>();

		static int GetHandRank(List<Occurance> occurances)
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

			for (i = 0; i < 5; i++)
			{
				var c = hand[i];
				var s = scores.IndexOf(c);

				score1 += score1 * 100 + s;
				score2 += score2 * 100 + (c == 'J' ? 0 : s + 1);
			}

			var occurances = hand
				.ToArray()
				.GroupBy(c => c)
				.Select(c => new Occurance(c.Key, c.Count()))
				.OrderByDescending(c => c.Count)
				.ToList();

			var rank = GetHandRank(occurances);
			hands1.Add(new(bid, rank, score1));

			i = occurances.FindIndex(c => c.Label == 'J');

			if (i > -1)
			{
				if (occurances.Count == 1)
				{
					rank = 6; // JJJJJ
				}
				else
				{
					var jokers = occurances[i].Count;
					occurances.RemoveAt(i);

					occurances[0] = new('J', occurances[0].Count + jokers);

					rank = GetHandRank(occurances);
				}
			}

			hands2.Add(new(bid, rank, score2));
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
