window.AdventOfCode.Day1 = function( input )
{
	input = input.split( '' );

	let sum = 0;
	let sumHalf = 0;
	const half = input.length / 2;

	for( let i = 0; i < input.length; i++ )
	{
		const current = input[ i ];
		const next = input[ ( i + 1 ) % input.length ];
		const nextHalf = input[ ( i + half ) % input.length ];

		if( current === next )
		{
			sum += +current;
		}

		if( current === nextHalf )
		{
			sumHalf += +current;
		}
	}

	return [ sum, sumHalf ];
};
