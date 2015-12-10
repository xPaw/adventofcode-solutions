window.AdventOfCode.Day10 = function( input )
{
	var nextInput = '';
	var fortyLength = 0;
	
	for( var x = 0; x < 50; x++ )
	{
		var currentDigit = input[ 0 ];
		var currentCount = 0;
		
		for( var i = 0; i < input.length; i++ )
		{
			if( currentDigit != input[ i ] )
			{
				nextInput += currentCount + '' + currentDigit;
				currentDigit = input[ i ];
				currentCount = 1;
			}
			else
			{
				currentCount++;
			}
		}
		
		nextInput += currentCount + '' + currentDigit;
		input = nextInput;
		nextInput = '';
		
		if( x === 39 )
		{
			fortyLength = input.length;
		}
	}
	
	return [ fortyLength, input.length ];
};
