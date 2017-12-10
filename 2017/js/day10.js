window.AdventOfCode.Day10 = function( input )
{
	const toHex = ( sequence ) => sequence.map( ( x ) => x.toString( 16 ).padStart( 2, '0' ) ).join( '' );
	
	const dense = ( list ) =>
	{
		let denseList = [];
		
		for( let i = 0; i < 16; i++ )
		{
			denseList.push( list.slice( i * 16, 16 + i * 16 ).reduce( ( a, b ) => a ^ b ) );
		}
		
		return denseList;
	};
	
	const hash = ( list, lengths, rounds = 1 ) =>
	{
		let position = 0;
		let globalSkip = 0;
		
		for( let round = 0; round < rounds; round++ )
		{
			for( let i = 0; i < lengths.length; i++ )
			{
				let reversed = [];
				
				for( let skip = 0; skip < lengths[ i ]; skip++ )
				{
					reversed.unshift( list[ ( position + skip ) % list.length ] );
				}
				
				for( let skip = 0; skip < lengths[ i ]; skip++ )
				{
					list[ ( position + skip ) % list.length ] = reversed[ skip ];
				}
				
				position = ( position + lengths[ i ] + globalSkip++ ) % list.length;
			}
		}
		
		return list;
	};
	
	const lengths = input
		.split( ',' )
		.map( ( num ) => parseInt( num, 10 ) );
	let part1 = hash( [ ...Array( 256 ).keys() ], lengths );
	part1 = part1[ 0 ] * part1[ 1 ];
	
	const bytes = input
		.split( '' )
		.map( ( c ) => c.charCodeAt( 0 ) )
		.concat( [ 17, 31, 73, 47, 23 ] );
	let part2 = hash( [ ...Array( 256 ).keys() ], bytes, 64 );
	part2 = toHex( dense( part2 ) );
	
	return [ part1, part2 ];
};
