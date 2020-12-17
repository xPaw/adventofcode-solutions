module.exports = ( input ) =>
{
	const loc = ( x, y, z, w ) => `${x},${y},${z},${w}`;
	const originalSpace = new Set();

	input = input.split( '\n' ).map( x => x.split( '' ).map( x => x === '#' ) );

	input.forEach( ( row, y ) => row.forEach( ( cell, x ) =>
	{
		if( cell )
		{
			originalSpace.add( loc( x, y, 0, 0 ) );
		}
	} ) );

	function countAdjacent( space, baseX, baseY, baseZ, baseW, p2 )
	{
		let count = 0;

		for( let w = ( p2 ? baseW - 1 : 0 ); w <= ( p2 ? baseW + 1 : 0 ); w++)
		{
			for( let z = baseZ - 1; z <= baseZ + 1; z++ )
			{
				for( let y = baseY - 1; y <= baseY + 1; y++ )
				{
					for( let x = baseX - 1; x <= baseX + 1; x++ )
					{
						if ( space.has( loc( x, y, z, w ) ) && ( baseX !== x || baseY !== y || baseZ !== z || baseW !== w ) )
						{
							count++;
						}
					}
				}
			}
		}

		return count;
	}

	function run( p2 )
	{
		let space = new Set( originalSpace );
		let x1 = 0;
		let x2 = input[ 0 ].length;
		let y1 = 0;
		let y2 = input.length;
		let z1 = 0;
		let z2 = 1;
		let w1 = 0;
		let w2 = 1;

		for( let cycles = 0; cycles < 6; cycles++ )
		{
			let newSpace = new Set();

			x1--;
			x2++;
			y1--;
			y2++;
			z1--;
			z2++;

			if( p2 )
			{
				w1--;
				w2++;
			}

			for( let w = w1; w < w2; w++ )
			{
				for( let z = z1; z < z2; z++ )
				{
					for( let y = y1; y < y2; y++ )
					{
						for( let x = x1; x < x2; x++ )
						{
							let occupied = countAdjacent( space, x, y, z, w, p2 );

							if( occupied === 3 || ( occupied === 2 && space.has( loc( x, y, z, w ) ) ) )
							{
								newSpace.add( loc( x, y, z, w ) );
							}
						}
					}
				}
			}
	
			space = newSpace;
		}

		return space.size;
	}

	const part1 = run( false );
	const part2 = run( true );

	return [ part1, part2 ];
};
