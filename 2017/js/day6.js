window.AdventOfCode.Day6 = function( input )
{
	input = input.split( /\s/ ).map( ( num ) => parseInt( num, 10 ) );
	
	const duplicates = new Map();
	let duplicateHit = false;
	let isDuplicate = false;
	let part1 = 0;
	let part2 = 0;
	let hit = '';
	
	while( true )
	{
		isDuplicate = duplicates.has( hit );
		
		duplicates.set( hit, true );
		
		if( duplicateHit )
		{
			part2++;
			
			if( duplicateHit === hit )
			{
				break;
			}
		}
		else if( isDuplicate )
		{
			duplicateHit = hit;
		}
		else
		{
			part1++;
		}
		
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
	
	return [ part1, part2 ];
};
