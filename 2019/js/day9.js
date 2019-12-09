const stateMachine = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	let part1 = stateMachine( [ ...input ], [ 1 ] ).pop();
	let part2 = stateMachine( [ ...input ], [ 2 ] ).pop();

	return [ part1, part2 ];
};
