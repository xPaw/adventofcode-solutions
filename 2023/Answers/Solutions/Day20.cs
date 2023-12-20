using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode;

[Answer(20)]
public class Day20 : IAnswer
{
	record struct NextQueue(string Source, string Destination, bool IsHigh);

	abstract class IModule(string name, string[] dests)
	{
		public string Name = name;
		public string[] Destinations { get; init; } = dests;

		public virtual IEnumerable<NextQueue> ProcessPulse(string source, bool isHigh)
		{
			foreach (var dest in Destinations)
			{
				yield return new(Name, dest, isHigh);
			}
		}
	}

	class FlipFlopModule(string name, string[] dests) : IModule(name, dests)
	{
		public bool TurnedOn { get; set; }

		public override IEnumerable<NextQueue> ProcessPulse(string source, bool isHigh)
		{
			if (isHigh)
			{
				return [];
			}

			TurnedOn = !TurnedOn;

			return base.ProcessPulse(source, TurnedOn);
		}
	}

	class ConjunctionModule(string name, string[] dests) : IModule(name, dests)
	{
		public Dictionary<string, bool> Sources { get; } = [];

		public override IEnumerable<NextQueue> ProcessPulse(string source, bool isHigh)
		{
			Sources[source] = isHigh;

			return base.ProcessPulse(source, !Sources.Values.All(b => b));
		}
	}

	class BroadcasterModule(string[] dests) : IModule("broadcaster", dests)
	{

	}

	private Dictionary<string, IModule> ParseModules(string input)
	{
		var modules = new Dictionary<string, IModule>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var dash = line.IndexOf('-');
			var name = line[..(dash - 1)];
			var destinations = line[(dash + 3)..].ToString().Split(", ");

			if (name[0] == '%')
			{
				name = name[1..];
				var nameStr = name.ToString();

				modules[nameStr] = new FlipFlopModule(nameStr, destinations);
			}
			else if (name[0] == '&')
			{
				name = name[1..];
				var nameStr = name.ToString();

				modules[nameStr] = new ConjunctionModule(nameStr, destinations);
			}
			else
			{
				modules[name.ToString()] = new BroadcasterModule(destinations);
			}
		}

		foreach (var module in modules.Values)
		{
			foreach (var dest in module.Destinations)
			{
				if (modules.TryGetValue(dest, out var moduleDest) && moduleDest is ConjunctionModule conjunctionModule)
				{
					conjunctionModule.Sources[module.Name] = false;
				}
			}
		}

		return modules;
	}

	public Solution Solve(string input)
	{
		var modules = ParseModules(input);

		var part2 = 0L;
		var lowPulses = 0;
		var highPulses = 0;

		var rxPulses = new Dictionary<string, long>();
		var rxSource = modules.Values.First(m => Array.IndexOf(m.Destinations, "rx") != -1).Name;

		foreach (var module in modules.Values)
		{
			foreach (var dest in module.Destinations)
			{
				if (dest == rxSource)
				{
					rxPulses[module.Name] = 0;
				}
			}
		}

		for (var i = 0; ; i++)
		{
			if (rxPulses.All(rxs => rxs.Value > 0))
			{
				part2 = Lcm(rxPulses.Values.Select(x => x));
				break;
			}

			var queue = new Queue<NextQueue>();
			queue.Enqueue(new("button", "broadcaster", false));

			while (queue.TryDequeue(out var next))
			{
				//Console.WriteLine($"{next.Source} -{(next.IsHigh ? "high" : "low")}-> {next.Destination}");

				if (i < 1000)
				{
					if (next.IsHigh)
					{
						highPulses++;
					}
					else
					{
						lowPulses++;
					}
				}

				if (!modules.TryGetValue(next.Destination, out var destinationModule))
				{
					continue;
				}

				foreach (var future in destinationModule.ProcessPulse(next.Source, next.IsHigh))
				{
					queue.Enqueue(future);

					if (future.Destination == "rx" && destinationModule is ConjunctionModule conjunctionModule)
					{
						foreach (var conjunctionInput in conjunctionModule.Sources.Where(b => b.Value))
						{
							rxPulses[conjunctionInput.Key] = i + 1;
						}
					}
				}
			}
		}

		var part1 = lowPulses * highPulses;

		return new(part1.ToString(), part2.ToString());
	}

	private static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);
	private static long Lcm(IEnumerable<long> numbers) => numbers.Aggregate((a, b) => a * b / Gcd(a, b));
}
