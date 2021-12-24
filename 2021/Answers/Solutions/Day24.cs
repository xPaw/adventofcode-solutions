using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(24, slow: true)]
class Day24 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n');
		long part1 = 0;
		long part2 = long.MaxValue;

		var a = new int[14];
		var b = new int[14];
		var c = new int[14];
		var number = new int[14];

		for (var i = 0; i < 14; i++)
		{
			a[i] = ParseNumber(lines[4 + 18 * i]);
			b[i] = ParseNumber(lines[5 + 18 * i]);
			c[i] = ParseNumber(lines[15 + 18 * i]);
		}

		for (var n1 = 1; n1 < 10; n1++)
		{
			for (var n2 = 1; n2 < 10; n2++)
			{
				for (var n3 = 1; n3 < 10; n3++)
				{
					for (var n4 = 1; n4 < 10; n4++)
					{
						for (var n5 = 1; n5 < 10; n5++)
						{
							for (var n6 = 1; n6 < 10; n6++)
							{
								for (var n7 = 1; n7 < 10; n7++)
								{
									var stack = new Stack<int>();
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
											stack.Push(i);
										}
										else
										{
											var y = stack.Pop();
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

									if (part1 < n)
									{
										part1 = n;
									}

									if (part2 > n)
									{
										part2 = n;
									}
								}
							}
						}
					}
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}

	int ParseNumber(string line) => int.Parse(line[line.LastIndexOf(' ')..]);
}
