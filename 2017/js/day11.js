window.AdventOfCode.Day11 = function( input )
{
	const distances = [];
	let x = 0;
	let y = 0;
	let z = 0;
	
	input.split( ',' ).forEach( ( direction ) =>
	{
		switch( direction )
		{
			case 'n': y++; z--; break;
			case 's': y--; z++; break;
			case 'se': x++; y--; break;
			case 'sw': x--; z++; break;
			case 'ne': x++; z--; break;
			case 'nw': x--; y++; break;
		}
		
		distances.push( ( Math.abs( x ) + Math.abs( y ) + Math.abs( z ) ) / 2 );
	} );
	
	const part1 = distances[ distances.length - 1 ];
	const part2 = Math.max( ...distances );
	
	return [ part1, part2 ];
};
