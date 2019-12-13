const IntCode = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	const part1 = new IntCode( input ).execute( [ 1 ] ).pop();
	const part2 = new IntCode( input ).execute( [ 2 ] ).pop();

	return [ part1, part2 ];
};
