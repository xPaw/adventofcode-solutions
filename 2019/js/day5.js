const stateMachine = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	const part1 = stateMachine( [ ...input ], [ 1 ] ).pop();
	const part2 = stateMachine( [ ...input ], [ 5 ] ).pop();

	return [ part1, part2 ];
};
