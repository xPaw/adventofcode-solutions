using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(11)]
public class Day11 : IAnswer
{
	enum Operator
	{
		Multiply,
		Add,
		Power,
	}

	class Monkey
	{
		public ulong[] Items = new ulong[64];
		public int ItemsCount = 0;
		public Operator Operation;
		public uint OperationVariable;
		public uint DivisibleTest;
		public int MonkeyIfTrue;
		public int MonkeyIfFalse;
		public ulong Inspections;
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var monkeys = new List<Monkey>();

		static (int Index, ulong Result) ParseIntUntil(ReadOnlySpan<char> line)
		{
			var result = 0ul;
			var i = 0;

			do
			{
				var t = line[i++];

				if (t == ',')
				{
					i++;
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < line.Length);

			return (i, result);
		}

		{
			Monkey monkey = new();
			var i = 0;

			foreach (var line in input.AsSpan().EnumerateLines())
			{
				switch (++i)
				{
					case 1:
						monkey = new Monkey();
						monkeys.Add(monkey);
						break;

					case 2:
						var numbers = line[18..];
						do
						{
							var (index, result) = ParseIntUntil(numbers);
							numbers = numbers[index..];
							monkey.Items[monkey.ItemsCount++] = result;
						}
						while (numbers.Length > 0);

						break;

					case 3:
						if (line[25] == 'o')
						{
							if (line[23] == '*')
							{
								monkey.Operation = Operator.Power;
							}
							else if (line[23] == '+')
							{
								monkey.Operation = Operator.Multiply;
								monkey.OperationVariable = 2;
							}
							else
							{
								throw new NotImplementedException();
							}
						}
						else
						{
							monkey.Operation = line[23] switch
							{
								'*' => Operator.Multiply,
								'+' => Operator.Add,
								_ => throw new NotImplementedException(),
							};
							monkey.OperationVariable = uint.Parse(line[25..]);
						}

						break;

					case 4:
						monkey.DivisibleTest = uint.Parse(line[21..]);
						break;

					case 5:
						monkey.MonkeyIfTrue = int.Parse(line[28..]);
						break;

					case 6:
						monkey.MonkeyIfFalse = int.Parse(line[30..]);
						break;

					case 7:
						i = 0;
						break;
				}
			}
		}

		var remainder = monkeys.Aggregate(1u, (current, monkey) => current * monkey.DivisibleTest);

		ulong Simulate(List<Monkey> monkeys, bool isPartTwo)
		{
			var iterations = isPartTwo ? 10000 : 20;

			while (iterations-- > 0)
			{
				foreach (var monkey in monkeys)
				{
					for (var i = 0; i < monkey.ItemsCount; i++)
					{
						var oldItem = monkey.Items[i];
						var newItem = monkey.Operation switch
						{
							Operator.Power => oldItem * oldItem,
							Operator.Multiply => oldItem * monkey.OperationVariable,
							Operator.Add => oldItem + monkey.OperationVariable,
							_ => throw new NotImplementedException(),
						};

						if (isPartTwo)
						{
							newItem %= remainder;
						}
						else
						{
							newItem /= 3ul;
						}

						var throwTo = newItem % monkey.DivisibleTest == 0 ? monkey.MonkeyIfTrue : monkey.MonkeyIfFalse;

						var newMonkey = monkeys[throwTo];
						newMonkey.Items[newMonkey.ItemsCount++] = newItem;
						monkey.Inspections++;
					}

					monkey.ItemsCount = 0;
				}
			}

			monkeys = monkeys.OrderByDescending(monkey => monkey.Inspections).ToList();

			return monkeys[0].Inspections * monkeys[1].Inspections;
		}

		var monkeys2 = new List<Monkey>(monkeys.Count);

		foreach (var monkey in monkeys)
		{
			var newMonkey = new Monkey
			{
				ItemsCount = monkey.ItemsCount,
				MonkeyIfTrue = monkey.MonkeyIfTrue,
				MonkeyIfFalse = monkey.MonkeyIfFalse,
				DivisibleTest = monkey.DivisibleTest,
				Operation = monkey.Operation,
				OperationVariable = monkey.OperationVariable,
			};

			Array.Copy(monkey.Items, newMonkey.Items, monkey.ItemsCount);

			monkeys2.Add(newMonkey);
		}

		var part1 = Simulate(monkeys, false);
		var part2 = Simulate(monkeys2, true);

		return (part1.ToString(), part2.ToString());
	}
}
