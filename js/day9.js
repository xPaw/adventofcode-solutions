window.AdventOfCode.Day9 = function( input )
{
	var permutator = function(inputArr)
	{
		var results = [];

		function permute(arr, memo)
		{
			var cur; memo = memo || [];

			for (var i = 0; i < arr.length; i++)
			{
				cur = arr.splice(i, 1);
				if (arr.length === 0)
				{
					results.push(memo.concat(cur));
				}
				permute(arr.slice(), memo.concat(cur));
				arr.splice(i, 0, cur[0]);
			}

			return results;
		}

		return permute(inputArr);
	};
	
	input = input.split( '\n' );
	var travelPaths = {};
	var cities = {};

	for( var i = 0; i < input.length; i++ )
	{
		var data = input[ i ].split( ' ' );
		
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
	
	var permuted = permutator( Object.keys( cities ) );
	var shortestDistance = Number.MAX_VALUE;
	var longestDistance = 0;
	
	for( i = 0; i < permuted.length; i++ )
	{
		var distance = 0;
		
		for( var x = 1; x < permuted[ i ].length; x++ )
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
