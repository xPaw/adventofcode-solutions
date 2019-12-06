module.exports = ( input ) =>
{
	const objects = new Map();
	objects.set( 'COM', null );

	input
		.split( '\n' )
		.map( x => x.split( ')' ) )
		.forEach( ( [ a, b ] ) => objects.set( b, a ) );

	const orbits = ( obj ) => obj ? 1 + orbits( objects.get( obj ) ) : 0;
	const part1 = Array.from( objects.keys() )
		.reduce( ( total, obj ) => total + orbits( objects.get( obj ) ), 0 );

	const chain = ( obj, links ) => obj ? chain( objects.get( obj ), [ ...links, obj ] ) : links;
	const transfer = ( obj, path, depth ) =>
	{
		const index = path.indexOf( obj );

		if( index > -1 )
		{
			return depth + index;
		}

		return transfer( objects.get( obj ), path, depth + 1 );
	};
	const part2 = transfer( objects.get( 'SAN' ), chain( objects.get( 'YOU' ), [] ), 0 );

	return [ part1, part2 ];
};
