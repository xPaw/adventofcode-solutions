window.AdventOfCode.Day7 = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( l =>
		{
			l = l.split( ' ' );

			return [ l[ 1 ], l[ 7 ] ];
		} );

	let graph = new Map();

	for( const pair	of input )
	{
		if( !graph.has( pair[ 0 ] ) )
		{
			graph.set( pair[ 0 ], new Set() );
		}

		if( !graph.has( pair[ 1 ] ) )
		{
			graph.set( pair[ 1 ], new Set() );
		}

		graph.get( pair[ 0 ] ).add( pair[ 1 ] );
	}

	let part1 = '';
	graph = new Map( [ ...graph.entries() ].sort() );

	while( graph.size > 0 )
	{
		for( const set of graph )
		{
			let depends = false;

			for( const set2 of graph )
			{
				if( set2[ 1 ].has( set[ 0 ] ) )
				{
					depends = true;
					break;
				}
			}
			
			if( !depends )
			{
				part1 += set[ 0 ];
				graph.delete( set[ 0 ] );
				break;
			}
		}
	}

	return [ part1, 0 ];
};
