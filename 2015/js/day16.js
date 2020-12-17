window.AdventOfCode.Day16 = function( input )
{
	const thingsYouRemember =
	{
		children   : '3',
		cats       : '7',
		samoyeds   : '2',
		pomeranians: '3',
		akitas     : '0',
		vizslas    : '0',
		goldfish   : '5',
		trees      : '3',
		cars       : '2',
		perfumes   : '1'
	};

	input = input.replace( /[,:]/g, '' ).split( '\n' ).map( function( aunt )
	{
		return aunt.split( ' ' );
	} );

	const dayOne = input.filter( function( aunt )
	{
		return (
			thingsYouRemember[ aunt[ 2 ] ] == aunt[ 3 ] &&
			thingsYouRemember[ aunt[ 4 ] ] == aunt[ 5 ] &&
			thingsYouRemember[ aunt[ 6 ] ] == aunt[ 7 ] );
	} );

	const filterPartTwo = function( what, count )
	{
		if( what === 'cats' || what === 'trees' )
		{
			return thingsYouRemember[ what ] < count;
		}

		if( what === 'pomeranians' || what === 'goldfish' )
		{
			return thingsYouRemember[ what ] > count;
		}

		return thingsYouRemember[ what ] == count;
	};

	const dayTwo = input.filter( function( aunt )
	{
		return (
			filterPartTwo( aunt[ 2 ], aunt[ 3 ] ) &&
			filterPartTwo( aunt[ 4 ], aunt[ 5 ] ) &&
			filterPartTwo( aunt[ 6 ], aunt[ 7 ] ) );
	} );

	return [ dayOne[ 0 ][ 1 ], dayTwo[ 0 ][ 1 ] ];
};
