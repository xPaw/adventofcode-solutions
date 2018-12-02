window.AdventOfCode.Day2 = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => x.split( '' ) );

	let part1twos = 0;
	let part1threes = 0;
	let part2;

	for( const id of input )
	{
		const letters = new Map();

		for( const letter of id )
		{
			letters.set( letter, ( letters.get( letter ) || 0 ) + 1 );
		}

		const values = Array.from(letters.values());

		part1twos += +values.includes(2);
		part1threes += +values.includes(3);
	}

part2:
	for( const id of input )
	{
		for( const id2 of input )
		{
			let differences = 0;
			part2 = '';

			for( let i = 0; i < id.length; i++ )
			{
				if( id[ i ] !== id2[ i ] )
				{
					differences++;
				}
				else
				{
					part2 += id[ i ];
				}
			}

			if( differences === 1 )
			{
				break part2;
			}
		}
	}

	return [ part1twos * part1threes, part2 ];
};
