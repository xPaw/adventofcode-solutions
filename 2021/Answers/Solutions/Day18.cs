using System;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode2021;

[Answer(18)]
class Day18 : IAnswer
{
	[DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
	class SnailfishNumber
	{
		public int Value;
		public SnailfishNumber? X;
		public SnailfishNumber? Y;
		public SnailfishNumber? Parent;

		public static SnailfishNumber operator +(SnailfishNumber a, SnailfishNumber b)
		{
			var newNumber = new SnailfishNumber
			{
				X = a,
				Y = b,
			};
			newNumber.X!.Parent = newNumber;
			newNumber.Y!.Parent = newNumber;

			while (true)
			{
				if (newNumber.Explode())
				{
					continue;
				}

				if (!newNumber.Split())
				{
					break;
				}
			}

			return newNumber;
		}

		public SnailfishNumber? FindLeft()
		{
			var previous = this;
			var number = previous.Parent;

			while (number != null)
			{
				if (number.Y == previous)
				{
					previous = number;
					number = number.X;
					continue;
				}

				if (number.X == previous)
				{
					previous = number;
					number = number.Parent;
					continue;
				}

				if (number.X == null && number.Y == null)
				{
					return number;
				}

				number = number.Y;
			}

			return null;
		}

		public SnailfishNumber? FindRight()
		{
			var previous = this;
			var number = previous.Parent;

			while (number != null)
			{
				if (number.X == previous)
				{
					previous = number;
					number = number.Y;
					continue;
				}

				if (number.Y == previous)
				{
					previous = number;
					number = number.Parent;
					continue;
				}

				if (number.X == null && number.Y == null)
				{
					return number;
				}

				number = number.X;
			}

			return null;
		}
		public bool Explode(int depth = 0)
		{
			if (depth == 5)
			{
				var number = Parent!;
				var x = number.FindLeft();

				if (x != null)
				{
					x.Value += number.X!.Value;
				}

				var y = number.FindRight();

				if (y != null)
				{
					y.Value += number.Y!.Value;
				}

				number.X = null;
				number.Y = null;

				return true;
			}

			if (X == null)
			{
				return false;
			}

			var exploded = X.Explode(depth + 1);

			if (exploded || Y == null)
			{
				return exploded;
			}

			return Y.Explode(depth + 1);
		}
		public bool Split()
		{
			if (Value > 9)
			{
				X = new SnailfishNumber
				{
					Parent = this,
					Value = (int)Math.Floor(Value / 2.0d),
				};
				Y = new SnailfishNumber
				{
					Parent = this,
					Value = (int)Math.Ceiling(Value / 2.0d),
				};
				Value = 0;
				return true;
			}

			if (X == null)
			{
				return false;
			}

			var split = X.Split();

			if (split || Y == null)
			{
				return split;
			}

			return Y.Split();
		}

		public int Magnitude()
		{
			if (X == null && Y == null)
			{
				return Value;
			}

			return 3 * X!.Magnitude() + 2 * Y!.Magnitude();
		}

		public override string ToString()
		{
			var writer = new StringWriter();

			if (X != null)
			{
				writer.Write('[');
				writer.Write(X.ToString());
				writer.Write(',');
			}

			if (Y != null)
			{
				writer.Write(Y.ToString());
				writer.Write(']');
			}

			if (X == null && Y == null)
			{
				writer.Write(Value);
			}

			return writer.ToString();
		}
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = input.Split('\n');

		var result = ParseArray(lines[0]);

		for (var i = 1; i < lines.Length; i++)
		{
			result += ParseArray(lines[i]);
		}

		var part1 = result.Magnitude();
		var part2 = 0;

		for (var x = 0; x < lines.Length; x++)
		{
			for (var y = 0; y < lines.Length; y++)
			{
				if (x == y)
				{
					continue;
				}

				result = ParseArray(lines[x]) + ParseArray(lines[y]);
				var magnitude = result.Magnitude();

				if (part2 < magnitude)
				{
					part2 = magnitude;
				}
			}
		}

		return (part1.ToString(), part2.ToString());
	}

	SnailfishNumber ParseArray(ReadOnlySpan<char> line)
	{
		var number = new SnailfishNumber();
		var start = number;

		foreach (var c in line)
		{
			if (c == '[')
			{
				number.X = new SnailfishNumber
				{
					Parent = number
				};
				number.Y = new SnailfishNumber
				{
					Parent = number
				};
				number = number.X;
			}
			else if (c == ']')
			{
				number = number!.Parent!;
			}
			else if (c == ',')
			{
				number = number!.Parent!.Y!;
			}
			else
			{
				number.Value = c - '0';
			}
		}

		return start;
	}
}
