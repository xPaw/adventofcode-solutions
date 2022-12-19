using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(19)]
public class Day19 : IAnswer
{
	record struct State
	{
		public int Time;
		public int RobotsOre;
		public int RobotsClay;
		public int RobotsObsidian;
		public int RobotsGeode;
		public int Ore;
		public int Clay;
		public int Obsidian;
		public int Geode;
	}

	record Blueprint
	{
		public int oreRobotCost;
		public int clayRobotCost;
		public int obsidianRobotCostOre;
		public int obsidianRobotCostClay;
		public int geodeRobotCostOre;
		public int geodeRobotCostObsidian;
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var part1 = 0;
		var part2 = 1;

		static int ParseInt(ref int i, ReadOnlySpan<char> line)
		{
			var result = 0;

			do
			{
				var t = line[i++];

				if (t == ' ')
				{
					i--;
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < line.Length);

			return result;
		}

		var blueprints = new List<Blueprint>(32);

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var blueprint = new Blueprint();
			blueprints.Add(blueprint);

			var i = "Blueprint ".Length;
			ParseInt(ref i, line);
			i += " Each ore robot costs ".Length;
			blueprint.oreRobotCost = ParseInt(ref i, line);
			i += " ore. Each clay robot costs ".Length;
			blueprint.clayRobotCost = ParseInt(ref i, line);
			i += " ore. Each obsidian robot costs ".Length;
			blueprint.obsidianRobotCostOre = ParseInt(ref i, line);
			i += " ore and ".Length;
			blueprint.obsidianRobotCostClay = ParseInt(ref i, line);
			i += " clay. Each geode robot costs ".Length;
			blueprint.geodeRobotCostOre = ParseInt(ref i, line);
			i += " ore and ".Length;
			blueprint.geodeRobotCostObsidian = ParseInt(ref i, line);
		}

		for (var i = 1; i <= blueprints.Count; i++)
		{
			var blueprint = blueprints[i - 1];
			part1 += i * Solve(blueprint, 24);
		}

		for (var i = Math.Min(blueprints.Count, 3) - 1; i >= 0; i--)
		{
			var blueprint = blueprints[i];
			part2 *= Solve(blueprint, 32);
		}

		return (part1.ToString(), part2.ToString());
	}

	int Solve(Blueprint blueprint, int time)
	{
		var seen = new HashSet<State>();
		var queue = new Queue<State>();
		queue.Enqueue(new State
		{
			Time = time,
			RobotsOre = 1,
		});

		var maxGeodes = 0;
		var maxObsidianCost = blueprint.geodeRobotCostObsidian;
		var maxClayCost = blueprint.obsidianRobotCostClay;
		var maxOreCost = blueprint.oreRobotCost;
		if (maxOreCost < blueprint.clayRobotCost) maxOreCost = blueprint.clayRobotCost;
		if (maxOreCost < blueprint.obsidianRobotCostOre) maxOreCost = blueprint.obsidianRobotCostOre;
		if (maxOreCost < blueprint.geodeRobotCostOre) maxOreCost = blueprint.geodeRobotCostOre;

		while (queue.TryDequeue(out var state))
		{
			if (state.Time == -1)
			{
				continue;
			}

			if (maxGeodes < state.Geode)
			{
				maxGeodes = state.Geode;
			}

			state.Ore = Math.Min(state.Ore, maxOreCost + (maxOreCost - state.RobotsOre) * (state.Time - 1));
			state.Clay = Math.Min(state.Clay, maxClayCost + (maxClayCost - state.RobotsClay) * (state.Time - 1));
			state.Obsidian = Math.Min(state.Obsidian, maxObsidianCost + (maxObsidianCost - state.RobotsObsidian) * (state.Time - 1));

			if (seen.Contains(state))
			{
				continue;
			}

			seen.Add(state);

			var newState = state with
			{
				Time = state.Time - 1,
				Geode = state.Geode + state.RobotsGeode,
				Ore = state.Ore + state.RobotsOre,
				Clay = state.Clay + state.RobotsClay,
				Obsidian = state.Obsidian + state.RobotsObsidian,
			};

			queue.Enqueue(newState);

			// Each ore robot costs 4 ore.
			if (state.Ore >= blueprint.oreRobotCost && state.RobotsOre < maxOreCost)
			{
				queue.Enqueue(newState with
				{
					Ore = newState.Ore - blueprint.oreRobotCost,
					RobotsOre = newState.RobotsOre + 1,
				});
			}

			// Each clay robot costs 4 ore.
			if (state.Ore >= blueprint.clayRobotCost && state.RobotsClay < maxClayCost)
			{
				queue.Enqueue(newState with
				{
					Ore = newState.Ore - blueprint.clayRobotCost,
					RobotsClay = newState.RobotsClay + 1,
				});
			}

			// Each obsidian robot costs 4 ore and 20 clay.
			if (state.Clay >= blueprint.obsidianRobotCostClay && state.Ore >= blueprint.obsidianRobotCostOre && state.RobotsObsidian < maxObsidianCost)
			{
				queue.Enqueue(newState with
				{
					Clay = newState.Clay - blueprint.obsidianRobotCostClay,
					Ore = newState.Ore - blueprint.obsidianRobotCostOre,
					RobotsObsidian = newState.RobotsObsidian + 1,
				});
			}

			// Each geode robot costs 2 ore and 12 obsidian.
			if (state.Obsidian >= blueprint.geodeRobotCostObsidian && state.Ore >= blueprint.geodeRobotCostOre)
			{
				queue.Enqueue(newState with
				{
					Obsidian = newState.Obsidian - blueprint.geodeRobotCostObsidian,
					Ore = newState.Ore - blueprint.geodeRobotCostOre,
					RobotsGeode = newState.RobotsGeode + 1,
				});
			}
		}

		return maxGeodes;
	}
}
