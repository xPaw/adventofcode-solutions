using System;
using System.Runtime.CompilerServices;

namespace AdventOfCode;

public readonly struct ReadOnlyGrid
{
	private readonly char OutOfBoundsCharacter;
	private readonly int Stride;

	public readonly string Data { get; }
	public readonly int Width { get; }
	public readonly int Height { get; }

	public ReadOnlyGrid(string data, char oob = '\0')
	{
		OutOfBoundsCharacter = oob;
		Data = data;
		Width = data.IndexOf('\n');
		Stride = Width + 1;
		Height = (data.Length + 1) / Stride;
	}

	public readonly char this[int row, int column]
	{
		get
		{
			if (row < 0 || column < 0 || row >= Height || column >= Width)
			{
				return OutOfBoundsCharacter;
			}

			return Data[row * Stride + column];
		}
	}

	public readonly char this[(int row, int column) d] => this[d.row, d.column];

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public (int Row, int Column) IndexOf(char c) => Math.DivRem(Data.IndexOf(c), Stride);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public char InfiniteAt(int row, int column)
	{
		var r = row % Height;
		row = r < 0 ? r + Height : r;

		r = column % Width;
		column = r < 0 ? r + Width : r;

		return Data[row * Stride + column];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public char InfiniteAt((int row, int column) d) => InfiniteAt(d.row, d.column);
}
