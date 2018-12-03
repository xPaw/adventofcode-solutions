window.AdventOfCode.Day3 = ( input ) =>
{
	input = input
		.split( '\n' );

	const stride = 1000;
	const grid = new Float32Array( stride * stride );
	const claims = new Float32Array( stride * stride );
	const claimed = new Set();
	const parse = /^#([0-9]+) @ ([0-9]+),([0-9]+): ([0-9]+)x([0-9]+)$/;

	for( const line of input )
	{
		const data = parse.exec( line );
		const id = +data[ 1 ];
		const x1 = +data[ 2 ];
		const y1 = +data[ 3 ];
		const width = +data[ 4 ];
		const height = +data[ 5 ];
		let overlapped = false;

		for( let x = x1; x < x1 + width; x++ )
		{
			for( let y = y1; y < y1 + height; y++ )
			{
				if( grid[ x * stride + y ] > 0 )
				{
					overlapped = true;

					claimed.delete( claims[ x * stride + y ] );
				}

				grid[ x * stride + y ]++;
				claims[ x * stride + y ] = id;
			}
		}

		if( !overlapped )
		{
			claimed.add( id );
		}
	}

	let overlaps = 0;

	for( let i = grid.length; i > 0; i-- )
	{
		if( grid[ i ] > 1 )
		{
			overlaps++;
		}
	}

	return [ overlaps, claimed.values().next().value ];
};
