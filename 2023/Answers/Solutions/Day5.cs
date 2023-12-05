using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode;

[Answer(5)]
public class Day5 : IAnswer
{
	enum ThingType
	{
		Seed,
		Soil,
		Fertilizer,
		Water,
		Light,
		Temperature,
		Humidity,
		Location,
	}

	public record RangeLong(long Start, long Length)
	{
		public long End => Start + Length - 1;
	}

	public record RemappedRange(RangeLong Source, RangeLong Destination);

	public Solution Solve(string input)
	{
		var part1 = long.MaxValue;
		var seeds = new List<long>();
		var ranges = new Dictionary<ThingType, List<RemappedRange>>(); // todo: list
		var fromType = ThingType.Seed;
		var toType = ThingType.Seed;

		for (ThingType thing = ThingType.Seed; thing <= ThingType.Location; thing++)
		{
			ranges[thing] = [];
		}

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

				ranges[fromType].Add(new RemappedRange(src, dst));

				continue;
			}

			var split = line.IndexOf("-to-");

			if (split == -1)
			{
				seeds = line[(line.IndexOf(' ') + 1)..].ToString().Split(' ').Select(x => long.Parse(x)).ToList();
				continue;
			}

			var end = line[split..].IndexOf(' ');
			var from = line[..split];
			var to = line[(split + 4)..(split + end)];

			_ = Enum.TryParse(from, true, out fromType);
			_ = Enum.TryParse(to, true, out toType);
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

			for (ThingType thing = ThingType.Seed; thing <= ThingType.Location; thing++)
			{
				foreach (var thingRanges in ranges[thing])
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

		for (ThingType thing = ThingType.Seed; thing <= ThingType.Location; thing++)
		{
			seedRanges = Map(seedRanges, ranges[thing]);
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
				var (m, u) = Map(r, mapping);
				if (m != null) result.Add(m);
				next.AddRange(u);
			}
			unmapped = next;
			next = [];
		}

		result.AddRange(unmapped);
		return result;
	}

	private (RangeLong? Mapped, List<RangeLong> Unmapped) Map(RangeLong range, RemappedRange mapping)
	{
		var headingNum = mapping.Source.Start - range.Start;
		var trailingNum = range.End - mapping.Source.End;

		if (mapping.Source.Start <= range.Start && mapping.Source.End >= range.End)
		{
			return (new RangeLong(range.Start + (mapping.Destination.Start - mapping.Source.Start), range.Length), new List<RangeLong>());
		}

		if (mapping.Source.Start >= range.Start && mapping.Source.End <= range.End)
		{
			// ====|****|=====
			var mappedRange = mapping.Destination; // source range is equal to mapping, so destination range is also equal
			var unmapped = new List<RangeLong>();
			if (headingNum > 0) unmapped.Add(new(range.Start, headingNum));
			if (trailingNum > 0) unmapped.Add(new(mapping.Source.End + 1, trailingNum));
			return (mappedRange, unmapped);
		}
		else if (mapping.Source.Start >= range.Start && mapping.Source.Start <= range.End)
		{
			// ======|*****
			var length = range.End - mapping.Source.Start + 1;
			var mappedRange = new RangeLong(mapping.Destination.Start, length);

			return (mappedRange, new List<RangeLong> { new(range.Start, range.Length - length) });
		}
		else if (mapping.Source.End >= range.Start && mapping.Source.End <= range.End)
		{
			// *****|======
			var length = mapping.Source.End - range.Start + 1;
			var mappedRange = new RangeLong(mapping.Destination.Start + (range.Start - mapping.Source.Start), length);

			return (mappedRange, new List<RangeLong> { new(mapping.Source.End + 1, range.Length - length) });
		}

		return (null, [range]);
	}
}
