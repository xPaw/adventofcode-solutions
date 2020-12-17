/* globals bigInt */
window.AdventOfCode.Day25 = function( input )
{
	input = input.match( /([0-9]+)/g );

	const row = +input[ 0 ];
	const column = +input[ 1 ];

	const firstCode  = 20151125;
	const multiplier = 252533;
	const divider    = 33554393;

	const exp = ( row + column - 2 ) * ( row + column - 1 ) / 2 + column - 1;
	const answer = ( bigInt( multiplier ).modPow( exp, divider ) * firstCode ) % divider;

	return [ answer, 'Merry Christmas!' ];
};
