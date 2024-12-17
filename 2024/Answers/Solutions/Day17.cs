using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(17)]
public class Day17 : IAnswer
{
	public Solution Solve(string input)
	{
		var span = input.AsSpan();

		var A = ParseInt(span[(span.IndexOf("A:") + 3)..]);
		var B = ParseInt(span[(span.IndexOf("B:") + 3)..]);
		var C = ParseInt(span[(span.IndexOf("C:") + 3)..]);
		var program = span[(span.IndexOf("Program: ") + 9)..].ToString().Split(',').Select(x => x[0] - '0').ToArray();
		var outs = new List<int>(32);

		RunProgram(program, outs, A, B, C);
		var part1 = string.Join(',', outs);

		var part2 = Search(0, 0, program, outs, B, C, long.MaxValue);

		return new(part1.ToString(), part2.ToString());
	}

	private long Search(long currentA, int depth, int[] program, List<int> outs, long B, long C, long prevMin)
	{
		var offset = currentA << 3;
		var min = prevMin;

		for (var i = 0; i < 8; i++)
		{
			var A = offset + i;

			RunProgram(program, outs, A, B, C);

			if (!outs.SequenceEqual(program.TakeLast(depth + 1)))
			{
				continue;
			}

			if (depth < program.Length - 1)
			{
				A = Search(A, depth + 1, program, outs, B, C, min);
			}

			if (min > A)
			{
				min = A;
			}
		}

		return min;
	}

	static void RunProgram(int[] program, List<int> outs, long A, long B, long C)
	{
		outs.Clear();

		long GetComboOperand(int operand)
		{
			return operand switch
			{
				0 => 0,
				1 => 1,
				2 => 2,
				3 => 3,
				4 => A,
				5 => B,
				6 => C,
				_ => throw new NotImplementedException(),
			};
		}

		for (var i = 0; i < program.Length; i++)
		{
			var opcode = program[i];
			var operand = program[++i];

			switch (opcode)
			{
				case 0: // adv
					A /= (long)Math.Pow(2, GetComboOperand(operand));
					break;

				case 6: // bdv
					B = A / (long)Math.Pow(2, GetComboOperand(operand));
					break;

				case 7: // cdv
					C = A / (long)Math.Pow(2, GetComboOperand(operand));
					break;

				case 2: // bst
					B = GetComboOperand(operand) % 8;
					break;

				case 1: // bxl
					B ^= operand;
					break;

				case 4: // bxc
					B ^= C;
					break;

				case 3: // bst
					if (A == 0)
					{
						break;
					}

					i = operand;
					i--; // the loop will i++

					break;

				case 5: // out
					outs.Add((int)(GetComboOperand(operand) % 8));
					break;

				default:
					throw new NotImplementedException();
			}
		}
	}

	static long ParseInt(ReadOnlySpan<char> line)
	{
		var result = 0;

		while (true)
		{
			if (!char.IsAsciiDigit(line[0]))
			{
				break;
			}

			result = 10 * result + line[0] - '0';
			line = line[1..];
		}

		return result;
	}
}
