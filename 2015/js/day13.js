window.AdventOfCode.Day13 = function( input )
{
	const permutator = function( inputArr )
	{
		const results = [];

		function permute( arr, memo )
		{
			let cur; memo = memo || [];

			for( let i = 0; i < arr.length; i++ )
			{
				cur = arr.splice( i, 1 );
				if( arr.length === 0 )
				{
					results.push( memo.concat( cur ) );
				}
				permute( arr.slice(), memo.concat( cur ) );
				arr.splice( i, 0, cur[ 0 ] );
			}

			return results;
		}

		return permute( inputArr );
	};

	// Silly javascript quirks
	const mod = function( n, m )
	{
		return ( ( n % m ) + m ) % m;
	};

	input = input.replace( /\./g, '' ).split( '\n' );
	const travelPaths = {};
	let cities = {};

	for( let i = 0; i < input.length; i++ )
	{
		const data = input[ i ].split( ' ' );

		if( !cities[ data[ 0 ] ] )
		{
			cities[ data[ 0 ] ] = true;
			travelPaths[ data[ 0 ] ] = {};
		}

		travelPaths[ data[ 0 ] ][ data[ 10 ] ] = data[ 2 ] === 'lose' ? -data[ 3 ] : +data[ 3 ];
	}

	cities = Object.keys( cities );

	const CalculateHappiness = function()
	{
		const permuted = permutator( cities );
		let bestHappiness = 0;

		for( let i = 0; i < permuted.length; i++ )
		{
			let distance = 0;

			for( let x = 0; x < cities.length; x++ )
			{
				distance += travelPaths[ permuted[ i ][ x ] ][ permuted[ i ][ mod( x - 1, cities.length ) ] ];
				distance += travelPaths[ permuted[ i ][ x ] ][ permuted[ i ][ mod( x + 1, cities.length ) ] ];
			}

			if( bestHappiness < distance )
			{
				bestHappiness = distance;
			}
		}

		return bestHappiness;
	};

	const answerOne = CalculateHappiness();

	travelPaths[ 'You' ] = {};

	for( let i = 0; i < cities.length; i++ )
	{
		travelPaths[ cities[ i ] ][ 'You' ] = 0;
		travelPaths[ 'You' ][ cities[ i ] ] = 0;
	}

	cities.push( 'You' );

	const answerTwo = CalculateHappiness();

	return [ answerOne, answerTwo ];
};
