window.AdventOfCode.Day15 = function( input )
{
	input = input.split( '\n' ).map( function( a )
	{
		const b = a.match( /.+: .+ (-?\d+), .+ (-?\d+), .+ (-?\d+), .+ (-?\d+), .+ (-?\d+)/ );

		return [ +b[ 1 ], +b[ 2 ], +b[ 3 ], +b[ 4 ], +b[ 5 ] ];
	} );

	let totalScore = 0;
	let totalScore500 = 0;

	for( let a = 0; a <= 100; a++ )
		for( let b = 0; b <= 100 - a; b++ )
			for( let c = 0; c <= 100 - a - b; c++ )
			{
				const d = 100 - a - b - c;

				const cap = input[ 0 ][ 0 ] * a + input[ 1 ][ 0 ] * b + input[ 2 ][ 0 ] * c + input[ 3 ][ 0 ] * d;
				const dur = input[ 0 ][ 1 ] * a + input[ 1 ][ 1 ] * b + input[ 2 ][ 1 ] * c + input[ 3 ][ 1 ] * d;
				const fla = input[ 0 ][ 2 ] * a + input[ 1 ][ 2 ] * b + input[ 2 ][ 2 ] * c + input[ 3 ][ 2 ] * d;
				const tex = input[ 0 ][ 3 ] * a + input[ 1 ][ 3 ] * b + input[ 2 ][ 3 ] * c + input[ 3 ][ 3 ] * d;
				const cal = input[ 0 ][ 4 ] * a + input[ 1 ][ 4 ] * b + input[ 2 ][ 4 ] * c + input[ 3 ][ 4 ] * d;

				if( cap <= 0 || dur <= 0 || fla <= 0 || tex <= 0 )
				{
					continue;
				}

				const score = cap * dur * fla * tex;

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
