window.AdventOfCode.Day5 = ( input ) =>
{
	const reactions = [];

	for( let i = 65; i <= 90; i++ )
	{
		const uppercase = String.fromCodePoint( i );
		const lowercase = String.fromCodePoint( i + 32 );

		reactions.push( uppercase + lowercase );
		reactions.push( lowercase + uppercase );
	}

	const reduceReactions = new RegExp( reactions.join( '|' ), 'g' );
	const reducedLengths = [];

	// 64 is @ which doesn't actually replace anything and we use that for part 1
	for( let i = 64; i <= 90; i++ )
	{
		const uppercase = String.fromCodePoint( i );

		let previous;
		let current = input.replace( new RegExp( uppercase, 'ig' ), '' );

		do
		{
			previous = current;
			current = current.replace( reduceReactions, '' );
		}
		while( previous !== current );

		reducedLengths.push( current.length );

		// Pass part1 output into part2
		if( i === 64 )
		{
			input = current;
		}
	}

	return [ reducedLengths[ 0 ], Math.min( ...reducedLengths ) ];
};
