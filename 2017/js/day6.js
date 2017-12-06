window.AdventOfCode.Day6 = function( input )
{
	input = input.split( /\s/ ).map( ( num ) => parseInt( num, 10 ) );
	
	const duplicates = new Map();
	let part1 = 0;
	let part2 = 0;
	let hit = '';
	
	while( !duplicates.has( hit ) )
	{
		duplicates.set( hit, part1++ );
		
		let highestValue = -1;
		let highestIndex = -1;
		
		for( let i = 0; i < input.length; i++ )
		{
			if( input[ i ] > highestValue )
			{
				highestValue = input[ i ];
				highestIndex = i;
			}
		}
		
		input[ highestIndex ] = 0;
		
		while( highestValue-- > 0 )
		{
			input[ ++highestIndex % input.length ]++;
		}
		
		hit = input.join( ',' );
	}
	
	part2 = part1 - duplicates.get( hit );
	
	return [ part1, part2 ];
};
