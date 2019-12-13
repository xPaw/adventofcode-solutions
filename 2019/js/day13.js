const IntCode = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	input[ 0 ] = 2;
	const stateMachine = new IntCode( input );

	let part1 = 0;
	let part2 = 0;
	let ballX = 0;
	let paddleX = 0;
	let nextInput = 0;

	do
	{
		const output = stateMachine.execute( [ nextInput ] );

		for( let i = 0; i < output.length; i += 3 )
		{
			const x = output[ i ];
			const y = output[ i + 1 ];
			const tile = output[ i + 2 ];

			if( x === -1 && y === 0 )
			{
				part2 = tile;
			}
			else if( tile === 2 )
			{
				part1++;
			}
			else if( tile === 3 )
			{
				paddleX = x;
			}
			else if( tile === 4 )
			{
				ballX = x;
			}
		}

		if( paddleX < ballX )
		{
			nextInput = 1;
			paddleX++;
		}
		else if( paddleX > ballX )
		{
			nextInput = -1;
			paddleX--;
		}
		else
		{
			nextInput = 0;
		}
	}
	while( !stateMachine.halted );

	return [ part1, part2 ];
};
