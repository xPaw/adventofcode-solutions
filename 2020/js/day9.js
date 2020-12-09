module.exports = ( input ) =>
{
	input = input.split( '\n' ).map( x => parseInt( x, 10 ) );

	const PREAMBLE_SIZE = 25;
	let part1 = 0;

outer:
	for( let i = PREAMBLE_SIZE; i < input.length; i++ )
	{
		for( let j = i - PREAMBLE_SIZE; j < i; j++ )
		{
			for( let y = i - PREAMBLE_SIZE; y < i; y++ )
			{
				if( input[ j ] + input[ y ] === input[ i ] )
				{
					continue outer;
				}
			}
		}

		part1 = input[ i ];
		break;
	}

	let sum = 0;
	let lower = 0;
	let upper = 0;

	while( sum !== part1 || upper === lower )
	{
		if( sum < part1 )
		{
			upper++;
			sum += input[ upper ];
		}

		if( sum > part1 )
		{
			lower++;
			sum -= input[ lower ];
		}
	}

	const slice = input.slice( lower + 1, upper + 1 );
	const part2 = Math.min( ...slice ) + Math.max( ...slice );

	return [ part1, part2 ];
};
