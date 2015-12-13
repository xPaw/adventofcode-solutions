window.AdventOfCode.Day8 = function( input )
{
	input = input.split( '\n' );
	
	var totalLength = 0;
	var cleanLength = 0;
	var encodedLength = 0;
	
	for( var i = 0; i < input.length; i++ )
	{
		var str = input[ i ];
		var escaping = false;
		
		totalLength += str.length;
		encodedLength += str.length + 4;
		
		for( var x = 1; x < str.length - 1; x++ )
		{
			var char = str[ x ];
			
			if( escaping )
			{
				escaping = false;
				
				if( char === 'x' )
				{
					x += 2;
				}
				else
				{
					encodedLength++;
				}
			}
			else if( char === '\\' )
			{
				encodedLength++;
				
				escaping = true;
				
				continue;
			}
			
			cleanLength++;
		}
	}
	
	return [ totalLength - cleanLength, encodedLength - totalLength ];
};
