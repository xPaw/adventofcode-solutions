window.AdventOfCode.Day6 = function( input )
{
	input = input.split( '\n' );
	
	// Because your neighbors keep defeating you in the holiday house
	// decorating contest year after year, you've decided
	// to deploy one million lights in a 1000x1000 grid.
	var grid = [];
	
	// var row = Array( 1000 ).fill( 0 );
	// var grid = Array( 1000 ).fill( row );
	// This would be a nice solution, right?
	// But Array.fill just uses the same array reference, instead of cloning
	
	var i, y;
	
	for( i = 0; i < 1000; i++ )
	{
		grid[ i ] = [];
		
		for( y = 0; y < 1000; y++ )
		{
			grid[ i ][ y ] =
			{
				status: 0,
				brightness: 0
			};
		}
	}
	
	var regex = /^(turn on|turn off|toggle) (\d+),(\d+) through (\d+),(\d+)$/;
	
	for( i = 0; i < input.length; i++ )
	{
		var operation = input[ i ].match( regex );
		
		// Using + to convert it to a number
		var x1 = +operation[ 2 ];
		var y1 = +operation[ 3 ];
		var x2 = +operation[ 4 ];
		var y2 = +operation[ 5 ];
		operation = operation[ 1 ];
		
		for( var i1 = x1; i1 <= x2; i1++ )
		{
			for( var i2 = y1; i2 <= y2; i2++ )
			{
				// The instructions include whether to turn on, turn off,
				// or toggle various inclusive ranges given as coordinate pairs.
				switch( operation )
				{
					case 'turn on':
					{
						grid[ i1 ][ i2 ].status = 1;
						grid[ i1 ][ i2 ].brightness += 1;
						
						break;
					}
					case 'turn off':
					{
						grid[ i1 ][ i2 ].status = 0;
						
						if( grid[ i1 ][ i2 ].brightness > 0 )
						{
							grid[ i1 ][ i2 ].brightness -= 1;
						}
						
						break;
					}
					case 'toggle':
					{
						grid[ i1 ][ i2 ].status ^= 1;
						grid[ i1 ][ i2 ].brightness += 2;
						
						break;
					}
				}
			}
		}
	}
	
	var turnedOnLights = 0;
	var totalBrightness = 0;
	
	for( i = 0; i < grid.length; i++ )
	{
		// Using .reduce is nice, but not worth the double iteration
		for( y = 0; y < grid[ i ].length; y++ )
		{
			turnedOnLights += grid[ i ][ y ].status;
			totalBrightness += grid[ i ][ y ].brightness;
		}
	}
	
	return [ turnedOnLights, totalBrightness ];
};
