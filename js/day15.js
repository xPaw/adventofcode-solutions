window.AdventOfCode.Day15 = function( input )
{
	input = input.split( '\n' ).map( function( a )
	{
		var b = a.match( /.+: .+ (-?\d+), .+ (-?\d+), .+ (-?\d+), .+ (-?\d+), .+ (-?\d+)/ );
		
		return [ +b[ 1 ], +b[ 2 ], +b[ 3 ], +b[ 4 ], +b[ 5 ] ];
	} );
	
	var totalScore = 0;
	var totalScore500 = 0;
	
	for( var a = 0; a <= 100; a++ )
	for( var b = 0; b <= 100 - a; b++ )
	for( var c = 0; c <= 100 - a - b; c++ )
	{
		var d = 100 - a - b - c;
		
		var cap = input[ 0 ][ 0 ] * a + input[ 1 ][ 0 ] * b + input[ 2 ][ 0 ] * c + input[ 3 ][ 0 ] * d;
		var dur = input[ 0 ][ 1 ] * a + input[ 1 ][ 1 ] * b + input[ 2 ][ 1 ] * c + input[ 3 ][ 1 ] * d;
		var fla = input[ 0 ][ 2 ] * a + input[ 1 ][ 2 ] * b + input[ 2 ][ 2 ] * c + input[ 3 ][ 2 ] * d;
		var tex = input[ 0 ][ 3 ] * a + input[ 1 ][ 3 ] * b + input[ 2 ][ 3 ] * c + input[ 3 ][ 3 ] * d;
		var cal = input[ 0 ][ 4 ] * a + input[ 1 ][ 4 ] * b + input[ 2 ][ 4 ] * c + input[ 3 ][ 4 ] * d;
		
		if( cap <= 0 || dur <= 0 || fla <= 0 || tex <= 0 )
		{
			continue;
		}
		
		var score = cap * dur * fla * tex;
		
		if( totalScore < score )
		{
			totalScore = score;
		}
		
		if( totalScore500 < score && cal === 500 )
		{
			totalScore500 = score;
		}
	}
	
	return [ totalScore, totalScore500 ];
};
