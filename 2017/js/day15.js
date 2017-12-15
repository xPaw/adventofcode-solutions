window.AdventOfCode.Day15 = function( input )
{
	input = input.match( /(\d+)/g );
	
	const A = +input[ 0 ];
	const B = +input[ 1 ];
	
	const factorA = 16807;
	const factorB = 48271;
	const product = 2147483647;
	
	const generate = ( value, factor, multiplier ) =>
	{
		const newValue = value * factor % product;
		
		if( newValue % multiplier )
		{
			return generate( newValue, factor, multiplier );
		}
		
		return newValue;
	};
	
	const count = ( a, b, iterations, multiplierA, multiplierB ) =>
	{
		let count = 0;
		
		while( iterations-- > 0 )
		{
			a = generate( a, factorA, multiplierA );
			b = generate( b, factorB, multiplierB );
			count += ( a & 0xFFFF ) === ( b & 0xFFFF );
		}
		
		return count;
	};
	
	const part1 = count( A, B, 40000000, 1, 1 );
	const part2 = count( A, B, 5000000, 4, 8 );
	
	return [ part1, part2 ];
};
