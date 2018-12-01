window.AdventOfCode.Day1 = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => +x );
	
	let index = 0;
	let seenValue = 0;
	const seen = new Map();

	do
	{
		seenValue += input[ index % input.length ];
		index++;

		if( seen.has( seenValue ) )
		{
			break;
		}

		seen.set( seenValue, true );
	}
	while( true );

	const sum = input.reduce( ( p, c ) => p + c, 0 );
	
	return [ sum, seenValue ];
};
