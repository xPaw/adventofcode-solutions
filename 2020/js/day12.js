module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x =>
		{
			return {
				action: x[ 0 ],
				value: parseInt( x.substring( 1 ), 10 )
			};
		} );

	// Part 1
	let x = 0;
	let y = 0;
	let wx = 1;
	let wy = 0;

	function solve()
	{
		for( const { action, value } of input )
		{
			actions[ action ]( value );
		}

		return Math.abs( x ) + Math.abs( y );
	}

	const actions =
	{
		N: value => y += value,
		E: value => x += value,
		S: value => y -= value,
		W: value => x -= value,
		L: value =>
		{
			for( let angle = value; angle > 0; angle -= 90 ) [ wx, wy ] = [ -wy, wx ];
		},
		R: value =>
		{
			for( let angle = value; angle > 0; angle -= 90 ) [ wx, wy ] = [ wy, -wx ];
		},
		F: value =>
		{
			x += wx * value; y += wy * value;
		},
	};

	const part1 = solve();

	// Part 2
	x = 0;
	y = 0;
	wx = 10;
	wy = 1;

	actions.N = value => wy += value;
	actions.E = value => wx += value;
	actions.S = value => wy -= value;
	actions.W = value => wx -= value;

	const part2 = solve();

	return [ part1, part2 ];
};
