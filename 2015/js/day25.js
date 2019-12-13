/* globals bigInt */
window.AdventOfCode.Day25 = function( input )
{
	input = input.match( /([0-9]+)/g );
	
	var row = +input[ 0 ];
	var column = +input[ 1 ];
	
	var firstCode  = 20151125;
	var multiplier = 252533;
	var divider    = 33554393;
	
	var exp = ( row + column - 2 ) * ( row + column - 1 ) / 2 + column - 1;
	var answer = ( bigInt( multiplier ).modPow( exp, divider ) * firstCode ) % divider;
	
	return [ answer, 'Merry Christmas!' ];
};
