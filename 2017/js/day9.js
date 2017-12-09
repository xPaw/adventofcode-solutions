window.AdventOfCode.Day9 = function( input )
{
	let isGarbage = false;
	let totalScore = 0;
	let totalGarbage = 0;
	let groups = 0;
	
	for( let i = 0; i < input.length; i++ )
	{
		const character = input[ i ];
		
		// any character that comes after ! should be ignored
		if( character === '!' )
		{
			i++;
		}
		// count the garbage
		else if( isGarbage )
		{
			// garbage ends with >
			if( character === '>' )
			{
				isGarbage = false;
			}
			else
			{
				totalGarbage++;
			}
		}
		// garbage begins with <
		else if( character === '<' )
		{
			isGarbage = true;
		}
		// groups begin with {
		else if( character === '{' )
		{
			totalScore += ++groups;
		}
		// groups end with }
		else if( character == '}' )
		{
			--groups;
		}
	}
	
	return [ totalScore, totalGarbage ];
};
