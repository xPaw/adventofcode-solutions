window.AdventOfCode.Day12 = function( input )
{
	input = input
		.split( '\n' )
		.map( ( line ) => line
			.split( ' <-> ' )[ 1 ]
			.split( ', ' )
			.map( ( id ) => parseInt( id, 10 ) )
		);

	const visited = new Map();

	const visit = ( value, id ) =>
	{
		if( visited.has( id ) )
		{
			return 0;
		}

		visited.set( id, true );

		return value.reduce( ( total, nextId ) => total + visit( input[ nextId ], nextId ), 1 );
	};

	input = input.map( visit );

	const part1 = input[ 0 ];
	const part2 = input.reduce( ( total, x ) => x > 0 ? ++total : total, 0 );

	return [ part1, part2 ];
};
