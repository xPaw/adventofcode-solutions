window.AdventOfCode.Day12 = function( input )
{
	input = JSON.parse( input );

	const SumAllNumbers = function( input, skip )
	{
		let result = 0;

		if( typeof input === 'object' )
		{
			if( !Array.isArray( input ) )
			{
				const keys = Object.keys( input );
				const dirtyResult = [];

				for( let i = 0; i < keys.length; i++ )
				{
					const value = input[ keys[ i ] ];

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

	const answers =
	[
		SumAllNumbers( input, 'kebab' ),
		SumAllNumbers( input, 'red' )
	];

	return answers;
};
