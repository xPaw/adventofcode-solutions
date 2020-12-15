module.exports = ( input ) =>
{
	input = input.split( ',' ).map( x => parseInt( x, 10 ) );

	const memory = new Int32Array( 30000000 ).fill( -1, 0, 30000000 );
	let lastNumber = 0;
	let newNumber = 0;
	let part1 = 0;
	let part2 = 0;

	for( let i = 0; i < input.length; i++ )
	{
		memory[ input[ i ] ] = i;
	}

	for( let i = input.length; i < 30000000; i++ )
	{
		lastNumber = newNumber;
		const seen = memory[ newNumber ];

		if( seen > -1 )
		{
			newNumber = i - seen;
		}
		else
		{
			newNumber = 0;
		}

		memory[ lastNumber ] = i;

		if( i === 2019 )
		{
			part1 = lastNumber;
		}
	}

	part2 = lastNumber;

	return [ part1, part2 ];
};
