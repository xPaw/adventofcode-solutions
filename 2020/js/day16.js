module.exports = ( input ) =>
{
	input = input.split( '\n\n' ).map( l => l.split( '\n' ) );
	input[ 2 ].shift();

	const myTicket = input[ 1 ][ 1 ]
		.split( ',' )
		.map( x => parseInt( x, 10 ) );
	const otherTickets = input[ 2 ]
		.map( l => l
			.split( ',' )
			.map( x => parseInt( x, 10 ) )
		);
	const fields = input[ 0 ]
		.map( l => {
			const m = l.match( /(?<name>[a-z ]+): (?<min1>\d+)-(?<max1>\d+) or (?<min2>\d+)-(?<max2>\d+)/ ).groups;
			m.min1 = parseInt( m.min1, 10 );
			m.min2 = parseInt( m.min2, 10 );
			m.max1 = parseInt( m.max1, 10 );
			m.max2 = parseInt( m.max2, 10 );
			m.indices = Array( myTicket.length ).fill( 0, 0, myTicket.length );
			return m;
		} );
	let part1 = 0;
	let part2 = 1;
	let validTickets = 0;

	function isValidField( t, f )
	{
		return ( t >= f.min1 && t <= f.max1 ) || ( t >= f.min2 && t <= f.max2 );
	}

	for( const ticket of otherTickets )
	{
		const invalid = ticket.filter( t => !fields.some( f => isValidField( t, f ) ) );

		if( invalid.length > 0 )
		{
			invalid.forEach( f => part1 += f );
			continue;
		}

		validTickets++;

		for( let i = 0; i < ticket.length; i++ )
		{
			for( const f of fields )
			{
				if( isValidField( ticket[ i ], f ) )
				{
					f.indices[ i ]++;
				}
			}
		}
	}

	const fieldsItCanBe = [];

	for( let i = 0; i < myTicket.length; i++ )
	{
		fieldsItCanBe[ i ] =
		{
			eliminated: false,
			names: fields.filter( f => f.indices[ i ] === validTickets ).map( f => f.name )
		};
	}

	do
	{
		const field = fieldsItCanBe.find( f => !f.eliminated && f.names.length === 1 );

		if( typeof field === 'undefined' )
		{
			break;
		}

		field.eliminated = true;

		for( const otherField of fieldsItCanBe )
		{
			if( !otherField.eliminated )
			{
				otherField.names = otherField.names.filter( f => f !== field.names[ 0 ] );
			}
		}
	}
	while( true ); // eslint-disable-line

	for( let i = 0; i < fieldsItCanBe.length; i++ )
	{
		if( fieldsItCanBe[ i ].names[ 0 ].startsWith( 'departure' ) )
		{
			part2 *= myTicket[ i ];
		}
	}

	return [ part1, part2 ];
};
