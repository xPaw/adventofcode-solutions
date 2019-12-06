"use strict";

process.chdir( __dirname );

const fs = require( 'fs' );
const day = parseInt( process.argv[ 2 ], 10 );

if( isNaN( day ) )
{
	console.error( 'Usage: node index.js <day> [input]' );
	console.error( 'If input is not specified, reads from data/day%.txt' );
	process.exit( 1 );
}

console.log( `Day ${day}` );

let input = process.argv[ 3 ] || fs.readFileSync( `./data/day${day}.txt` ).toString();
input = input.replace( /\r/g, '' ).trim();

const solution = require( `./js/day${day}.js` );

console.time( 'Time' );

const result = solution( input );

console.timeEnd( 'Time' );

console.log( `Part 1: ${result[ 0 ]}` );
console.log( `Part 2: ${result[ 1 ]}` );
