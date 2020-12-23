module.exports = ( input ) =>
{
	input = input.split( '' ).map( x => parseInt( x, 10 ) );

	const part2 = 0;

	for( let i = 0; i < 100; i++ )
	{
		const takenCups = input.slice( 1, 4 );
		const remainingCups = [ input[ 0 ], ...input.slice( 4 ) ];
		let destination = input[ 0 ] - 1;
		let destinationPosition = -1;

		do
		{
			destinationPosition = remainingCups.indexOf( destination-- );

			if( destination === -1 )
			{
				destination = input.length;
			}
		}
		while( destinationPosition === -1 );

		input =
		[
			...remainingCups.slice( 1, destinationPosition + 1 ),
			...takenCups,
			...remainingCups.slice( destinationPosition + 1 ),
			remainingCups[ 0 ]
		];
	}

	const one = input.indexOf( 1 );
	const part1 = parseInt( [ ...input.slice( one + 1 ), ...input.slice( 0, one ) ].join( '' ), 10 );

	return [ part1, part2 ];
};
