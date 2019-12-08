const COLOR_BLACK = 0;
const COLOR_WHITE = 1;
const COLOR_TRANSPARENT = 2;

module.exports = ( input ) =>
{
	const w = 25;
	const h = 6;
	const stride = w * h;
	const layers = input.match( new RegExp( '.{1,' + stride + '}', 'g' ) );
	let lowestCounts = [ Number.MAX_SAFE_INTEGER ];
	let pixels = new Array( stride ).fill( COLOR_TRANSPARENT );

	for( const layer of layers )
	{
		let counts = new Array( 10 ).fill( 0 );

		for( let i = 0; i < stride; i++ )
		{
			const pixel = +layer[ i ];
			counts[ pixel ]++;

			if( pixels[ i ] === COLOR_TRANSPARENT )
			{
				pixels[ i ] = pixel;
			}
		}

		if( lowestCounts[ 0 ] > counts[ 0 ] )
		{
			lowestCounts = counts;
		}
	}

	const part1 = lowestCounts[ 1 ] * lowestCounts[ 2 ];
	let part2 = [];

	for( let i = 0; i < stride; i++ )
	{
		if( i % w === 0 )
		{
			part2.push( '\n' );
		}

		part2.push( pixels[ i ] === COLOR_BLACK ? '\u2B1B' : '\u2B1C' );
	}

	return [ part1, part2.join( '' ) ];
};
