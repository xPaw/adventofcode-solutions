
using System;

namespace AdventOfCode;

public static class Utils
{
	extension(ReadOnlySpan<char> target)
	{
		public int ParseInt()
		{
			var result = 0;

			while (target.Length > 0)
			{
				result = 10 * result + target[0] - '0';
				target = target[1..];
			}

			return result;
		}

		public long ParseLong()
		{
			var result = 0L;

			while (target.Length > 0)
			{
				result = 10 * result + target[0] - '0';
				target = target[1..];
			}

			return result;
		}
	}
}
