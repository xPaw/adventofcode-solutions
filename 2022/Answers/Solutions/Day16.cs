using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(16)]
public class Day16 : IAnswer
{
	class Valve
	{
		public int Name;
		public int FlowRate;
		public List<int> Connections = new();
	}

	readonly Dictionary<int, Valve> valves = new();
	readonly Dictionary<int, int> visited = new();

	public (string Part1, string Part2) Solve(string input)
	{
		var i = 0;

		int ParseInt()
		{
			var result = 0;

			do
			{
				var t = input[i++];

				if (!char.IsAsciiDigit(t))
				{
					break;
				}

				result = 10 * result + t - '0';
			}
			while (i < input.Length);

			return result;
		}

		int Hash(char a, char b)
		{
			return (a - 'A') * 26 + (b - 'A');
		}

		int ParseValve()
		{
			var a = input[i++];
			var b = input[i++];
			return Hash(a, b);
		}

		while (i < input.Length)
		{
			i += "Valve ".Length;

			var name = ParseValve();

			var valve = new Valve
			{
				Name = name,
			};

			i += "has flow rate =".Length;

			valve.FlowRate = ParseInt();

			i += " tunnels lead to valves ".Length;

			if (input[i - 1] != ' ')
			{
				i--;
			}

			while (true)
			{
				name = ParseValve();
				valve.Connections.Add(name);

				if (i == input.Length || input[i] == '\n')
				{
					i++;
					break;
				}

				i += ", ".Length;
			}

			valves[valve.Name] = valve;
		}

		var AA = Hash('A', 'A');
		var part1 = Visit(1, 0, 0, valves[AA], new());
		visited.Clear();
		var part2 = Visit2(1, 0, 0, valves[AA], valves[AA], new());

		return (part1.ToString(), part2.ToString());
	}

	int Visit(int minute, int pressure, int totalPressure, Valve currentValve, HashSet<int> openValves)
	{
		if (minute > 30)
		{
			return totalPressure;
		}

		var hash = currentValve.Name * 100_000 + minute * 3_000 + pressure;

		if (visited.TryGetValue(hash, out var cached) && cached >= totalPressure)
		{
			return cached;
		}

		visited[hash] = totalPressure;

		int currentPressure;
		var maxPressure = 0;

		foreach (var valve in currentValve.Connections)
		{
			currentPressure = Visit(minute + 1, pressure, totalPressure + pressure, valves[valve], openValves);

			if (maxPressure < currentPressure)
			{
				maxPressure = currentPressure;
			}
		}

		if (currentValve.FlowRate == 0 || openValves.Contains(currentValve.Name))
		{
			return maxPressure;
		}

		var newOpenValves = new HashSet<int>(openValves)
		{
			currentValve.Name
		};

		currentPressure = Visit(minute + 1, pressure + currentValve.FlowRate, totalPressure + pressure, currentValve, newOpenValves);

		return Math.Max(currentPressure, maxPressure);
	}

	int Visit2(int minute, int pressure, int totalPressure, Valve currentValve, Valve elephantValve, HashSet<int> openValves)
	{
		if (minute > 26)
		{
			return totalPressure;
		}

		var hash = elephantValve.Name * 100_000_000 + currentValve.Name * 100_000 + minute * 3_000 + pressure;

		if (visited.TryGetValue(hash, out var cached) && cached >= totalPressure)
		{
			return cached;
		}

		visited[hash] = totalPressure;

		var maxPressure = 0;

		foreach (var valve1 in currentValve.Connections)
		{
			foreach (var valve2 in elephantValve.Connections)
			{
				if (valve1 == valve2)
				{
					continue;
				}

				var currentPressure = Visit2(minute + 1, pressure, totalPressure + pressure, valves[valve1], valves[valve2], openValves);

				if (maxPressure < currentPressure)
				{
					maxPressure = currentPressure;
				}
			}
		}

		if (currentValve == elephantValve)
		{
			return maxPressure;
		}

		var canOpen = currentValve.FlowRate > 0 && !openValves.Contains(currentValve.Name);
		var canElephantOpen = elephantValve.FlowRate > 0 && !openValves.Contains(elephantValve.Name);

		if (canOpen && canElephantOpen)
		{
			var newOpenValves = new HashSet<int>(openValves)
			{
				currentValve.Name,
				elephantValve.Name
			};

			var currentPressure = Visit2(minute + 1, pressure + currentValve.FlowRate + elephantValve.FlowRate, totalPressure + pressure, currentValve, elephantValve, newOpenValves);

			if (maxPressure < currentPressure)
			{
				maxPressure = currentPressure;
			}
		}
		else if (canOpen)
		{
			var newOpenValves = new HashSet<int>(openValves)
			{
				currentValve.Name
			};

			foreach (var valve in elephantValve.Connections)
			{
				var currentPressure = Visit2(minute + 1, pressure + currentValve.FlowRate, totalPressure + pressure, currentValve, valves[valve], newOpenValves);

				if (maxPressure < currentPressure)
				{
					maxPressure = currentPressure;
				}
			}
		}
		else if (canElephantOpen)
		{
			var newOpenValves = new HashSet<int>(openValves)
			{
				elephantValve.Name
			};

			foreach (var valve in currentValve.Connections)
			{
				var currentPressure = Visit2(minute + 1, pressure + elephantValve.FlowRate, totalPressure + pressure, valves[valve], elephantValve, newOpenValves);

				if (maxPressure < currentPressure)
				{
					maxPressure = currentPressure;
				}
			}
		}

		return maxPressure;
	}
}
