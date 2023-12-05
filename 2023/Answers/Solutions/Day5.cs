using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(5)]
public class Day5 : IAnswer
{
	public record struct RangeLong(long Start, long Length)
	{
		public readonly long End => Start + Length - 1;
	}

	public record struct RemappedRange(RangeLong Source, RangeLong Destination);

	readonly List<RangeLong> ReusedList = new(2);

	public Solution Solve(string input)
	{
		var part1 = long.MaxValue;
		var seeds = new List<long>();
		var ranges = new List<List<RemappedRange>>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			if (line.Length == 0)
			{
				continue;
			}

			if (char.IsAsciiDigit(line[0]))
			{
				var numbers = line.ToString().Split(' ').Select(x => long.Parse(x)).ToList();

				var src = new RangeLong(numbers[1], numbers[2]);
				var dst = new RangeLong(numbers[0], numbers[2]);

				ranges[^1].Add(new RemappedRange(src, dst));

				continue;
			}

			var split = line.IndexOf("-to-");

			if (split == -1)
			{
				seeds = line[(line.IndexOf(' ') + 1)..].ToString().Split(' ').Select(x => long.Parse(x)).ToList();
				continue;
			}

			ranges.Add([]);
		}

		var seedRanges = new List<RangeLong>(seeds.Count / 2);

		for (var i = 0; i < seeds.Count; i++)
		{
			var location = seeds[i];

			if ((i + 1) % 2 == 0)
			{
				var prevSeed = seeds[i - 1];
				seedRanges.Add(new RangeLong(prevSeed, location));
			}

			foreach (var thingsRanges2 in ranges)
			{
				foreach (var thingRanges in thingsRanges2)
				{
					if (thingRanges.Source.Start <= location && thingRanges.Source.End >= location)
					{
						location += thingRanges.Destination.Start - thingRanges.Source.Start;
						break;
					}
				}
			}

			if (part1 > location)
			{
				part1 = location;
			}
		}

		foreach (var thingsRanges2 in ranges)
		{
			seedRanges = Map(seedRanges, thingsRanges2);
		}

		var part2 = seedRanges.MinBy(r => r.Start)!.Start;

		return new(part1.ToString(), part2.ToString());
	}

	// Stolen from Perry
	private List<RangeLong> Map(List<RangeLong> unmapped, List<RemappedRange> mappings)
	{
		var next = new List<RangeLong>();
		var result = new List<RangeLong>();

		foreach (var mapping in mappings)
		{
			foreach (var r in unmapped)
			{
				var m = Map(r, mapping);
				if (m != null) result.Add(m.Value);
				next.AddRange(ReusedList);
			}
			unmapped = next;
			next = [];
		}

		result.AddRange(unmapped);
		return result;
	}

	private RangeLong? Map(RangeLong range, RemappedRange mapping)
	{
		var headingNum = mapping.Source.Start - range.Start;
		var trailingNum = range.End - mapping.Source.End;

		ReusedList.Clear();

		if (mapping.Source.Start <= range.Start && mapping.Source.End >= range.End)
		{
			return new RangeLong(range.Start + (mapping.Destination.Start - mapping.Source.Start), range.Length);
		}

		if (mapping.Source.Start >= range.Start && mapping.Source.End <= range.End)
		{
			// ====|****|=====
			var mappedRange = mapping.Destination; // source range is equal to mapping, so destination range is also equal
			if (headingNum > 0) ReusedList.Add(new(range.Start, headingNum));
			if (trailingNum > 0) ReusedList.Add(new(mapping.Source.End + 1, trailingNum));
			return mappedRange;
		}
		else if (mapping.Source.Start >= range.Start && mapping.Source.Start <= range.End)
		{
			// ======|*****
			var length = range.End - mapping.Source.Start + 1;
			var mappedRange = new RangeLong(mapping.Destination.Start, length);

			ReusedList.Add(new(range.Start, range.Length - length));

			return mappedRange;
		}
		else if (mapping.Source.End >= range.Start && mapping.Source.End <= range.End)
		{
			// *****|======
			var length = mapping.Source.End - range.Start + 1;
			var mappedRange = new RangeLong(mapping.Destination.Start + (range.Start - mapping.Source.Start), length);

			ReusedList.Add(new(mapping.Source.End + 1, range.Length - length));

			return mappedRange;
		}

		ReusedList.Add(range);

		return null;
	}
}
