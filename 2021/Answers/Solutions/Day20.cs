using System;
using System.Collections.Generic;

namespace AdventOfCode2021;

[Answer(20)]
class Day20 : IAnswer
{
	struct Pixel
	{
		public int X;
		public int Y;

		public Pixel(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString() => $"{X},{Y}";
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split("\n\n");
		var algorithm = new bool[512];

		for (var i = 0; i < 512; i++)
		{
			algorithm[i] = lines[0][i] == '#';
		}

		var imageRaw = lines[1].Split('\n');
		var imageSize = imageRaw.Length;
		var image = new bool[imageSize * imageSize + imageSize];
		int x;
		int y;

		for (x = 0; x < imageSize; x++)
		{
			for (y = 0; y < imageSize; y++)
			{
				if (imageRaw[x][y] == '#')
				{
					image[x * imageSize + y] = true;
				}
			}
		}

		var border = false;
		var part1 = 0;
		var part2 = 0;

		for (var iteration = 1; iteration <= 50; iteration++)
		{
			var newImageSize = imageSize + 2;
			var newImage = new bool[newImageSize * newImageSize + newImageSize];
			var lit = 0;

			for (x = -1; x < imageSize + 1; x++)
			{
				for (y = -1; y < imageSize + 1; y++)
				{
					var bitOffset = 9;
					var algorhitmOffset = 0;

					for (var dx = -1; dx <= 1; dx++)
					{
						for (var dy = -1; dy <= 1; dy++)
						{
							bitOffset--;

							var rx = x + dx;
							var ry = y + dy;
							bool set;

							if (rx >= 0 && rx < imageSize && ry >= 0 && ry < imageSize)
							{
								set = image[rx * imageSize + ry];
							}
							else
							{
								set = border;
							}

							if (set)
							{
								algorhitmOffset |= 1 << bitOffset;
							}
						}
					}

					if (algorithm[algorhitmOffset])
					{
						newImage[(x + 1) * newImageSize + y + 1] = true;
						lit++;
					}
				}
			}

			image = newImage;
			imageSize = newImageSize;
			border = algorithm[border ? 511 : 0];

			if (iteration == 2)
			{
				part1 = lit;
			}
			else if (iteration == 50)
			{
				part2 = lit;
			}
		}

		return (part1.ToString(), part2.ToString());
	}
}
