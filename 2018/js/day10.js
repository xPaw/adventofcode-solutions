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

	const pixels = [];

	for( const point of points )
	{
		point.y -= minY;
		point.x -= minX;

		if( !pixels[ point.y ] )
		{
			pixels[ point.y ] = [];
		}

		pixels[ point.y ][ point.x ] = 1;
	}

	const letters =
	{
		'  ##   #  # #    ##    ##    ########    ##    ##    ##    #': 'A',
		'##### #    ##    ##    ###### #    ##    ##    ##    ###### ': 'B',
		' #### #    ##     #     #     #     #     #     #    # #### ': 'C',
		'#######     #     #     ##### #     #     #     #     ######': 'E',
		'#######     #     #     ##### #     #     #     #     #     ': 'F',
		' #### #    ##     #     #     #  ####    ##    ##   ## ### #': 'G',
		'#    ##    ##    ##    ########    ##    ##    ##    ##    #': 'H',
		'   ###    #     #     #     #     #     # #   # #   #  ###  ': 'J',
		'#    ##   # #  #  # #   ##    ##    # #   #  #  #   # #    #': 'K',
		'#     #     #     #     #     #     #     #     #     ######': 'L',
		'#    ###   ###   ## #  ## #  ##  # ##  # ##   ###   ###    #': 'N',
		'##### #    ##    ##    ###### #     #     #     #     #     ': 'P',
		'##### #    ##    ##    ###### #  #  #   # #   # #    ##    #': 'R',
		'#    ##    # #  #  #  #   ##    ##   #  #  #  # #    ##    #': 'X',
		'######     #     #    #    #    #    #    #     #     ######': 'Z',
	};

	let answer = '';
	const letterCount = Math.round( maxX / 8 );

	for( let letter = 0; letter < letterCount; letter++ )
	{
		let stride = letter * 8;
		let string = '';

		for( let y = 0; y <= maxY; y++ )
		{
			for( let x = stride; x < stride + 6; x++ )
			{
				string += pixels[ y ][ x ] ? '#' : ' ';
			}
		}

		answer += letters[ string ];
	}

	return [ answer, iterations ];
};
