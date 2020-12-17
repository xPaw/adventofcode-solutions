window.AdventOfCode.Day23 = function( input )
{
	const parser = /([a-z]+) ([+-]?[a-z0-9]+)(?:, ([+-]?[0-9]+))?/;

	input = input.split( '\n' ).map( function( a )
	{
		return a.match( parser );
	} );

	const Execute = function( startingRegister )
	{
		const registers = { a: startingRegister, b: 0 };
		let offset = 0;

		while( input.length > offset )
		{
			const instruction = input[ offset ];

			switch( instruction[ 1 ] )
			{
				// hlf r sets register r to half its current value, then continues with the next instruction.
				case 'hlf':
					registers[ instruction[ 2 ] ] /= 2;

					break;

				// tpl r sets register r to triple its current value, then continues with the next instruction.
				case 'tpl':
					registers[ instruction[ 2 ] ] *= 3;

					break;

				// inc r increments register r, adding 1 to it, then continues with the next instruction.
				case 'inc':
					registers[ instruction[ 2 ] ] += 1;

					break;

				// jmp offset is a jump; it continues with the instruction offset away relative to itself.
				case 'jmp':
					offset += +instruction[ 2 ] - 1;

					break;

				// jie r, offset is like jmp, but only jumps if register r is even ("jump if even").
				case 'jie':
					if( registers[ instruction[ 2 ] ] % 2 === 0 )
					{
						offset += +instruction[ 3 ] - 1;
					}

					break;

				// jio r, offset is like jmp, but only jumps if register r is 1 ("jump if one", not odd).
				case 'jio':
					if( registers[ instruction[ 2 ] ] === 1 )
					{
						offset += +instruction[ 3 ] - 1;
					}

					break;
			}

			offset++;
		}

		return registers.b;
	};

	return [ Execute( 0 ), Execute( 1 ) ];
};
