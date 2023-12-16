namespace AdventOfCode;

public readonly struct ReadOnlyGrid
{
	private readonly string Data;
	private readonly char OutOfBoundsCharacter;
	private readonly int Stride;
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
}
