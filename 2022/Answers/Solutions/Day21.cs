using System;
using System.Collections.Generic;

namespace AdventOfCode;

[Answer(21)]
public class Day21 : IAnswer
{
	enum Operator
	{
		Add,
		Remove,
		Multiply,
		Divide,
	}

	interface INode
	{
		INode Reduce();
	}

	record Expression(Operator Operation, INode Left, INode Right) : INode
	{
		public INode Reduce()
		{
			var left = Left.Reduce();
			var right = Right.Reduce();

			if (left is Constant leftConstant && right is Constant rightConstant)
			{
				var value = Operation switch
				{
					Operator.Add => leftConstant.Value + rightConstant.Value,
					Operator.Remove => leftConstant.Value - rightConstant.Value,
					Operator.Multiply => leftConstant.Value * rightConstant.Value,
					Operator.Divide => leftConstant.Value / rightConstant.Value,
					_ => throw new Exception(),
				};

				return new Constant(value);
			}

			return new Expression(Operation, left, right);
		}
	}

	record Constant(long Value) : INode
	{
		public INode Reduce() => this;
	}

	record Variable() : INode
	{
		public INode Reduce() => this;
	}

	record Equal(INode Left, INode Right) : INode
	{
		public INode Reduce() => new Equal(Left.Reduce(), Right.Reduce());
	}

	public (string Part1, string Part2) Solve(string input)
	{
		var lines = new Dictionary<string, string[]>();

		foreach (var line in input.AsSpan().EnumerateLines())
		{
			var key = line[0..4].ToString();
			var formula = line[6..].ToString().Split(' ');

			lines[key] = formula;
		}

		INode Build(string key, bool part2 = false)
		{
			var line = lines[key];

			if (line.Length == 1)
			{
				if (part2 && key == "humn")
				{
					return new Variable();
				}

				return new Constant(int.Parse(line[0]));
			}

			return new Expression(
				line[1][0] switch
				{
					'*' => Operator.Multiply,
					'/' => Operator.Divide,
					'+' => Operator.Add,
					'-' => Operator.Remove,
					_ => throw new NotImplementedException(),
				},
				Build(line[0], part2),
				Build(line[2], part2)
			);
		}

		Equal Invert(Equal inputExpression)
		{
			if (inputExpression.Left is Constant)
			{
				return new Equal(inputExpression.Right, inputExpression.Left);
			}

			if (inputExpression.Left is not Expression expression)
			{
				throw new NotImplementedException();
			}

			var flippedOperator = expression.Operation switch
			{
				Operator.Add => Operator.Remove,
				Operator.Remove => Operator.Add,
				Operator.Multiply => Operator.Divide,
				Operator.Divide => Operator.Multiply,
				_ => throw new NotImplementedException(),
			};

			if (expression.Left is Constant && expression.Operation is Operator.Add or Operator.Multiply)
			{
				return new Equal(expression.Right, new Expression(flippedOperator, inputExpression.Right, expression.Left).Reduce());
			}

			return new Equal(expression.Left, new Expression(flippedOperator, inputExpression.Right, expression.Right).Reduce());
		}

		if (Build("root").Reduce() is not Constant part1)
		{
			throw new Exception();
		}

		var rootExpr = Build("root", true) as Expression;
		var root = new Equal(rootExpr!.Left, rootExpr.Right);

		while (root.Left is not Variable)
		{
			root = Invert(root);
		}

		if (root.Right is not Constant part2)
		{
			throw new Exception();
		}

		return (part1.Value.ToString(), part2.Value.ToString());
	}
}
