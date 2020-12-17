const COLOR_BLACK = 0;
const COLOR_TRANSPARENT = 2;

module.exports = ( input ) =>
{
	const w = 25;
	const h = 6;
	const stride = w * h;
	const layersCount = input.length / stride;
	const layers = new Array( layersCount );
	const pixels = new Array( stride ).fill( COLOR_TRANSPARENT );
	let lowestCounts = [ Number.MAX_SAFE_INTEGER, 0, 0 ];

	for( let i = 0, o = 0; i < layersCount; i++, o += stride )
	{
		layers[ i ] = input.substr( o, stride );
	}

	for( const layer of layers )
	{
		const counts = [ 0, 0, 0 ];

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
	const part2 = [];

	for( let i = 0; i < stride; i++ )
	{
		if( i % w === 0 )
		{
			part2.push( '\n' );
		}

		part2.push( pixels[ i ] === COLOR_BLACK ? '  ' : '00' );
	}

	return [ part1, part2.join( '' ) ];
};
