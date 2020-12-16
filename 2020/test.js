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
	assert( t, 6, 6565, 3137 );
	assert( t, 7, 169, 82372 );
	assert( t, 8, 2003, 1984 );
	assert( t, 9, 556543474, 76096372 );
	assert( t, 10, 2760, 13816758796288 );
	assert( t, 11, 2296, 2089 );
	assert( t, 12, 845, 27016 );
	assert( t, 13, 138, 226845233210288 );
	assert( t, 14, 18630548206046, 4254673508445 );
	assert( t, 15, 1194, 48710 );
	assert( t, 16, 20231, 1940065747861 );
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
