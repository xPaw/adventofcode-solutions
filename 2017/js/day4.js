window.AdventOfCode.Day4 = function( input )
{
	input = input.split( '\n' ).map( ( pass ) => pass.split( ' ' ) );

	const countValid = ( counter, pass ) =>
	{
		if( ( new Set( pass ) ).size === pass.length )
		{
			counter++;
		}

		return counter;
	};

	const part1 = input.reduce( countValid, 0 );

	const part2 = input
		.map( ( pass ) => pass.map( ( word ) => [ ...word ].sort( ).join( '' ) ) )
		.reduce( countValid, 0 );

	return [ part1, part2 ];
};
