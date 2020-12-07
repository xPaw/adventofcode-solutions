module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => x.match( /^(?<color>[a-z]+ [a-z]+) bags contain (?<contains>.+)\./ ) )
		.map( x =>
		{
			const colors = new Map();

			if( x.groups.contains !== 'no other bags' )
			{
				const contains = x.groups.contains.matchAll( /(?<count>[0-9]+) (?<color>[a-z]+ [a-z]+) bag/g );

				for( const color of contains )
				{
					colors.set( color.groups.color, parseInt( color.groups.count, 10 ) );
				}
			}

			return [ x.groups.color, colors ];
		} );

	input = new Map( input );

	function containsShinyGoldBag( [ color, contains ] )
	{
		if( typeof contains === 'number' )
		{
			return containsShinyGoldBag( [ color, input.get( color ) ] );
		}

		if( contains.has( 'shiny gold' ) )
		{
			return true;
		}

		return [ ...contains ].some( containsShinyGoldBag );
	}

	function countBags( [ color, contains ] )
	{
		if( typeof contains === 'number' )
		{
			return contains + countBags( [ color, input.get( color ) ] ) * contains;
		}

		return [ ...contains ].reduce( ( a, b ) => a + countBags( b ), 0 );
	}

	const part1 = [ ...input ].reduce( ( a, b ) => a + containsShinyGoldBag( b ), 0 );
	const part2 = countBags( [ null, input.get( 'shiny gold' ) ] );

	return [ part1, part2 ];
};
