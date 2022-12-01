using System;

namespace AdventOfCode;

[AttributeUsage(AttributeTargets.Class)]
class AnswerAttribute : Attribute
{
	public int Day { get; init; }
	public bool Slow { get; init; }
	public AnswerAttribute(int day) => Day = day;
	public AnswerAttribute(int day, bool slow)
	{
		Day = day;
		Slow = slow;
	}
}
