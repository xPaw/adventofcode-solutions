module.exports = ( input ) =>
{
	input = input.split( "\n" );

	let mask;
	let mask1;
	let mask2;
	let memoryP1 = {};
	let memoryP2 = {};

	for( const line of input )
	{
		if( line[ 1 ] === 'a' ) // mask
		{
			mask  = line.match( /mask = (?<mask>.+)/ ).groups.mask;
			mask1 = BigInt( '0b' + mask.replaceAll( 'X', 1 ) );
			mask2 = BigInt( '0b' + mask.replaceAll( 'X', 0 ) );
			mask  = mask.split( '' );
		}
		else // mem
		{
			const mem = line.match( /mem\[(?<addr>\d+)] = (?<value>\d+)/ ).groups;
			const addr = Number( mem.addr );
			const value = BigInt( mem.value );

			// part 1
			memoryP1[ addr ] = value & mask1 | mask2;

			// part 2
			const paddedAddr = addr.toString( 2 ).padStart( 36, '0' );
			let addrs = [ 0n ];

			mask.forEach( ( bit, index ) =>
			{
				addrs = addrs.map( v => v << 1n );

				if( bit === 'X' )
				{
					addrs = addrs.concat( addrs.map( v => v | 1n ) );
				}
				else if( bit === '1' || paddedAddr[ index ] === '1' )
				{
					addrs = addrs.map( v => v | 1n );
				}
			} );

			for( const addr of addrs )
			{
				memoryP2[ addr ] = value;
			}
		}
	}

	const bigSum = input => Number( Object.values( input ).reduce( ( a, b ) => a + b, 0n ) );
	const part1 = bigSum( memoryP1 );
	const part2 = bigSum( memoryP2 );

	return [ part1, part2 ];
};
