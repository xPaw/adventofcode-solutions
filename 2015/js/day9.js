window.AdventOfCode.Day9 = function( input )
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

	input = input.split( '\n' );
	const travelPaths = {};
	const cities = {};

	for( let i = 0; i < input.length; i++ )
	{
		const data = input[ i ].split( ' ' );

		if( !cities[ data[ 0 ] ] )
		{
			cities[ data[ 0 ] ] = true;
			travelPaths[ data[ 0 ] ] = {};
		}

		if( !cities[ data[ 2 ] ] )
		{
			cities[ data[ 2 ] ] = true;
			travelPaths[ data[ 2 ] ] = {};
		}

		travelPaths[ data[ 0 ] ][ data[ 2 ] ] = +data[ 4 ];
		travelPaths[ data[ 2 ] ][ data[ 0 ] ] = +data[ 4 ];
	}

	const permuted = permutator( Object.keys( cities ) );
	let shortestDistance = Number.MAX_VALUE;
	let longestDistance = 0;

	for( let i = 0; i < permuted.length; i++ )
	{
		let distance = 0;

		for( let x = 1; x < permuted[ i ].length; x++ )
		{
			distance += travelPaths[ permuted[ i ][ x - 1 ] ][ permuted[ i ][ x ] ];
		}

		if( shortestDistance > distance )
		{
			shortestDistance = distance;
		}

		if( longestDistance < distance )
		{
			longestDistance = distance;
		}
	}

	return [ shortestDistance, longestDistance ];
};
