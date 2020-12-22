module.exports = ( input ) =>
{
	input = input
		.replace( /Player [12]:\n/g, '' )
		.split( '\n\n' )
		.map( l => l.split( '\n' ).map( x => parseInt( x, 10 ) ) );

	function score( deck )
	{
		return deck.reduce( ( a, b, i ) => a + b * ( deck.length - i ), 0 );
	}

	function solvePart1()
	{
		const d1 = [ ...input[ 0 ] ];
		const d2 = [ ...input[ 1 ] ];

		do
		{
			const c1 = d1.shift();
			const c2 = d2.shift();

			if( c1 > c2 )
			{
				d1.push( c1 );
				d1.push( c2 );
			}
			else
			{
				d2.push( c2 );
				d2.push( c1 );
			}
		}
		while( d1.length > 0 && d2.length > 0 );

		return score( d1.length > d2.length ? d1 : d2 );
	}

	function solvePart2()
	{
		const p1Winner = recursiveCombat( input[ 0 ], input[ 1 ] );
		return score( input[ p1Winner ? 0 : 1 ] );
	}

	function recursiveCombat( d1, d2 )
	{
		const previousRounds = new Set();

		do
		{
			const hash = score( d1 ) + ( 10000 * score( d2 ) );

			if( previousRounds.has( hash ) )
			{
				return true;
			}

			previousRounds.add( hash );

			const c1 = d1.shift();
			const c2 = d2.shift();
			let p1Winner = c1 > c2;

			if( c1 <= d1.length && c2 <= d2.length )
			{
				p1Winner = recursiveCombat( d1.slice( 0, c1 ), d2.slice( 0, c2 ) );
			}

			if( p1Winner )
			{
				d1.push( c1 );
				d1.push( c2 );
			}
			else
			{
				d2.push( c2 );
				d2.push( c1 );
			}
		}
		while( d1.length > 0 && d2.length > 0 );

		return d1.length > d2.length;
	}

	const part1 = solvePart1();
	//const part1 = 0;
	const part2 = solvePart2();

	return [ part1, part2 ];
};
