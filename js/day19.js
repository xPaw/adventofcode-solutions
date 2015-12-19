window.AdventOfCode.Day19 = function( input )
{
	input = input.split( '\n' );
	
	var molecule = input.pop();
	input.pop(); // empty line
	
	input = input.map( function( a )
	{
		return a.split( ' => ' );
	} );
	
	var uniqueNewMolecules = {};
	
	for( var i = 0; i < molecule.length; i++ )
	{
		for( var y = 0; y < input.length; y++ )
		{
			if( molecule.substr( i, input[ y ][ 0 ].length ) === input[ y ][ 0 ] )
			{
				uniqueNewMolecules[ molecule.slice( 0, i ) + input[ y ][ 1 ] + molecule.slice( i + input[ y ][ 0 ].length ) ] = true;
			}
		}
	}
	
	return [ Object.keys( uniqueNewMolecules ).length, 0 ];
};
