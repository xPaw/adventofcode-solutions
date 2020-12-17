window.AdventOfCode.Day3 = function( input )
{
	const GetUniqueHouses = function( numberOfSantas )
	{
		let uniqueHousesCount = 1;
		const uniqueHouses =
		{
			'0x0': 1
		};

		for( let santa = 0; santa < numberOfSantas; santa++ )
		{
			// Santa is delivering presents to an infinite two-dimensional grid of houses.
			let x = 0, y = 0;

			// Then an elf at the North Pole calls him via radio and tells him where to move next.
			for( let i = santa; i < input.length; i += numberOfSantas )
			{
				switch( input[ i ] )
				{
					case '^': x++; break;
					case 'v': x--; break;
					case '>': y++; break;
					case '<': y--; break;
				}

				const coord = x + 'x' + y;

				if( !uniqueHouses[ coord ] )
				{
					uniqueHousesCount++;

					uniqueHouses[ coord ] = 1;
				}
			}
		}

		return uniqueHousesCount;
	};

	return [ GetUniqueHouses( 1 ), GetUniqueHouses( 2 ) ];
};
