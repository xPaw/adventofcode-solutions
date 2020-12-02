const test = require( 'ava' );
const { readFileSync } = require( 'fs' );

process.chdir( __dirname );

test( '2020', t =>
{
	assert( t, 1, 63616, 67877784 );
	assert( t, 2, 439, 584 );
} );

function assert( t, day, answer1, answer2 )
{
	let input = readFileSync( `./data/day${day}.txt` ).toString();
	input = input.replace( /\r/g, '' ).trim();

	const solution = require( `./js/day${day}.js` );
	const result = solution( input );

	t.is( result[ 0 ], answer1 );
	t.is( result[ 1 ], answer2 );
}
