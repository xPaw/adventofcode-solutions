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

	public record RangeLong(long Start, long End);
	public record RemappedRange(RangeLong Source, RangeLong Destination);

	public Solution Solve(string input)
	{
		var part1 = long.MaxValue;
		var part2 = long.MaxValue;

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

				var src = new RangeLong(numbers[1], numbers[1] + numbers[2] - 1);
				var dst = new RangeLong(numbers[0], numbers[0] + numbers[2] - 1);

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

		/*
				foreach (var range in ranges)
				{
					ranges[range.Key] = [.. range.Value.OrderBy(x => x.From.Start)];
				}
		*/

		var seedRanges = new List<RangeLong>(seeds.Count / 2);

		for (var i = 0; i < seeds.Count; i++)
		{
			var location = seeds[i];

			if ((i + 1) % 2 == 0)
			{
				var prevSeed = seeds[i - 1];
				seedRanges.Add(new RangeLong(prevSeed, prevSeed + location - 1));
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

		Parallel.ForEach(seedRanges, (pair) =>
		{
			Parallel.For(pair.Start, pair.End + 1, (i) =>
			{
				var location = i;

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

				if (part2 > location)
				{
					part2 = location;
				}
			});

			Console.WriteLine("ok");
		});

		return new(part1.ToString(), part2.ToString());
	}
}
