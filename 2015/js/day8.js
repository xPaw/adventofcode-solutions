window.AdventOfCode.Day8 = function( input )
{
	input = input.split( '\n' );

	let totalLength = 0;
	let cleanLength = 0;
	let encodedLength = 0;

	for( let i = 0; i < input.length; i++ )
	{
		const str = input[ i ];
		let escaping = false;

		totalLength += str.length;
		encodedLength += str.length + 4;

		for( let x = 1; x < str.length - 1; x++ )
		{
			const char = str[ x ];

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
