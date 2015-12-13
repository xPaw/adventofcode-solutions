window.AdventOfCode.Day13 = function( input )
{
	var permutator = function(inputArr)
	{
		var results = [];

		function permute(arr, memo)
		{
			var cur, memo = memo || [];

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
	
	// Silly javascript quirks
	var mod = function( n, m )
	{
		return ( ( n % m ) + m ) % m;
	}
	
	var input = input.replace( /\./g, '' ).split( '\n' );
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
		
		var happiness = +data[ 3 ];
		
		if( data[ 2 ] === 'lose' )
		{
			happiness *= -1;
		}
		
		travelPaths[ data[ 0 ] ][ data[ 10 ] ] = happiness;
	}
	
	var cities = Object.keys( cities );
	
	var CalculateHappiness = function()
	{
		var permuted = permutator( cities );
		var bestHappiness = 0;
		
		for( var i = 0; i < permuted.length; i++ )
		{
			var distance = 0;
			
			for( var x = 0; x < cities.length; x++ )
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
	
	var answerOne = CalculateHappiness();
	
	travelPaths[ 'You' ] = {};
	
	for( var i = 0; i < cities.length; i++ )
	{
		travelPaths[ cities[ i ] ][ 'You' ] = 0;
		travelPaths[ 'You' ][ cities[ i ] ] = 0;
	}
	
	cities.push( 'You' );
	
	var answerTwo = CalculateHappiness();
	
	return [ answerOne, answerTwo ];
};
