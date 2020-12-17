window.AdventOfCode.Day13 = function( input )
{
	input = input.split( '\n' ).map( ( line ) => line.split( ': ' ).map( ( num ) => parseInt( num, 10 ) ) );

	const part1 = input
		.filter( ( [ depth, range ] ) => depth % ( ( range - 1 ) * 2 ) === 0 )
		.reduce( ( count, [ depth, range ] ) => count + depth * range, 0 );

	let part2 = -1;
	const isCaught = delay => ( [ depth, range ] ) => ( delay + depth ) % ( 2 * ( range - 1 ) ) === 0;

	while( input.some( isCaught( ++part2 ) ) );

	return [ part1, part2 ];
};
