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

	const size = input.length;

	input.unshift( [] );
	input.push( [] );

	function countNeighbors( grid, x, y )
	{
		let count = 0;

		for( let y1 = y - 1; y1 < y + 2; y1++ )
		{
			for( let x1 = x - 1; x1 < x + 2; x1++ )
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
		const newGrid = [];

		for( let y = 1; y <= size; y++ )
		{
			newGrid[ y ] = [];

			for( let x = 1; x <= size; x++ )
			{
				const neighbors = countNeighbors( grid, y, x );

				newGrid[ y ][ x ] = neighbors === 3 || ( neighbors === 2 && grid[ y ][ x ] );
			}
		}

		newGrid[ 0 ] = [];
		newGrid[ size + 1 ] = [];

		return newGrid;
	}

	let inputPartTwo = JSON.parse( JSON.stringify( input ) );

	for( let i = 0; i < 100; i++ )
	{
		input = nextGeneration( input );
		inputPartTwo = nextGeneration( inputPartTwo );

		inputPartTwo[ 1 ][ 1 ] = 1;
		inputPartTwo[ 1 ][ size ] = 1;
		inputPartTwo[ size ][ 1 ] = 1;
		inputPartTwo[ size ][ size ] = 1;
	}

	let partOne = 0;
	let partTwo = 0;

	for( let i = 1; i <= size; i++ )
	{
		for( let y = 1; y <= size; y++ )
		{
			partOne += input[ i ][ y ];
			partTwo += inputPartTwo[ i ][ y ];
		}
	}

	return [ partOne, partTwo ];
};
