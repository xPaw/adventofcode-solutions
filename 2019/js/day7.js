module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	let part1 = 0;
	let part2 = 0;
	const phases = permutator( [ 0, 1, 2, 3, 4 ] );

	for( const phase of phases )
	{
		let signal = 0;

		for( let i = 0; i < 5; i++ )
		{
			const ioInput = [ phase[ i ], signal ];
			signal = stateMachine( [ ...input ], ioInput );
		}

		if( part1 < signal )
		{
			part1 = signal;
		}
	}

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
				input[ input[ i + 1 ] ] = ioInput.shift();
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

function permutator(inputArr)
{
	let result = [];

	const permute = (arr, m = []) =>
	{
		if (arr.length === 0)
		{
			result.push(m)
		}
		else
		{
			for (let i = 0; i < arr.length; i++)
			{
				let curr = arr.slice();
				let next = curr.splice(i, 1);
				permute(curr.slice(), m.concat(next))
			}
		}
	}

	permute(inputArr)

	return result;
}
