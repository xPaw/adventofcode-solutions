module.exports = ( input ) =>
{
	input = input.split( '\n' ).map( x => parseInt( x, 10 ) );

	const PREAMBLE_SIZE = 25;
	let part1 = 0;
	let part2 = 0;

	for( let i = PREAMBLE_SIZE; i < input.length; i++ )
	{
		const sums = [];

		for( let j = i - PREAMBLE_SIZE; j < i; j++ )
		{
			for( let y = i - PREAMBLE_SIZE; y < i; y++ )
			{
				sums[ input[ j ] + input[ y ] ] = true;
			}
		}

		if( sums[ input[ i ] ] )
		{
			continue;
		}

		part1 = input[ i ];
		break;
	}

	for( let i = 0; i < input.length - 1; i++ )
	{
		let currentSum = 0;

		for( let j = i; j < input.length; j++ )
		{
			currentSum += input[ j ];

			if( currentSum === part1 && j - i > 1 )
			{
				const slice = input.slice( i, j + 1 );
				part2 = Math.min( ...slice ) + Math.max( ...slice );
			}
		}
	}

	return [ part1, part2 ];
};
