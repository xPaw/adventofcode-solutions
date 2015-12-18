window.AdventOfCode.Day18 = function( input )
{
	input = input.split( '\n' ).map( function( row )
	{
		return row.split( '' ).map( function( cell )
		{
			return cell === '#' ? 1 : 0;
		} );
	} );
	
	var width = input.length;
	var height = width;
	
	// Stolen from somewhere on stackoverflow
	function countNeighbors(grid, x, y)
	{
		var count = 0;
		
		for (var y1 = Math.max(0, y - 1); y1 <= Math.min(height, y + 1); y1++)
		{
			for (var x1 = Math.max(0, x - 1); x1 <= Math.min(width, x + 1); x1++)
			{
				try // lol!
				{
					if ((x1 != x || y1 != y) && grid[x1][y1])
						count += 1;
				}
				catch (e)
				{}
			}
		}
		
		return count;
	}

	function nextGeneration( grid )
	{
		var newGrid = [];
		
		for( var y = 0; y < height; y++ )
		{
			newGrid[y] = [];
			
			for( var x = 0; x < width; x++ )
			{
				var neighbors = countNeighbors( grid, y, x );

				if( grid[y][x] )
				{
					newGrid[y][x] = neighbors === 2 || neighbors === 3 ? 1 : 0;
				}
				else
				{
					newGrid[y][x] = neighbors === 3 ? 1 : 0;
				}
			}
		}
		
		return newGrid;
	}
	
	for( var i = 0; i < 5; i++ )
	{
		input = nextGeneration( input, [] );
	}
	
	var partOne = 0;
	var partTwo = 0;
	
	for( i = 0; i < input.length; i++ )
	{
		for( y = 0; y < input[ i ].length; y++ )
		{
			partOne += input[ i ][ y ];
		}
	}
	
	return [ partOne, partTwo ];
};
