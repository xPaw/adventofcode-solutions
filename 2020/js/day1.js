module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => parseInt( x, 10 ) );

	let part1 = 0;
	let part2 = 0;

	for( let a = 0; a < input.length; a++ )
	{
		for( let b = a + 1; b < input.length; b++ )
		{
			if( !part1 && input[ a ] + input[ b ] === 2020 )
			{
				part1 = input[ a ] * input[ b ];
			}

			for( let c = b + 1; c < input.length; c++ )
			{
				if( input[ a ] + input[ b ] + input[ c ] === 2020 )
				{
					part2 = input[ a ] * input[ b ] * input[ c ];
				}
			}
		}
	}

	return [ part1, part2 ];
};
