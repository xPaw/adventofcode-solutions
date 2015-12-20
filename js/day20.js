window.AdventOfCode.Day20 = function( input )
{
	input = +input;
	
	var i, j;
	var n = input / ( input / 1E6 );
	var houses = {};
	var housesPartTwo = {};
	
	for( i = 1; i < n; i++ )
	{
		for( j = i; j < n; j += i )
		{
			houses[ j ] = ( houses[ j ] || 0 ) + i * 10;
			
			if( houses[ j ] >= input )
			{
				break;
			}
		}
		
		var lazyElfs = 0;
		for( j = i; lazyElfs < 50; j += i )
		{
			housesPartTwo[ j ] = ( housesPartTwo[ j ] || 0 ) + i * 11;
			
			if( housesPartTwo[ j ] >= input )
			{
				break;
			}
			
			lazyElfs++;
		}
	}
	
	for( i = 1; i < input; i++ )
	{
		if( houses[ i ] >= input )
		{
			break;
		}
	}
	
	for( j = 1; j < input; j++ )
	{
		if( housesPartTwo[ j ] >= input )
		{
			break;
		}
	}
	
	return [ i, j ];
};
