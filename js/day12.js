
window.AdventOfCode.Day12 = function( input )
{
	var SumAllNumbers = function( input )
	{
		return input.match( /-?\d+/g ).reduce( function( a, b )
		{
			return +a + +b;
		} );
	};
	
	var answers =
	[
		SumAllNumbers( input ),
		0, //SumAllNumbers( input.replace( /\{[^\[]+\"red\"[^\[]*]/g, '0' ) )
	];
	
	return answers;
};
