using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(24, slow: true)]
class Day24 : IAnswer
{
	readonly int[] a = new int[14];
	readonly int[] b = new int[14];
	readonly int[] c = new int[14];
	readonly int[] stackLookup = new int[14];

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n');
		var stack = new Stack<int>(7);

		for (var i = 0; i < 14; i++)
		{
			a[i] = ParseNumber(lines[4 + 18 * i]);
			b[i] = ParseNumber(lines[5 + 18 * i]);
			c[i] = ParseNumber(lines[15 + 18 * i]);

			if (a[i] == 1)
			{
				stack.Push(i);
			}
			else
			{
				stackLookup[i] = stack.Pop();
			}
		}

		var part1 = Solve(new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 });
		var part2 = Solve(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

		return (part1.ToString(), part2.ToString());
	}

	int ParseNumber(string line) => int.Parse(line[line.LastIndexOf(' ')..]);

	long Solve(int[] range)
	{
		var number = new int[14];

		foreach (var n1 in range)
		{
			foreach (var n2 in range)
			{
				foreach (var n3 in range)
				{
					foreach (var n4 in range)
					{
						foreach (var n5 in range)
						{
							foreach (var n6 in range)
							{
								foreach (var n7 in range)
								{
									var digit = 0;
									var bad = false;

									for (var i = 0; i < 14; i++)
									{
										if (a[i] == 1)
										{
											number[i] = digit switch
											{
												0 => n1,
												1 => n2,
												2 => n3,
												3 => n4,
												4 => n5,
												5 => n6,
												6 => n7,
												_ => throw new NotImplementedException(),
											};
											digit++;
										}
										else
										{
											var y = stackLookup[i];
											number[i] = number[y] + c[y] + b[i];

											if (number[i] < 1 || number[i] > 9)
											{
												bad = true;
												break;
											}
										}
									}

									if (bad)
									{
										continue;
									}

									long n = 0;

									for (var i = 0; i < 14; i++)
									{
										n = 10 * n + number[i];
									}

									return n;
								}
							}
						}
					}
				}
			}
		}

		return 0;
	}
}
