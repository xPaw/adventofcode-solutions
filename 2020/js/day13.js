module.exports = ( input ) =>
{
	input = input.split( "\n" );

	const timestamp = parseInt( input[ 0 ], 10 );
	const buses = input[ 1 ]
		.split( ',' )
		.map( ( bus, i ) => bus === 'x' ? null : { bus: parseInt( bus, 10 ), index: i } )
		.filter( x => x !== null );

	let time = Number.MAX_SAFE_INTEGER;
	let multiplier = null;
	let part1 = 0;
	let part2 = 0;

	for( const bus of buses )
	{
		const delay = bus.bus - ( timestamp % bus.bus );

		if( time > delay )
		{
			time = delay;
			part1 = delay * bus.bus;
		}

		if( multiplier === null )
		{
			multiplier = bus.bus;
			continue;
		}

		while( true ) // eslint-disable-line
		{
			if( ( part2 + bus.index ) % bus.bus === 0 )
			{
				multiplier *= bus.bus;
				break;
			}

			part2 += multiplier;
		}
	}

	return [ part1, part2 ];
};
