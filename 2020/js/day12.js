module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x => {
			return {
				action: x[ 0 ],
				value: parseInt( x.substring( 1 ), 10 )
			}
		} );

	function solvePart1()
	{
		let x = 0;
		let y = 0;
		let facing = 90; // east

		const angles =
		{
			0  : 'N',
			90 : 'E',
			180: 'S',
			270: 'W',
		};
		const actions =
		{
			N: value => y += value,
			E: value => x += value,
			S: value => y -= value,
			W: value => x -= value,
			L: value => facing = ( facing - ( value % 360 ) + 360 ) % 360,
			R: value => facing = ( facing + ( value % 360 ) + 360 ) % 360,
			F: value => actions[ angles[ facing ] ]( value ),
		}

		for( const { action, value } of input )
		{
			actions[ action ]( value );
		}

		return Math.abs( x ) + Math.abs( y );
	}

	function solvePart2()
	{
		let x = 0;
		let y = 0;
		let wx = 10;
		let wy = 1;

		const actions =
		{
			N: value => wy += value,
			E: value => wx += value,
			S: value => wy -= value,
			W: value => wx -= value,
			L: value => { for( let angle = value; angle > 0; angle -= 90 ) [ wx, wy ] = [ -wy, wx ] },
			R: value => { for( let angle = value; angle > 0; angle -= 90 ) [ wx, wy ] = [ wy, -wx ] },
			F: value => { x += wx * value; y += wy * value; },
		}

		for( const { action, value } of input )
		{
			actions[ action ]( value );
		}

		return Math.abs( x ) + Math.abs( y );
	}

	const part1 = solvePart1();
	const part2 = solvePart2();

	return [ part1, part2 ];
};
