module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	const part1 = stateMachine( [ ...input ], 1 );
	const part2 = stateMachine( [ ...input ], 5 );

	return [ part1, part2 ];
};

function stateMachine( input, ioInput )
{
	let lastOutput;

halt:
	for( let i = 0; i < input.length; i++ )
	{
		const instruction = input[ i ].toString().padStart( 5, '0' );
		const opcode = +instruction[ 3 ] * 10 + +instruction[ 4 ];
		const getValue = ( n ) => input[ instruction[ 3 - n ] === '1' ? ( i + n ) : input[ i + n ] ];

		switch( opcode )
		{
			case 1: // add
			{
				input[ input[ i + 3 ] ] = getValue( 1 ) + getValue( 2 );
				i += 3;

				break;
			}
			case 2: // multiply
			{
				input[ input[ i + 3 ] ] = getValue( 1 ) * getValue( 2 );
				i += 3;

				break;
			}
			case 3: // input
			{
				input[ input[ i + 1 ] ] = ioInput;
				i += 1;

				break;
			}
			case 4: // output
			{
				lastOutput = getValue( 1 );
				i += 1;

				break;
			}
			case 5: // jump-if-true
			{
				if( getValue( 1 ) !== 0 )
				{
					i = getValue( 2 ) - 1;
				}
				else
				{
					i += 2;
				}

				break;
			}
			case 6: // jump-if-false
			{
				if( getValue( 1 ) === 0 )
				{
					i = getValue( 2 ) - 1;
				}
				else
				{
					i += 2;
				}

				break;
			}
			case 7: // less than
			{
				input[ input[ i + 3 ] ] = getValue( 1 ) < getValue( 2 ) ? 1 : 0;
				i += 3;

				break;
			}
			case 8: // equals
			{
				input[ input[ i + 3 ] ] = getValue( 1 ) === getValue( 2 ) ? 1 : 0;
				i += 3;

				break;
			}
			case 99:
			{
				break halt;
			}
		}
	}

	return lastOutput;
}
