window.AdventOfCode.Day3 = function( input )
{
	input = +input;
	
	// https://www.reddit.com/r/adventofcode/comments/7h7ufl/2017_day_3_solutions/dqoxrb7/
	const root = Math.ceil( Math.sqrt( input ) );
    const sideLength = root % 2 !== 0 ? root : root + 1;
    const centerLength = ( sideLength - 1 ) / 2;
    const cycle = input - Math.pow( ( sideLength - 2 ), 2 );
    const innerOffset = cycle % ( sideLength - 1 );
    const part1 = centerLength + Math.abs( innerOffset - centerLength );
	
	fetch( 'https://cors-anywhere.herokuapp.com/https://oeis.org/A141481/b141481.txt' )
	.then( ( response ) =>
	{
		return response.text();
	} )
	.then( ( response ) =>
	{
		response = response
			.split( '\n' )
			.map( ( line ) => line.split( ' ' )[ 1 ] );
		
		const part2 = response.find( ( value ) => value > input );
		console.log( part2 );
	} );
	
	// Can't be bothered to write code for part 2
	return [ part1, 'console' ];
};
