using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(7)]
public class Day7 : IAnswer
{
	public (string Part1, string Part2) Solve(string input)
	{
		const int TOTAL_DISK_SIZE = 70_000_000;
		const int WANTED_FREE_SPACE = 30_000_000;

		var currentPath = new string[32];
		currentPath[0] = string.Empty;
		var currentPathDepth = 1;
		var totalFreeSpace = TOTAL_DISK_SIZE;
		var allDirectories = new Dictionary<string, int>
		{
			[string.Empty] = new() // root
		};

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			if (line[0] == '$')
			{
				if (line[2] == 'l')
				{
					//
				}
				else // cd
				{
					if (line[5] == '.')
					{
						currentPathDepth--;
					}
					else if (line[5] == '/')
					{
						currentPathDepth = 1;
					}
					else
					{
						var name = line[5..].ToString();
						var path = string.Concat(currentPath[currentPathDepth - 1], "/", name);

						currentPath[currentPathDepth++] = path;
						allDirectories[path] = 0;
					}
				}

				continue;
			}
			else if (line[0] == 'd') // dir
			{
				continue;
			}

			var size = int.Parse(line[..line.IndexOf(' ')]);
			totalFreeSpace -= size;

			for (var i = 0; i < currentPathDepth; i++)
			{
				allDirectories[currentPath[i]] += size;
			}
		}

		var part1 = 0;
		var part2 = int.MaxValue;

		foreach (var (_, size) in allDirectories)
		{
			if (size <= 100_000)
			{
				part1 += size;
			}

			if (part2 > size && totalFreeSpace + size >= WANTED_FREE_SPACE)
			{
				part2 = size;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
