const test = require( 'ava' );
const { readFileSync } = require( 'fs' );

process.chdir( __dirname );

test( '2020', t =>
{
	assert( t, 1, 63616, 67877784 );
	assert( t, 2, 439, 584 );
	assert( t, 3, 276, 7812180000 );
	assert( t, 4, 208, 167 );
	assert( t, 5, 901, 661 );
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
