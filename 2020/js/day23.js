module.exports = ( input ) =>
{
	input = input.split( '' ).map( x => parseInt( x, 10 ) );

	function solve( max, iterations )
	{
		const cups = new Int32Array( max + 1 );

		for( let i = 0; i < input.length - 1; i++ )
		{
			cups[ input[ i ] ] = input[ i + 1 ];
		}

		let prev = input[ input.length - 1 ];

		if( max > 10 ) // this is dumb
		{
			for( let i = input.length + 1; i <= max; i++ )
			{
				cups[ prev ] = i;
				prev = i;
			}

			cups[ max ] = input[ 0 ];
		}
		else
		{
			cups[ prev ] = input[ 0 ];
		}

		let currentCup = input[ 0 ];

		for( let i = 0; i < iterations; i++ )
		{
			let nextCup = currentCup;
			const cup1 = cups[ currentCup ];
			const cup2 = cups[ cup1 ];
			const cup3 = cups[ cup2 ];

			do
			{
				if( --nextCup === 0 )
				{
					nextCup = max === 10 ? ( max - 1 ) : max;
				}
			}
			while( cup1 === nextCup || cup2 === nextCup || cup3 === nextCup );

			cups[ currentCup ] = cups[ cup3 ];
			cups[ cup3 ] = cups[ nextCup ];
			cups[ nextCup ] = cup1;
			currentCup = cups[ currentCup ];
		}

		return cups;
	}

	let cups = solve( 10, 100 );
	let part1 = 0;
	let current = cups[ 1 ];

	for( let i = 0; i < input.length - 1; ++i )
	{
		part1 += Math.pow( 10, input.length - i - 2 ) * current;
		current = cups[ current ];
	}

	cups = solve( 1000000, 10000000 );
	const part2 = cups[ 1 ] * cups[ cups[ 1 ] ];

	return [ part1, part2 ];
};
