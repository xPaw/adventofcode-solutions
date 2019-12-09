const stateMachine = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	input[ 1 ] = 12;
	input[ 2 ] = 2;
	const state = [ ...input ];
	stateMachine( state );
	const part1 = state[ 0 ];
	let part2 = 0;

bruteforce:
	for( let y = 0; y <= input.length; y++ )
	{
		input[ 1 ] = y;

		for( let x = 0; x <= input.length; x++ )
		{
			input[ 2 ] = x;

			const state = [ ...input ];
			stateMachine( state );

			if( state[ 0 ] === 19690720 )
			{
				part2 = 100 * y + x;
				break bruteforce;
			}
		}
	}

	return [ part1, part2 ];
};
