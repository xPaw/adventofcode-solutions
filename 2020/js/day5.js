module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( pass => pass.replace( /./g, letter =>
		{
			return letter === 'B' || letter === 'R' ? 1 : 0;
		} ) )
		.map( pass => parseInt( pass, 2 ) )
		.sort( ( a, b ) => b - a );

	const part1 = input[ 0 ];
	let part2 = 0;
	let lastPass = part1;

	for( const pass of input )
	{
		if( lastPass - pass > 1 )
		{
			part2 = pass + 1;
			break;
		}

		lastPass = pass;
	}

	return [ part1, part2 ];
};
