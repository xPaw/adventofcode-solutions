const stateMachine = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	stateMachine.reset();
	input[ 0 ] = 2;

	let part1 = 0;
	let part2 = 0;
	let output = stateMachine.run( input, [ 0 ] );
	const ball = { x: 0, y: 0 };
	const paddle = { x: 0, y: 0 };

	for( let i = 0; i < output.length; i+= 3 )
	{
		const x = output[ i ];
		const y = output[ i + 1 ];
		const tile = output[ i + 2 ];

		if( tile === 2 )
		{
			part1++;
		}
		else if( tile === 3 )
		{
			paddle.x = x;
			paddle.y = y;
		}
		else if( tile === 4 )
		{
			ball.x = x;
			ball.y = y;
		}
	}

	let nextInput = 0;

	do
	{
		output = stateMachine.run( input, [ nextInput ] );

		for( let i = 0; i < output.length; i+= 3 )
		{
			const x = output[ i ];
			const y = output[ i + 1 ];
			part2 = output[ i + 2 ];

			ball.x = x;
			ball.y = y;
		}

		if( paddle.x < ball.x )
		{
			nextInput = 1;
			paddle.x++;
		}
		else if( paddle.x > ball.x )
		{
			nextInput = -1;
			paddle.x--;
		}
		else
		{
			nextInput = 0;
		}
	}
	while( stateMachine.currentState !== stateMachine.State.HALTED );

	return [ part1, part2 ];
};
