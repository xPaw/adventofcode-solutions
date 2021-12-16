using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021;

[Answer(16)]
class Day16 : IAnswer
{
	byte[]? Data;
	int BitOffset;
	int ByteOffset;

	int Part1;

	public (string Part1, string Part2) Solve(string input)
	{
		Data = new byte[input.Length];

		for (var i = 0; i < input.Length; i++)
		{
			Data[i] = input[i] switch
			{
				'0' => 0,
				'1' => 1,
				'2' => 2,
				'3' => 3,
				'4' => 4,
				'5' => 5,
				'6' => 6,
				'7' => 7,
				'8' => 8,
				'9' => 9,
				'A' => 0xA,
				'B' => 0xB,
				'C' => 0xC,
				'D' => 0xD,
				'E' => 0xE,
				'F' => 0xF,
				_ => throw new NotImplementedException(),
			};
		}

		var part2 = ReadSinglePacket();

		return (Part1.ToString(), part2.ToString());
	}

	long ReadSinglePacket()
	{
		var version = ReadInt(3);
		var type = ReadInt(3);

#if DEBUG
		Console.WriteLine($"Version {version} - Type {type}");
#endif

		Part1 += version;

		return type switch
		{
			4 => ReadVarInt(),
			_ => ReadOperator(type),
		};
	}

	List<long> ReadPacketsByLength(int packetLength)
	{
		var packetEnd = ByteOffset + packetLength / 4;
		var values = new List<long>();

		while (ByteOffset < packetEnd)
		{
			values.Add(ReadSinglePacket());
		}

		return values;
	}

	List<long> ReadPackets(int packets)
	{
		var values = new List<long>();

		while (packets-- > 0)
		{
			values.Add(ReadSinglePacket());
		}

		return values;
	}

	long ReadOperator(int type)
	{
		var lengthType = ReadBit();

#if DEBUG
		Console.WriteLine($"Length type {lengthType}");
#endif

		var values = lengthType switch
		{
			0 => ReadPacketsByLength(ReadInt(15)),
			1 => ReadPackets(ReadInt(11)),
			_ => throw new NotImplementedException(),
		};

		return type switch
		{
			0 => values.Sum(),
			1 => values.Aggregate(1L, (acc, val) => acc * val),
			2 => values.Min(),
			3 => values.Max(),
			5 => values[0] > values[1] ? 1L : 0L,
			6 => values[0] < values[1] ? 1L : 0L,
			7 => values[0] == values[1] ? 1L : 0L,
			_ => throw new NotImplementedException(),
		};
	}

	int ReadInt(int bitLength)
	{
		int value = 0;

		for (var p = 0; p < bitLength; p++)
		{
			value |= ReadBit() << (bitLength - 1 - p);
		}

#if DEBUG
		Console.WriteLine($"Read int: {value}");
#endif

		return value;
	}

	long ReadVarInt()
	{
		long value = 0;

		while (true)
		{
			var continued = ReadBit();

			value <<= 4;
			value += ReadInt(4);

			if (continued == 0)
			{
				break;
			}
		}

#if DEBUG
		Console.WriteLine($"Read literal value: {value}");
#endif

		return value;
	}

	byte ReadBit()
	{
		var bit = (Data![ByteOffset] >> (3 - BitOffset)) & 1;

		BitOffset = (BitOffset + 1) % 4;

		if (BitOffset == 0)
		{
			ByteOffset++;
		}

		return (byte)bit;
	}
}
