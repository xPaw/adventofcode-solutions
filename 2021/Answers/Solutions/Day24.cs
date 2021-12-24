using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(24)]
class Day24 : IAnswer
{
	readonly int[] a = new int[14];
	readonly int[] b = new int[14];
	readonly int[] c = new int[14];

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n');

		for (var i = 0; i < 14; i++)
		{
			a[i] = ParseNumber(lines[4 + 18 * i]);
			b[i] = ParseNumber(lines[5 + 18 * i]);
			c[i] = ParseNumber(lines[15 + 18 * i]);
		}

		var part1 = Solve(true);
		var part2 = Solve(false);

#if DEBUG
		System.Diagnostics.Debug.Assert(ArithmeticLogicUnit.FromLong(lines, part1).Registers[3] == 0);
		System.Diagnostics.Debug.Assert(ArithmeticLogicUnit.FromLong(lines, part2).Registers[3] == 0);
		System.Diagnostics.Debug.Assert(ArithmeticLogicUnit.FromLong(lines, part1 + 1).Registers[3] != 0);
		System.Diagnostics.Debug.Assert(ArithmeticLogicUnit.FromLong(lines, part2 - 1).Registers[3] != 0);
#endif

		return (part1.ToString(), part2.ToString());
	}

	int ParseNumber(string line) => int.Parse(line[line.LastIndexOf(' ')..]);

	long Solve(bool max)
	{
		var number = new int[14];
		var stack = new Stack<int>(7);

		for (var i = 0; i < 14; i++)
		{
			if (a[i] == 1)
			{
				number[i] = max ? 9 : 1;
				stack.Push(i);
			}
			else
			{
				var y = stack.Pop();
				var digit = number[y] + c[y] + b[i];
				number[i] = digit;

				if (digit > 9)
				{
					number[y] -= digit - 9;
					number[i] = 9;
				}
				else if (digit < 1)
				{
					number[y] += 1 - digit;
					number[i] = 1;
				}
			}
		}

		long n = 0;

		for (var i = 0; i < 14; i++)
		{
			n = 10 * n + number[i];
		}

		return n;
	}

	class ArithmeticLogicUnit
	{
		public readonly int[] Registers = new int[4];

		public static ArithmeticLogicUnit FromLong(string[] instructions, long input)
		{
			var list = new List<int>();
			while (input > 0)
			{
				list.Add((int)(input % 10));
				input /= 10L;
			}
			list.Reverse();

			return new ArithmeticLogicUnit(instructions, list.ToArray());
		}

		public ArithmeticLogicUnit(string[] instructions, int[] input)
		{
			var inputIndex = 0;

			foreach (var instruction in instructions)
			{
				var line = instruction.Split(' ');

				switch (line[0])
				{
					case "inp":
						Registers[GetRegister(line[1])] = input[inputIndex++];
						break;

					case "add":
						Registers[GetRegister(line[1])] += GetValue(line[2]);
						break;

					case "mul":
						Registers[GetRegister(line[1])] *= GetValue(line[2]);
						break;

					case "div":
						Registers[GetRegister(line[1])] /= GetValue(line[2]);
						break;

					case "mod":
						Registers[GetRegister(line[1])] %= GetValue(line[2]);
						break;

					case "eql":
						var r = GetRegister(line[1]);
						Registers[r] = Registers[r] == GetValue(line[2]) ? 1 : 0;
						break;

					default:
						throw new NotImplementedException(instruction);
				}
			}
		}

		int GetRegister(string register)
		{
			return register[0] switch
			{
				'w' => 0,
				'x' => 1,
				'y' => 2,
				'z' => 3,
				_ => int.Parse(register)
			};
		}

		int GetValue(string value)
		{
			return value[0] switch
			{
				'w' => Registers[0],
				'x' => Registers[1],
				'y' => Registers[2],
				'z' => Registers[3],
				_ => int.Parse(value)
			};
		}
	}
}
