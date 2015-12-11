window.AdventOfCode.Day11 = function( input )
{
	var increase = /(abc|bcd|cde|def|efg|fgh|pqr|qrs|rst|stu|tuv|uvw|vwx|wxy|xyz)/;
	var repeat = /(.)\1.*(.)\2/; // BUG: this will match same repeating letters
	var illegal = /[iol]/;
	var answers = [];
	
	while( true )
	{
		input = ( ( parseInt( input, 36 ) + 1 ).toString( 36 ) ).replace( /0/g, 'a' );
		
		if( !illegal.test( input ) && increase.test( input ) && repeat.test( input ) )
		{
			
			answers.push( input );
			
			if( answers.length === 2 )
			{
				return answers;
			}
		}
	}
};
