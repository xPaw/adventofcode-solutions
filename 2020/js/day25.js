module.exports = ( input ) =>
{
	input = input.split( '\n' ).map( x => parseInt( x, 10 ) );

	const [ card, door ] = input;

	let part1 = 1;
	let door2 = 1;

	while( door2 !== door )
	{
		door2 = ( door2 * 7 ) % 20201227;
		part1 = ( part1 * card ) % 20201227;
	}

	return [ part1, 0 ];
};
