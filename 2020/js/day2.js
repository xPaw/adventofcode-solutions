module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => x.match( /(?<min>\d+)-(?<max>\d+) (?<letter>[a-z]+): (?<password>.+)/ ).groups );

	let part1 = 0;
	let part2 = 0;

	for( const pass of input )
	{
		const a = parseInt( pass.min, 10 );
		const b = parseInt( pass.max, 10 );

		let matches = 0;

		for( let i = 0; i < pass.password.length; i++ )
		{
			if( pass.password[ i ] === pass.letter )
			{
				matches++;
			}
		}

		if( matches >= a && matches <= b )
		{
			part1++;
		}

		const ap = pass.password[ a - 1 ];
		const bp = pass.password[ b - 1 ];

		if( ap !== bp && ( ap === pass.letter || bp === pass.letter ) )
		{
			part2++;
		}
	}

	return [ part1, part2 ];
};
