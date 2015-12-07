window.AdventOfCode.Day7 = function( originalInput )
{
	var Solve = function( wires )
	{
		var input = originalInput.split( '\n' );
		var numberRegex = /^[0-9]+$/;
		
		var TestValue = function( value )
		{
			return value in wires || numberRegex.test( value );
		};
		
		var GetValue = function( value )
		{
			if( value in wires )
			{
				return wires[ value ];
			}
			
			return +value;
		};
		
		var SetWire = function( wire, value )
		{
			// Can't assign same wire twice,
			// this becomes a problem when overriding b
			if( wire in wires )
			{
				return;
			}
			
			wires[ wire ] = value & 0xFFFF;
		};
		
		while( input.length )
		for( var i = 0; i < input.length; i++ )
		{
			var operation = input[ i ].split( ' ' );
			var solved = false;
			
			switch( operation.length )
			{
				// 123 -> x means that the signal 123 is provided to wire x.
				case 3: // ["123", "->", "x"]
					if( TestValue( operation[ 0 ] ) )
					{
						solved = true;
						
						SetWire( operation[ 2 ], GetValue( operation[ 0 ] ) );
					}
					
					break;
				
				// NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
				case 4: // ["NOT", "x", "->", "h"]
					if( TestValue( operation[ 1 ] ) )
					{
						solved = true;
						
						SetWire( operation[ 3 ], ~GetValue( operation[ 1 ] ) );
					}
					
					break;
				
				// x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
				case 5: // ["x", "AND", "y", "->", "d"]
					if( TestValue( operation[ 0 ] ) && TestValue( operation[ 2 ] ) )
					{
						solved = true;
						
						switch( operation[ 1 ] )
						{
							case 'AND'   : SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) &  GetValue( operation[ 2 ] ) ); break;
							case 'OR'    : SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) |  GetValue( operation[ 2 ] ) ); break;
							case 'LSHIFT': SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) << GetValue( operation[ 2 ] ) ); break;
							case 'RSHIFT': SetWire( operation[ 4 ], GetValue( operation[ 0 ] ) >> GetValue( operation[ 2 ] ) ); break;
						}
					}
					
					break;
			}
			
			if( solved )
			{
				input.splice( i, 1 );
			}
		}
		
		return wires[ 'a' ] || 0;
	};
	
	var solution1 = Solve( {} );
	var solution2 = Solve( { b: solution1 } );
	
	return [ solution1, solution2 ];
};
