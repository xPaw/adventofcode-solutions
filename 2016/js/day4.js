window.AdventOfCode.Day4 = function( input )
{
	input = input.split( '\n' );

	let validRooms = 0;
	let northpoleObjectStorage = -1;

	for( let line of input )
	{
		line = line.match( /(.+?)-(\d+)\[([a-z]+)\]/ );

		const name = line[ 1 ];
		const sector = +line[ 2 ];
		const checksum = line[ 3 ];
		const letters = line[ 1 ].match( /[a-z]/g );
		const countedLetters = new Map();

		for( const letter of letters )
		{
			countedLetters.set( letter, 1 + countedLetters.get( letter ) || 0 );
		}

		let sortedLetters = [];

		for( const letter of countedLetters )
		{
			sortedLetters.push( letter );
		}

		sortedLetters = sortedLetters
			.sort( ( a, b ) =>
			{
				if( a[ 1 ] === b[ 1 ] )
				{
					return a[ 0 ] > b[ 0 ] ? 1 : -1;
				}

				return b[ 1 ] - a[ 1 ];
			} )
			.map( letter => letter[ 0 ] );

		const actualChecksum = sortedLetters.join( '' ).substring( 0, 5 );

		if( checksum === actualChecksum )
		{
			validRooms += sector;

			if( northpoleObjectStorage === -1 )
			{
				let decryptedName = "";

				for( const letter of name )
				{
					if( letter === '-' )
					{
						decryptedName += ' ';
						continue;
					}

					decryptedName += String.fromCharCode( ( letter.charCodeAt( 0 ) - 97 + sector ) % 26 + 97 );
				}

				if( decryptedName === 'northpole object storage' )
				{
					northpoleObjectStorage = sector;
				}
			}
		}
	}

	return [ validRooms, northpoleObjectStorage ];
};
