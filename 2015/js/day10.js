window.AdventOfCode.Day10 = function( input )
{
	let nextInput = '';
	let fortyLength = 0;

	for( let x = 0; x < 50; x++ )
	{
		let currentDigit = input[ 0 ];
		let currentCount = 0;

		for( let i = 0; i < input.length; i++ )
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
