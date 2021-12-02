using System;

namespace AdventOfCode2021;

[AttributeUsage(AttributeTargets.Class)]
class AnswerAttribute : Attribute
{
	public int Day { get; init; }
	public AnswerAttribute(int day) => Day = day;
}
