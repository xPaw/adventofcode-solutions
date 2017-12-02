window.AdventOfCode.Day2 = function( input )
{
	input = input.split( '\n' ).map( ( line ) => line.split( /\s/ ) );
	
	const part1 = input.reduce( ( rowAccumulator, row ) =>
	{
		row.sort( ( a, b ) => a - b );
		
		return rowAccumulator + +row[ row.length - 1 ] - +row[ 0 ];
	}, 0 );
	
	const part2 = input.reduce( ( rowAccumulator, row ) =>
	{
		let division = 0;
		
		for( let i = 0; i < row.length; i++ )
		{
			for( let x = 0; x < row.length; x++ )
			{
				if( i !== x && row[ i ] % row[ x ] === 0 )
				{
					console.log(row[ i ], row[ x ], division);
				}
			}
		}
		
		return rowAccumulator + division;
	}, 0 );
	
	return [ part1, part2 ];
};
