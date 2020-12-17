module.exports = ( input ) =>
{
	input = input.split( '-' );
	let start = +input[ 0 ];
	const end = +input[ 1 ];
	let part1 = 0;
	let part2 = 0;

	do
	{
		const pass = start.toString();
		part1 += checkPassword( pass );
		part2 += checkPassword2( pass );
	}
	while( ++start < end );

	return [ part1, part2 ];
};

function checkPassword( pass )
{
	let hasDouble = 0;

	for( let i = 1; i < 6; i++ )
	{
		const a = pass[ i - 1 ];
		const b = pass[ i ];

		if( a > b )
		{
			return 0;
		}

		hasDouble |= a === b;
	}

	return hasDouble;
}

function checkPassword2( pass )
{
	let hasDouble = 0;
	let prev = -1;

	for( let i = 1; i < 6; i++ )
	{
		const a = pass[ i - 1 ];
		const b = pass[ i ];

		if( a > b )
		{
			return 0;
		}

		hasDouble |= a === b && b !== pass[ i + 1 ] && prev !== a;
		prev = a;
	}

	return hasDouble;
}
