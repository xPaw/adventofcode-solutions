window.AdventOfCode.Day16 = function( input )
{
	input = input.split( ',' );
	
	const permutations = [];
	const dancers = 'abcdefghijklmnop';
	let dancer = dancers.split( '' );
	let newDancer = dancers;
	
	const functions =
	{
		x: ( a, b ) => [ dancer[ a ], dancer[ b ] ] = [ dancer[ b ], dancer[ a ] ],
		p: ( a, b ) => functions.x( dancer.indexOf( a ), dancer.indexOf( b ) ),
		s: ( a ) => dancer = [ ...dancer.slice( -a ), ...dancer.slice( 0, -a ) ],
	};
	
	do
	{
		permutations.push( newDancer );
		
		input.forEach( d => functions[ d.charAt( 0 ) ]( ...d.substr( 1 ).split( '/' ) ) );
		
		newDancer = dancer.join( '' );
	}
	while( newDancer !== 'abcdefghijklmnop' );
	
	const part1 = permutations[ 1 ];
	const part2 = permutations[ 1000000000 % permutations.length ];
	
	return [ part1, part2 ];
};
