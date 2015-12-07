window.AdventOfCode.Day1 = function( input )
{
	var floor = 0;
	var position = -1;
	
	// Santa starts on the ground floor (floor 0) and then
	// follows the instructions one character at a time.
	for( var i = 0; i < input.length; i++ )
	{
		// An opening parenthesis, (, means he should go up one floor.
		if( input[ i ] === '(' )
		{
			floor++;
		}
		// A closing parenthesis, ), means he should go down one floor.
		else
		{
			floor--;
		}
		
		// Now, given the same instructions, find the position of the first
		// character that causes him to enter the basement (floor -1).
		// The first character in the instructions has position 1,
		// the second character has position 2, and so on.
		if( floor === -1 && position < 0 )
		{
			// ) causes him to enter the basement at character position 1.
			// ()()) causes him to enter the basement at character position 5.
			// This is 1-indexed.
			position = i + 1;
		}
	}
	
	return [ floor, position ];
};
