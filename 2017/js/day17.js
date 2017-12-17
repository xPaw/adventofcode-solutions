window.AdventOfCode.Day17 = function( input )
{
	input = parseInt( input, 10 );
	
	const buffer = [ 0 ];
	let step = 0;
	
	for( let i = 1; i <= 2017; i++ )
	{
		step = ( step + input + 1 ) % i;
		buffer.splice( step, 0, i );
	}
	
	const part1 = buffer[ ( step + 1 ) % buffer.length ];
	let part2 = 0;
	
	for( let i = 1; i <= 50000000; i++ )
	{
		step = ( step + input + 1 ) % i;
		
		if( step == 0 )
		{
			part2 = i;
		}
	}
	
	return [ part1, part2 ];
};
