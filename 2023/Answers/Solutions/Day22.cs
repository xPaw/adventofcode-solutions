using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode;

[Answer(22)]
public class Day22 : IAnswer
{
	[DebuggerDisplay("{X},{Y},{Z}")]
	class Coordinate(int x, int y, int z)
	{
		public int X { get; set; } = x;
		public int Y { get; set; } = y;
		public int Z { get; set; } = z;
	}

	[DebuggerDisplay("{Start} ~ {End} ({Below.Count} below, {Above.Count} above)")]
	record class Block(Coordinate Start, Coordinate End)
	{
		public List<Block> Below { get; set; } = [];
		public List<Block> Above { get; set; } = [];
	}

	public Solution Solve(string input)
	{
		var part1 = 0;
		var part2 = 0;
		var blocks = new List<Block>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var coords = line.ToString().Split('~');
			var start = coords[0].Split(',').Select(int.Parse).ToArray();
			var end = coords[1].Split(',').Select(int.Parse).ToArray();

			blocks.Add(new Block(
				new Coordinate(start[0], start[1], start[2]),
				new Coordinate(end[0], end[1], end[2])
			));
		}

		blocks.Sort(static (a, b) => a.Start.Z.CompareTo(b.Start.Z));

		var blocksBelow = new HashSet<Block>(16);
		var highestBlock = new Dictionary<int, Block>(blocks.Count);

		for (var i = 0; i < blocks.Count; i++)
		{
			var block = blocks[i];
			var z = 1;

			blocksBelow.Clear();

			for (var x = block.Start.X; x <= block.End.X; x++)
			{
				for (var y = block.Start.Y; y <= block.End.Y; y++)
				{
					var hash = x * 100000000 + y;

					if (highestBlock.TryGetValue(hash, out var otherBlock))
					{
						if (z < otherBlock.End.Z + 1)
						{
							z = otherBlock.End.Z + 1;
						}

						blocksBelow.Add(otherBlock);
					}

					highestBlock[hash] = block;
				}
			}

			block.End.Z = block.End.Z - block.Start.Z + z;
			block.Start.Z = z;

			foreach (var otherBlock in blocksBelow)
			{
				if (otherBlock.End.Z - block.Start.Z == -1)
				{
					block.Below.Add(otherBlock);
					otherBlock.Above.Add(block);
				}
			}
		}

		var removed = new HashSet<Block>();
		var queue = new Queue<Block>();

		foreach (var block in blocks)
		{
			part1++;

			foreach (var above in block.Above)
			{
				if (above.Below.Count == 1)
				{
					part1--;
					break;
				}
			}

			removed.Clear();
			queue.Clear();
			queue.Enqueue(block);

			while (queue.TryDequeue(out var otherBlock))
			{
				removed.Add(otherBlock);

				foreach (var aboveBlock in otherBlock.Above)
				{
					if (aboveBlock.Below.All(removed.Contains))
					{
						queue.Enqueue(aboveBlock);
						part2++;
					}
				}
			}
		}

		return new(part1.ToString(), part2.ToString());
	}
}
