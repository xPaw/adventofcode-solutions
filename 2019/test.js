const test = require( 'ava' );
const { readFileSync } = require( 'fs' );

process.chdir( __dirname );

test( '2019', t =>
{
	assert( t, 1, 3311492, 4964376 );
	assert( t, 2, 3058646, 8976 );
	assert( t, 4, 1955, 1319 );
	assert( t, 5, 13087969, 14110739 );
	assert( t, 6, 106065, 253 );
	assert( t, 7, 87138, 0 );
	assert( t, 8, 1965, `
  0000    00000000  00    00      0000  00      00
00    00        00  00  00          00  00      00
00            00    0000            00    00  00  
00  0000    00      00  00          00      00    
00    00  00        00  00    00    00      00    
  000000  00000000  00    00    0000        00    ` );
	assert( t, 9, 4288078517, 69256 );
	assert( t, 11, 2226, `
  00    00  000000      0000    00        00000000  00    00  00        00000000    
  00    00  00    00  00    00  00              00  00  00    00        00            
  00000000  000000    00        00            00    0000      00        000000        
  00    00  00    00  00  0000  00          00      00  00    00        00          
  00    00  00    00  00    00  00        00        00  00    00        00        
  00    00  000000      000000  00000000  00000000  00    00  00000000  00        ` );
	assert( t, 13, 296, 13824 );
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
