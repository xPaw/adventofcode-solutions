/* globals SparkMD5 */
window.AdventOfCode.Day5 = function( input )
{
	let i = 2307654;
	let hash;
	let part1password = '';
	let part2password = [];
	
	do
	{
		hash = SparkMD5.hash( input + i++ );
		
		if( hash[ 0 ] === '0'
		&&  hash[ 1 ] === '0'
		&&  hash[ 2 ] === '0'
		&&  hash[ 3 ] === '0'
		&&  hash[ 4 ] === '0' )
		{
			let position = hash[ 5 ];
			
			if( part1password.length < 8 )
			{
				part1password += position;
			}
			
			if( position >= 0 && position <= 7 && part2password[ position ] === undefined )
			{
				part2password[ position ] = hash[ 6 ];
			}
			
			if( part1password.length === 8 && part2password.filter( () => true ).length === 8 )
			{
				break;
			}
		}
	}
	while( true ); // eslint-disable-line no-constant-condition
	
	return [ part1password, part2password.join( '' ) ];
};
