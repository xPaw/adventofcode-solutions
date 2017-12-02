window.AdventOfCode.Day18 = function( input )
{
	input = input.split( '\n' ).map( function( row )
	{
		row = row.split( '' ).map( function( cell )
		{
			return cell === '#';
		} );
		
		row.unshift( 0 );
		row.push( 0 );
		
		return row;
	} );
	
	var size = input.length;
	
	input.unshift( [] );
	input.push( [] );
	
	function countNeighbors( grid, x, y )
	{
		var count = 0;
		
		for( var y1 = y - 1; y1 < y + 2; y1++ )
		{
			for( var x1 = x - 1; x1 < x + 2; x1++ )
			{
				if( ( x1 != x || y1 != y ) && grid[ x1 ][ y1 ] )
				{
					count += 1;
				}
			}
		}
		
		return count;
	}

	function nextGeneration( grid )
	{
		var newGrid = [];
		
		for( var y = 1; y <= size; y++ )
		{
			newGrid[ y ] = [];
			
			for( var x = 1; x <= size; x++ )
			{
				var neighbors = countNeighbors( grid, y, x );
				
				newGrid[ y ][ x ] = neighbors === 3 || ( neighbors === 2 && grid[ y ][ x ] );
			}
		}
		
		newGrid[ 0 ] = [];
		newGrid[ size + 1 ] = [];
		
		return newGrid;
	}
	
	var inputPartTwo = JSON.parse( JSON.stringify( input ) );
	
	for( var i = 0; i < 100; i++ )
	{
		input = nextGeneration( input );
		inputPartTwo = nextGeneration( inputPartTwo );
		
		inputPartTwo[ 1 ][ 1 ] = 1;
		inputPartTwo[ 1 ][ size ] = 1;
		inputPartTwo[ size ][ 1 ] = 1;
		inputPartTwo[ size ][ size ] = 1;
	}
	
	var partOne = 0;
	var partTwo = 0;
	
	for( i = 1; i <= size; i++ )
	{
		for( var y = 1; y <= size; y++ )
		{
			partOne += input[ i ][ y ];
			partTwo += inputPartTwo[ i ][ y ];
		}
	}
	
	return [ partOne, partTwo ];
};
