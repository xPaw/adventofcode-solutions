using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode;

[Answer(19)]
public class Day19 : IAnswer
{
	public Solution Solve(string input)
	{
		var split = input.Split("\n\n");
		var workflows = new Dictionary<string, string[]>();
		var ratings = split[1];

		static int ParseInt(ReadOnlySpan<char> line)
		{
			var result = 0;
			var i = 0;

			do
			{
				result = 10 * result + line[i++] - '0';
			}
			while (i < line.Length);

			return result;
		}

		long RunWorkflows(ImmutableArray<Range> ranges, string workflow)
		{
			if (workflow[0] == 'R')
			{
				return 0L;
			}
			else if (ranges.Any(r => r.Start.Value > r.End.Value))
			{
				return 0L;
			}
			else if (workflow[0] == 'A')
			{
				var combinations = 1L;

				foreach (var range in ranges)
				{
					combinations *= range.End.Value - range.Start.Value + 1;
				}

				return combinations;
			}

			var total = 0L;

			foreach (var expression in workflows[workflow])
			{
				var condition = expression.Length > 1 ? expression[1] : '\0';

				if (condition != '<' && condition != '>')
				{
					total += RunWorkflows(ranges, expression);
					continue;
				}

				var id = expression[0] switch
				{
					'x' => 0,
					'm' => 1,
					'a' => 2,
					's' => 3,
					_ => throw new Exception(),
				};
				var range = ranges[id];

				var colon = expression.IndexOf(':');
				var value = ParseInt(expression.AsSpan()[2..colon]);
				workflow = expression[(colon + 1)..];

				if (condition == '>')
				{
					var low = new Range(range.Start, Math.Min(value, range.End.Value));
					var high = new Range(Math.Max(range.Start.Value, value + 1), range.End);

					total += RunWorkflows(ranges.SetItem(id, high), workflow);
					ranges = ranges.SetItem(id, low);
				}
				else
				{
					var low = new Range(range.Start, Math.Min(value - 1, range.End.Value));
					var high = new Range(Math.Max(range.Start.Value, value), range.End);

					total += RunWorkflows(ranges.SetItem(id, low), workflow);
					ranges = ranges.SetItem(id, high);
				}
			}

			return total;
		}

		foreach (var line in split[0].AsSpan().EnumerateLines())
		{
			var end = line.IndexOf('{');
			var name = line[0..end].ToString();
			var expressions = line[(end + 1)..^1].ToString().Split(',').ToArray();

			workflows[name] = expressions;
		}

		var part1 = 0;

		foreach (var rating in ratings.AsSpan().EnumerateLines())
		{
			var variables = rating[1..^1].ToString().Split(',').Select(x =>
			{
				var number = ParseInt(x.AsSpan()[2..]);
				return new Range(number, number);
			}).ToImmutableArray();

			if (RunWorkflows(variables, "in") == 1)
			{
				part1 += variables.Sum(x => x.Start.Value);
			}
		}

		var ranges = new Range[]
		{
			new(1, 4000),
			new(1, 4000),
			new(1, 4000),
			new(1, 4000),
		}.ToImmutableArray();

		var part2 = RunWorkflows(ranges, "in");

		return new(part1.ToString(), part2.ToString());
	}
}
