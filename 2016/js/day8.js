window.AdventOfCode.Day8 = function( input )
{
	input = input.split( '\n' );
	
	let grid = [];
	
	for( let x = 0; x < 6; x++ )
	{
		grid[ x ] = [];
		
		for( let y = 0; y < 50; y++ )
		{
			grid[ x ][ y ] = false;
		}
	}
	
	for( let line of input )
	{
		const rect = line.match( /^rect (\d+)x(\d+)$/ );
		
		if( rect )
		{
			const min = +rect[ 1 ];
			const max = +rect[ 2 ];
			
			for( let x = 0; x < max; x++ )
			{
				for( let y = 0; y < min; y++ )
				{
					grid[ x ][ y ] = true;
				}
			}
		}
		else
		{
			const rotate = line.match( /^rotate (row y|column x)=(\d+) by (\d+)$/ );
			
			const what = +rotate[ 2 ];
			const much = +rotate[ 3 ];
			
			// JAVASCRIPT PLES
			const oldGrid = [];
			for( let x = 0; x < grid.length; x++ )
			{
				oldGrid[ x ] = grid[ x ].slice();
			}
			
			if( rotate[ 1 ] === 'row y' )
			{
				for( let y = 0; y < 50; y++ )
				{
					grid[ what ][ ( y + much ) % 50 ] = oldGrid[ what ][ y ];
				}
			}
			else
			{
				for( let x = 0; x < 6; x++ )
				{
					grid[ ( x + much ) % 6 ][ what ] = oldGrid[ x ][ what ];
				}
			}
		}
	}
	
	let part1 = 0;
	
	for( let x = 0; x < grid.length; x++ )
	{
		let line = '';
		
		for( let y = 0; y < grid[ x ].length; y++ )
		{
			line += grid[ x ][ y ] ? '#' : ' ';
			
			if( grid[ x ][ y ] )
			{
				part1++;
			}
		}
		
		console.log( line );
	}
	
	return [ part1, 'See console' ];
};
