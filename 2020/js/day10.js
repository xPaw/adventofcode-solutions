module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => parseInt( x, 10 ) )
		.sort( ( a, b ) => a - b );

	let current = 0;
	let diffs =
	[
		0,
		0,
		0,
		1, // 3 higher than the highest-rated adapter.
	];
	const paths =
	[
		1, // device's built-in
	];

	for( const adapter of input )
	{
		diffs[ adapter - current ]++;
		current = adapter;

		paths[ adapter ] =
			( paths[ adapter - 3 ] || 0 ) +
			( paths[ adapter - 2 ] || 0 ) +
			( paths[ adapter - 1 ] || 0 );
	}

	const part1 = diffs[ 3 ] * diffs[ 1 ];
	const part2 = paths[ paths.length - 1 ];

	return [ part1, part2 ];
};
