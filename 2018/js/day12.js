window.AdventOfCode.Day12 = ( input ) =>
{
	input = input.split( '\n' );

	const rules = [];

	for( let i = 2; i < input.length; i++ )
	{
		const rule = input[ i ].split( ' => ' );

		if( rule[ 1 ] === '#' )
		{
			rules.push( rule );
		}
	}

	const leftPadding = 20;
	const sum = ( state ) =>
	{
		let result = 0;

		for( let c in state )
		{
			if( state[ c ] === '#' )
			{
				result += c - leftPadding;
			}
		}

		return result;
	};

	let state = '.'.repeat( leftPadding ) + input[ 0 ].substring( 15 ) + '.'.repeat( 300 );
	state = state.split( '' );

	let previousSum = 0;
	let i = 0;
	let part1 = 0;

	for( ; i < 200; i++ )
	{
		let nextState = Array( state.length ).fill( '.' );

		for( let c = 0; c < state.length; c++ )
		{
			for( let r = 0; r < rules.length; r++ )
			{
				let match = true;
				for( let x = 0; x < 5; x++ )
				{
					if( state[ c + x ] !== rules[ r ][ 0 ][ x ] )
					{
						match = false;
						break;
					}
				}

				if( match )
				{
					nextState[ c + 2 ] = rules[ r ][ 1 ];
					break;
				}
			}
		}

		const currentSum = sum( nextState );

		if( previousSum + leftPadding === currentSum )
		{
			break;
		}

		previousSum = currentSum;
		state = nextState;

		if( i === 19 )
		{
			part1 = currentSum;
		}
	}

	const part2 = sum( state ) + ( ( 50000000000 - i ) * leftPadding );

	return [ part1, part2 ];
};
