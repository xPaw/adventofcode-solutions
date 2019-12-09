module.exports = ( input, ioInput ) =>
{
	const powers = [ 0, 100, 1000, 10000 ]; // Math.pow is slow
	let output = [];
	let base = 0;
	let i = 0;
	let instruction;

	const getAddress = ( n ) =>
	{
		const mode = ~~( instruction / powers[ n ] ) % 10;
		let offset = i + n;

		switch( mode )
		{
			case 0: offset = input[ offset ]; break;
			//case 1: offset = offset; break;
			case 2: offset = base + input[ offset ]; break;
		}

		return offset;
	};
	const getValue = ( n ) => input[ getAddress( n ) ] || 0;

halt:
	while( true )
	{
		instruction = input[ i ];
		const opcode = instruction % 100;

		switch( opcode )
		{
			case 1: // add
			{
				input[ getAddress( 3 ) ] = getValue( 1 ) + getValue( 2 );
				i += 4;

				break;
			}
			case 2: // multiply
			{
				input[ getAddress( 3 ) ] = getValue( 1 ) * getValue( 2 );
				i += 4;

				break;
			}
			case 3: // input
			{
				input[ getAddress( 1 ) ] = ioInput.shift();
				i += 2;

				break;
			}
			case 4: // output
			{
				output.push( getValue( 1 ) );
				i += 2;

				break;
			}
			case 5: // jump-if-true
			{
				if( getValue( 1 ) !== 0 )
				{
					i = getValue( 2 );
				}
				else
				{
					i += 3;
				}

				break;
			}
			case 6: // jump-if-false
			{
				if( getValue( 1 ) === 0 )
				{
					i = getValue( 2 );
				}
				else
				{
					i += 3;
				}

				break;
			}
			case 7: // less than
			{
				input[ getAddress( 3 ) ] = getValue( 1 ) < getValue( 2 ) ? 1 : 0;
				i += 4;

				break;
			}
			case 8: // equals
			{
				input[ getAddress( 3 ) ] = getValue( 1 ) === getValue( 2 ) ? 1 : 0;
				i += 4;

				break;
			}
			case 9: // modify relative base
			{
				base += getValue( 1 );
				i += 2;

				break;
			}
			case 99:
			{
				break halt;
			}
		}
	}

	return output;
};
