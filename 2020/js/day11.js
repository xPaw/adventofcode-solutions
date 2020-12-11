module.exports = ( input ) =>
{
	const FLOOR = 0;
	const SEAT = 1;
	const OCCUPIED = 2;

	input = input
		.split( '\n' )
		.map( x => x.split( '' ).map( x => x === '.' ? FLOOR : SEAT ) );

	const width = input[ 0 ].length;
	const height = input.length;

	function copy()
	{
		const newArray = [];

		for( let i = 0; i < input.length; i++ )
		{
			newArray[ i ] = input[ i ].slice();
		}

		return newArray;
	}

	function get( y, x )
	{
		if( y < 0 || y >= height || x < 0 || x >= width )
		{
			return FLOOR;
		}

		return input[ y ][ x ];
	}

	function run( seats, countAdjacent )
	{
		let changed = true;

		do
		{
			const newInput = copy();
			changed = false;

			for( let y = 0; y < height; y++ )
			{
				for( let x = 0; x < width; x++ )
				{
					if( input[ y ][ x ] === FLOOR )
					{
						continue;
					}

					const occupied = countAdjacent( y, x );

					if( occupied === 0 && input[ y ][ x ] !== OCCUPIED )
					{
						newInput[ y ][ x ] = OCCUPIED;
						changed = true;
					}
					else if( occupied >= seats && input[ y ][ x ] !== SEAT )
					{
						newInput[ y ][ x ] = SEAT;
						changed = true;
					}
				}
			}

			input = newInput;
		}
		while( changed );

		let occupied = 0;

		for( let y = 0; y < height; y++ )
		{
			for( let x = 0; x < width; x++ )
			{
				if( input[ y ][ x ] === OCCUPIED )
				{
					input[ y ][ x ] = SEAT;
					occupied++;
				}
			}
		}

		return occupied;
	}

	const part1 = run( 4, ( y, x ) =>
	{
		let occupied = 0;

		occupied += get( y - 1, x     ) === OCCUPIED ? 1 : 0;
		occupied += get( y + 1, x     ) === OCCUPIED ? 1 : 0;
		occupied += get( y    , x - 1 ) === OCCUPIED ? 1 : 0;
		occupied += get( y    , x + 1 ) === OCCUPIED ? 1 : 0;
		occupied += get( y - 1, x - 1 ) === OCCUPIED ? 1 : 0;
		occupied += get( y + 1, x + 1 ) === OCCUPIED ? 1 : 0;
		occupied += get( y - 1, x + 1 ) === OCCUPIED ? 1 : 0;
		occupied += get( y + 1, x - 1 ) === OCCUPIED ? 1 : 0;

		return occupied;
	} );

	const part2 = run( 5, ( baseY, baseX ) =>
	{
		let occupied = 0;

		for( let y = baseY - 1; y >= 0; y-- )
		{
			const pos = get( y, baseX );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let y = baseY + 1; y < height; y++ )
		{
			const pos = get( y, baseX );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let x = baseX - 1; x >= 0; x-- )
		{
			const pos = get( baseY, x );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let x = baseX + 1; x < width; x++ )
		{
			const pos = get( baseY, x );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let x = baseX - 1, y = baseY - 1; x >= 0 && y >= 0; x--, y-- )
		{
			const pos = get( y, x );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let x = baseX + 1, y = baseY + 1; x < width && y < height; x++, y++ )
		{
			const pos = get( y, x );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let x = baseX + 1, y = baseY - 1; x < width && y >= 0; x++, y-- )
		{
			const pos = get( y, x );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		for( let x = baseX - 1, y = baseY + 1; x >= 0 && y < height; x--, y++ )
		{
			const pos = get( y, x );

			if( pos !== FLOOR )
			{
				if( pos === OCCUPIED )
				{
					occupied++;
				}

				break;
			}
		}

		return occupied;
	} );

	return [ part1, part2 ];
};
