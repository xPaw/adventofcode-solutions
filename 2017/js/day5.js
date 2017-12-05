window.AdventOfCode.Day5 = function( input )
{
	let part2offsets = input.split( '\n' ).map( ( num ) => parseInt( num, 10 ) );
	
	let part1offsets = [...part2offsets];
	let offset = 0;
	let part1 = 0;
	let part2 = 0;
	
	do
	{
		part1++;
		
		const nextOffset = part1offsets[ offset ];
		
		part1offsets[ offset ]++;
		
		offset += nextOffset;
	}
	while( offset > -1 && offset < part1offsets.length );
	
	offset = 0;
	
	do
	{
		part2++;
		
		const nextOffset = part2offsets[ offset ];
		
		if( nextOffset >= 3 )
		{
			part2offsets[ offset ]--;
		}
		else
		{
			part2offsets[ offset ]++;
		}
		
		offset += nextOffset;
	}
	while( offset > -1 && offset < part2offsets.length );
	
	return [ part1, part2 ];
};
