using System;
using System.Runtime.CompilerServices;

namespace AdventOfCode;

public record struct Coord(int X, int Y)
{
	public static Coord operator +(Coord a, Coord b) => new(a.X + b.X, a.Y + b.Y);
	public static Coord operator +(Coord a, (int X, int Y) b) => new(a.X + b.X, a.Y + b.Y);

	public static Coord operator -(Coord a, Coord b) => new(a.X - b.X, a.Y - b.Y);
	public static Coord operator -(Coord a, (int X, int Y) b) => new(a.X - b.X, a.Y - b.Y);

	public readonly static Coord[] Directions =
	[
		new Coord(0, 1), // down
		new Coord(1, 0), // right
		new Coord(-1, 0), // left
		new Coord(0, -1), // up
	];
}

public readonly ref struct ReadOnlyGrid
{
	private readonly char OutOfBoundsCharacter;
	private readonly int Stride;

	public readonly ReadOnlySpan<char> Data { get; }
	public readonly int Width { get; }
	public readonly int Height { get; }

	public ReadOnlyGrid(ReadOnlySpan<char> data, char oob = '\0')
	{
		OutOfBoundsCharacter = oob;
		Data = data;
		Width = data.IndexOf('\n');
		Stride = Width + 1;
		Height = (data.Length + 1) / Stride;
	}

	public readonly char this[int y, int x]
	{
		get
		{
			if (y < 0 || x < 0 || y >= Height || x >= Width)
			{
				return OutOfBoundsCharacter;
			}

			return Data[y * Stride + x];
		}
	}

	public readonly char this[Coord d] => this[d.Y, d.X];

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Coord IndexOf(char c)
	{
		var a = Math.DivRem(Data.IndexOf(c), Stride);
		return new Coord(a.Remainder, a.Quotient);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public char InfiniteAt(int y, int x)
	{
		var r = y % Height;
		y = r < 0 ? r + Height : r;

		r = x % Width;
		x = r < 0 ? r + Width : r;

		return Data[y * Stride + x];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public char InfiniteAt(Coord d) => InfiniteAt(d.Y, d.X);
}
