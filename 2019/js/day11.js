const IntCode = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	let part1 = 0;
	let grid = robot( input );

	for( const key in grid )
	{
		for( const key2 in grid[ key ] ) // eslint-disable-line no-unused-vars
		{
			part1++;
		}
	}

	const part2 = [];
	grid = robot( input, 1 );

	for( const key in grid )
	{
		part2.push( '\n' );

		for( let i = 0; i < grid[ key ].length; i++ )
		{
			part2.push( grid[ key ][ i ] === 1 ? '00' : '  ' );
		}
	}

	return [ part1, part2.join( '' ) ];
};

function robot( input, defaultColor = 0 )
{
	let x = 0;
	let y = 0;
	let rotation = 0;
	const grid = [];

	const directions =
	[
		{ x: 0 , y: -1 },
		{ x: 1 , y: 0 },
		{ x: 0 , y: 1 },
		{ x: -1, y: 0 },
	];

	const stateMachine = new IntCode( input );

	do
	{
		if( !grid[ y ] )
		{
			grid[ y ] = [];
		}

		const [ color, turn ] = stateMachine.execute( [ grid[ y ][ x ] || defaultColor ] );

		grid[ y ][ x ] = color;

		rotation = rotation + ( turn ? 1 : -1 );
		rotation = ( ( rotation % 4 ) + 4 ) % 4;

		const direction = directions[ rotation ];
		x += direction.x;
		y += direction.y;
	}
	while( !stateMachine.halted );

	return grid;
}
