using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(4)]
class Day4 : IAnswer
{
	record BoardNumber
	{
		public int Number { get; init; }
		public bool Marked { get; set; }
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var inputs = input.Split("\n\n");
		var numbers = inputs[0]
			.Split(',')
			.Select(l => int.Parse(l))
			.ToArray();
		var boards = new BoardNumber[inputs.Length - 1][][];
		var boardsWon = new bool[inputs.Length - 1];

		for (var i = 1; i < inputs.Length; i++)
		{
			var boardLines = inputs[i].Split("\n");
			var board = new BoardNumber[boardLines.Length][];
			var y = 0;

			foreach (var row in boardLines)
			{
				board[y++] = row
					.TrimStart()
					.Replace("  ", " ")
					.Split(' ')
					.Select(l => new BoardNumber
					{
						Number = int.Parse(l)
					})
					.ToArray();
			}

			boards[i - 1] = board;
		}

		var part1 = 0;
		var part2 = 1;

		foreach (var number in numbers)
		{
			for (var i = 0; i < boards.Length; i++)
			{
				if (boardsWon[i])
				{
					continue;
				}

				var board = boards[i];
				foreach (var row in board)
				{
					foreach (var rowNumber in row)
					{
						if (rowNumber.Number == number)
						{
							rowNumber.Marked = true;
						}
					}
				}

				if (IsBingo(board))
				{
					boardsWon[i] = true;

					if (part1 == 0)
					{
						part1 = GetScore(board) * number;
					}
					else if (!boardsWon.Any(x => !x))
					{
						part2 = GetScore(board) * number;
					}
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}

	bool IsBingo(BoardNumber[][] board)
	{
		for (var x = 0; x < board.Length; x++)
		{
			var horizontal = 0;
			var vertical = 0;

			for (var y = 0; y < board.Length; y++)
			{
				if (board[x][y].Marked)
				{
					horizontal++;
				}

				if (board[y][x].Marked)
				{
					vertical++;
				}
			}

			if (horizontal == board.Length || vertical == board.Length)
			{
				return true;
			}
		}

		return false;
	}

	int GetScore(BoardNumber[][] board)
	{
		var score = 0;

		for (var x = 0; x < board.Length; x++)
		{
			for (var y = 0; y < board.Length; y++)
			{
				var number = board[x][y];

				if (!number.Marked)
				{
					score += number.Number;
				}
			}
		}

		return score;
	}
}
