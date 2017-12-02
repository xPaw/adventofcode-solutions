window.AdventOfCode.Day12 = function( input )
{
	input = JSON.parse( input );
	
	var SumAllNumbers = function( input, skip )
	{
		var result = 0;
		
		if( typeof input === 'object' )
		{
			if( !Array.isArray( input ) )
			{
				var keys = Object.keys( input );
				var dirtyResult = [];
				
				for( var i = 0; i < keys.length; i++ )
				{
					var value = input[ keys[ i ] ];
					
					// Ignore any object (and all of its children) which has any property with the value "red".
					if( value == skip )
					{
						return 0;
					}
					
					dirtyResult.push( value );
				}
				
				input = dirtyResult;
			}
			
			result = input.reduce( function( a, b )
			{
				return a + SumAllNumbers( b, skip );
			}, 0 );
		}
		else if( typeof input === 'number' )
		{
			result = input;
		}
		
		return result;
	};
	
	var answers =
	[
		SumAllNumbers( input, 'kebab' ),
		SumAllNumbers( input, 'red' )
	];
	
	return answers;
};
