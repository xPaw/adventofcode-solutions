window.AdventOfCode.Day19 = function( input )
{
	input = input.split( '' ).reverse().join( '' ).split( '\n' );

	let molecule = input.shift();
	input.shift(); // empty line

	input = input.map( function( a )
	{
		return a.split( ' >= ' );
	} );

	const uniqueNewMolecules = {};
	let i, y;

	for( i = 0; i < molecule.length; i++ )
	{
		for( y = 0; y < input.length; y++ )
		{
			if( molecule.substr( i, input[ y ][ 1 ].length ) === input[ y ][ 1 ] )
			{
				uniqueNewMolecules[ molecule.slice( 0, i ) + input[ y ][ 0 ] + molecule.slice( i + input[ y ][ 1 ].length ) ] = true;
			}
		}
	}

	let steps = 0;
	const lookup = {};
	let replaced = false;

	input = input.map( function( a )
	{
		lookup[ a[ 0 ] ] = a[ 1 ];

		return a[ 0 ];
	} );

	const regexp = new RegExp( '(' + input.join( '|' ) + ')', 'g' );
	const replaceCallback = function( matched )
	{
		replaced = true;

		steps++;

		return lookup[ matched ];
	};

	do
	{
		replaced = false;
		molecule = molecule.replace( regexp, replaceCallback );
	}
	while( replaced );

	return [ Object.keys( uniqueNewMolecules ).length, steps ];
};
