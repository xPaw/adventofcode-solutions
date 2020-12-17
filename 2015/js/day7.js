window.AdventOfCode.Day7 = function( input )
{
	// Split all operations by space
	input = input.split( '\n' ).map( function( a )
	{
		return a.split( ' ' );
	} );

	// Pre-sort the input to avoid multiple iterations
	input.sort( function( a, b )
	{
		// Get the output wire
		a = a[ a.length - 1 ];
		b = b[ b.length - 1 ];

		// Wire 'a' must end at the end of instructions
		if( a === 'a' )
		{
			return 1;
		}
		else if( b === 'a' )
		{
			return -1;
		}

		if( a.length === b.length )
		{
			return a > b ? 1 : -1;
		}

		return a.length - b.length;
	} );

	const Solve = function( wires )
	{
		const GetValue = function( value )
		{
			if( value in wires )
			{
				return wires[ value ];
			}

			return +value;
		};

		const SetWire = function( wire, value )
		{
			// Can't assign same wire twice,
			// this becomes a problem when overriding b
			if( wire in wires )
			{
				return;
			}

			wires[ wire ] = value & 0xFFFF;
		};

		for( let i = 0; i < input.length; i++ )
		{
			const operation = input[ i ];

			switch( operation.length )
			{
				// 123 -> x means that the signal 123 is provided to wire x.
				case 3: // ["123", "->", "x"]
					SetWire( operation[ 2 ], GetValue( operation[ 0 ] ) );

					break;

				// NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
				case 4: // ["NOT", "x", "->", "h"]
					SetWire( operation[ 3 ], ~GetValue( operation[ 1 ] ) );

					break;

				// x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
				case 5: // ["x", "AND", "y", "->", "d"]
					switch( operation[ 1 ] )
					{
						case 'AND'   : SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) &  GetValue( operation[ 2 ] ) ); break;
						case 'OR'    : SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) |  GetValue( operation[ 2 ] ) ); break;
						case 'LSHIFT': SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) << GetValue( operation[ 2 ] ) ); break;
						case 'RSHIFT': SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) >> GetValue( operation[ 2 ] ) ); break;
					}

					break;
			}
		}

		return wires[ 'a' ] || 0;
	};

	const solution1 = Solve( {} );
	const solution2 = Solve( { b: solution1 } );

	return [ solution1, solution2 ];
};
