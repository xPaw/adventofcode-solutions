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
	var steps = 0;
	var i, y;
	
	for( i = 0; i < molecule.length; i++ )
	{
		for( y = 0; y < input.length; y++ )
		{
			if( molecule.substr( i, input[ y ][ 0 ].length ) === input[ y ][ 0 ] )
			{
				uniqueNewMolecules[ molecule.slice( 0, i ) + input[ y ][ 1 ] + molecule.slice( i + input[ y ][ 0 ].length ) ] = true;
			}
		}
	}
	
	// http://stackoverflow.com/a/6274398/2200891
	function shuffle(array)
	{
		var counter = array.length, temp, index;
		
		while (counter > 0)
		{
			index = Math.floor(Math.random() * counter);
			
			counter--;
			
			temp = array[counter];
			array[counter] = array[index];
			array[index] = temp;
		}
		
		return array;
	}
	
	var target = molecule;
	
	while( target !== 'e' )
	{
		var lastTarget = target;
		
		for( y = 0; y < input.length; y++ )
		{
			i = target.indexOf( input[ y ][ 1 ] );
			
			if( i > -1 )
			{
				steps++;
				
				target = target.slice( 0, i ) + input[ y ][ 0 ] + target.slice( i + input[ y ][ 1 ].length );
			}
		}
		
		if( lastTarget === target )
		{
			// Shuffling is hilariously good enough to solve this
			input = shuffle( input );
			target = molecule;
			steps = 0;
		}
	}
	
	return [ Object.keys( uniqueNewMolecules ).length, steps ];
};
