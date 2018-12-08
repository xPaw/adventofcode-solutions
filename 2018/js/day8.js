window.AdventOfCode.Day8 = ( input ) =>
{
	input = input.split( ' ' ).map( x => +x );

	let part1 = 0;

	const process = () =>
	{
		const childrenSize = input.shift();
		const metadataSize = input.shift();
		let values = [];
		let value = 0;

		for( let i = childrenSize; i > 0; i-- )
		{
			values.push( process() );
		}

		const metadata = input.slice( 0, metadataSize );
		const metadataSum = metadata.reduce( ( a, b ) => a + b, 0 );

		part1 += metadataSum;

		if( childrenSize > 0 )
		{
			for( const i of metadata )
			{
				value += values[ i - 1 ] || 0;
			}
		}
		else
		{
			value = metadataSum;
		}

		input = input.slice( metadataSize );

		return value;
	};

	const part2 = process();

	return [ part1, part2 ];
};
