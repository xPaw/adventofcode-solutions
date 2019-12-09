"use strict";

process.chdir( __dirname );

const { readFileSync } = require( 'fs' );
const day = parseInt( process.argv[ 2 ], 10 );

if( isNaN( day ) )
{
	console.error( 'Usage: node index.js <day> [input]' );
	console.error( 'If input is not specified, reads from data/day%.txt' );
	process.exit( 1 );
}

let runs = 1;

if( process.argv[ 3 ] === 'bench' )
{
	delete process.argv[ 3 ];
	runs = parseInt( process.argv[ 4 ], 10 ) || 1000;
}

let input = process.argv[ 3 ] || readFileSync( `./data/day${day}.txt` ).toString();
input = input.replace( /\r/g, '' ).trim();

let result;
const solution = require( `./js/day${day}.js` );
const startTime = process.hrtime.bigint();

for( let i = 0; i < runs; i++ )
{
	result = solution( input );
}

const endTime = process.hrtime.bigint();

console.log( 'Part 1: \x1b[32m%s\x1b[0m', result[ 0 ] );
console.log( 'Part 2: \x1b[32m%s\x1b[0m', result[ 1 ] );

const time = Number( endTime - startTime ) / 1e6 / runs;

if( runs > 1 )
{
	console.log( `Time  : \x1b[34m${time.toFixed(6)}\x1b[0m ms average for \x1b[34m${runs}\x1b[0m runs` );
}
else
{
	console.log( `Time  : \x1b[34m${time.toFixed(6)}\x1b[0m ms` );
}
