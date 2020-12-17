window.AdventOfCode.Day6 = function( input )
{
	input = input.split( '\n' );

	const countedLetters = [];

	for( const line of input )
	{
		for( let i = 0; i < line.length; i++ )
		{
			if( !countedLetters[ i ] )
			{
				countedLetters[ i ] = new Map();
			}

			countedLetters[ i ].set( line[ i ], 1 + countedLetters[ i ].get( line[ i ] ) || 0 );
		}
	}

	let message1 = "";
	let message2 = "";

	for( const letters of countedLetters )
	{
		let commonLetter = '';
		let commonLetterCount = 0;
		let notCommonLetter = '';
		let notCommonLetterCount = Infinity;

		for( const letter of letters )
		{
			if( commonLetterCount < letter[ 1 ] )
			{
				commonLetterCount = letter[ 1 ];
				commonLetter = letter[ 0 ];
			}

			if( notCommonLetterCount > letter[ 1 ] )
			{
				notCommonLetterCount = letter[ 1 ];
				notCommonLetter = letter[ 0 ];
			}
		}

		message1 += commonLetter;
		message2 += notCommonLetter;
	}

	return [ message1, message2 ];
};
