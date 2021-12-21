using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(21)]
class Day21 : IAnswer
{
	readonly Dictionary<int, (long, long)> Cache = new();
	readonly (int, int)[] QuantumRolls = new[]
	{
		(3, 1),
		(4, 3),
		(5, 6),
		(6, 7),
		(7, 6),
		(8, 3),
		(9, 1),
	};

	public (string Part1, string Part2) Solve(string input)
	{
		var player1 = input[28] - '0';
		var player2 = input[58] - '0';

		var part1 = Part1(player1, player2);
		var part2 = Part2(player1, player2);

		return (part1.ToString(), part2.ToString());
	}

	int Part1(int player1, int player2)
	{
		var score1 = 0;
		var score2 = 0;
		var deterministicDice = 0;
		var rolls = 0;

		var roll = (int player) =>
		{
			rolls++;
			deterministicDice = deterministicDice % 100 + 1;
			player = (player + deterministicDice - 1) % 10 + 1;

			return player;
		};

		while (true)
		{
			player1 = roll(player1);
			player1 = roll(player1);
			player1 = roll(player1);
			score1 += player1;

			if (score1 >= 1000)
			{
				return score2 * rolls;
			}

			player2 = roll(player2);
			player2 = roll(player2);
			player2 = roll(player2);
			score2 += player2;

			if (score2 >= 1000)
			{
				return score1 * rolls;
			}
		}
	}

	long Part2(int player1, int player2)
	{
		var (score1, score2) = Part2Roll(player1, player2, 0, 0);

		return score1 > score2 ? score1 : score2;
	}

	(long, long) Part2Roll(int player1, int player2, int score1, int score2)
	{
		var index = ((player1 * 100 + player2) * 100 + score1) * 100 + score2;

		if (Cache.TryGetValue(index, out var cached))
		{
			return cached;
		}

		long result1 = 0;
		long result2 = 0;

		foreach (var (sum, multiplier) in QuantumRolls)
		{
			var player = (player1 + sum - 1) % 10 + 1;
			var newScore = score1 + player;

			if (newScore >= 21)
			{
				result1 += multiplier;
				continue;
			}

			var (score3, score4) = Part2Roll(player2, player, score2, newScore);

			result1 += score4 * multiplier;
			result2 += score3 * multiplier;
		}

		var score = (result1, result2);

		Cache.Add(index, score);

		return score;
	}
}
