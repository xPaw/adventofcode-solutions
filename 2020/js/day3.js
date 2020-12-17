module.exports = ( input ) =>
{
	input = input.split( '\n' ).map( row => row.split( '' ).map( cell => cell === '#' ) );

	const width = input[ 0 ].length;

	const slope = ( right, down ) =>
	{
		let trees = 0;

		for( let y = 0; y < input.length; y += down )
		{
			const x = ( right * y / down ) % width;

			if( input[ y ][ x ] )
			{
				trees++;
			}
		}

		return trees;
	};

	const part1 = slope( 3, 1 );
	const part2 = slope( 1, 1 ) * part1 * slope( 5, 1 ) * slope( 7, 1 ) * slope( 1, 2 );

	return [ part1, part2 ];
};
