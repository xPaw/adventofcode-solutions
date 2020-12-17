module.exports = ( input ) =>
{
	input = input.split( '\n\n' );

	const part1 = input
		.map( group => new Set( group.replace( /\n/g, '' ).split( '' ) ).size )
		.reduce( ( a, b ) => a + b, 0 );

	const part2 = input
		.map( group =>
		{
			const people = group.split( '\n' ).map( l => l.split( '' ) );
			const sum = new Map();

			for( const answers of people )
			{
				for( const answer of answers )
				{
					sum.set( answer, 1 + ( sum.get( answer ) || 0 ) );
				}
			}

			return [ ...sum.values() ].filter( a => a === people.length ).length;
		} )
		.reduce( ( a, b ) => a + b, 0 );

	return [ part1, part2 ];
};
