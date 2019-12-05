module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	input[ 1 ] = 12;
	input[ 2 ] = 2;
	const part1 = stateMachine( [ ...input ] );
	let part2 = 0;

bruteforce:
	for( let y = 0; y <= input.length; y++ )
	{
		input[ 1 ] = y;

		for( let x = 0; x <= input.length; x++ )
		{
			input[ 2 ] = x;

			if( stateMachine( [ ...input ] ) === 19690720 )
			{
				part2 = 100 * y + x;
				break bruteforce;
			}
		}
	}

	return [ part1, part2 ];
};

function stateMachine( input )
{
halt:
	for( let i = 0; i < input.length; i++ )
	{
		const opcode = input[ i ];

		switch( opcode )
		{
			case 1:
			{
				const result = input[ input[ ++i ] ] + input[ input[ ++i ] ];
				input[ input[ ++i ] ] = result;

				break;
			}
			case 2:
			{
				const result = input[ input[ ++i ] ] * input[ input[ ++i ] ];
				input[ input[ ++i ] ] = result;

				break;
			}
			case 99:
			{
				break halt;
			}
		}
	}

	return input[ 0 ];
}
