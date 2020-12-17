window.AdventOfCode.Day3 = function( input )
{
	const part1 = input.split( '\n' );
	const part2 = input.replace(
		/(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)\s+(\d+)/gm,
		'$1 $4 $7\n' +
		'$2 $5 $8\n' +
		'$3 $6 $9'
	).split( '\n' );

	function IsValidTriangle( a, b, c )
	{
		return a + b > c && a + c > b && b + c > a;
	}

	function CountValidTriangles( lines )
	{
		let triangles = 0;

		for( let line of lines )
		{
			line = line.match( /(\d+)\s+(\d+)\s+(\d+)/ );

			if( IsValidTriangle( +line[ 1 ], +line[ 2 ], +line[ 3 ] ) )
			{
				triangles++;
			}
		}

		return triangles;
	}

	return [ CountValidTriangles( part1 ), CountValidTriangles( part2 ) ];
};
