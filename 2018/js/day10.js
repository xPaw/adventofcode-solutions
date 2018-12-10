window.AdventOfCode.Day10 = ( input ) =>
{
	input = input.match( /(-?[0-9]+)/g );

	const points = [];

	for( let i = 0; i < input.length; i += 4 )
	{
		points.push( {
			x: +input[ i ],
			y: +input[ i + 1 ],
			vx: +input[ i + 2 ],
			vy: +input[ i + 3 ],
		} );
	}

	let iterations = 0;

	while( ++iterations )
	{
		let min = Number.MAX_SAFE_INTEGER;
		let max = Number.MIN_SAFE_INTEGER;

		for( const point of points )
		{
			point.x += point.vx;
			point.y += point.vy;

			if( point.y > max )
			{
				max = point.y;
			}
			if( point.y < min )
			{
				min = point.y;
			}
		}

		if( max - min === 9 )
		{
			break;
		}

		min = max;
	}

	let minX = Number.MAX_SAFE_INTEGER;
	let minY = Number.MAX_SAFE_INTEGER;
	let maxX = Number.MIN_SAFE_INTEGER;
	let maxY = Number.MIN_SAFE_INTEGER;

	for( const point of points )
	{
		if( point.x < minX ) minX = point.x;
		if( point.x > maxX ) maxX = point.x;
		if( point.y < minY ) minY = point.y;
		if( point.y > maxY ) maxY = point.y;
	}

	maxX -= minX;
	maxY -= minY;

	const pixels = Array( ( maxY + 1 ) * maxX ).fill( ' ' );

	for( const point of points )
	{
		point.y -= minY;
		point.x -= minX;

		pixels[ point.y * maxX + point.x ] = '#';
	}

	let string = '';

	for( let y = 0; y <= maxY; y++ )
	{
		for( let x = 0; x <= maxX; x++ )
		{
			string += pixels[ y * maxX + x ];
		}

		string += '\n';
	}

	console.log( string );

	return [ 'See console', iterations ];
};
