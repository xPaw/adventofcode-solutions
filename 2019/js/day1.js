module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => Math.floor( x / 3 ) - 2 );

	const part1 = input.reduce( ( x, y ) => x + y, 0 );
	let part2 = part1;

	do
	{
		input = input.map( x => Math.floor( x / 3 ) - 2 );
		part2 += input.reduce( ( x, y ) => y > 0 ? x + y : x, 0 );
	}
	while( input.some( x => x > 0 ) );

	return [ part1, part2 ];
};
