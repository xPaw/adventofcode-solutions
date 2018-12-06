// 3933
// 41145

window.AdventOfCode.Day6 = ( input ) =>
{
	let minX = Number.MAX_SAFE_INTEGER;
	let minY = Number.MAX_SAFE_INTEGER;
	let maxX = -Number.MAX_SAFE_INTEGER;
	let maxY = -Number.MAX_SAFE_INTEGER;

	input = input
		.split( '\n' )
		.map( c =>
		{
			c = c.split( ', ' );

			const x = +c[ 0 ];
			const y = +c[ 1 ];

			if( minY > y ) minY = y;
			if( maxY < y ) maxY = y;
			if( minX > x ) minX = x;
			if( maxX < x ) maxX = x;

			return [ x, y ];
		} );

	const grid = [];
	/*const hull = [];

	for( const coord of input )
	{
		let a, b, c, d;
		
		for( const coordOther of input )
		{
			if( coord[ 0 ] === coordOther[ 0 ] && coord[ 1 ] === coordOther[ 1 ] ) continue;
			if( coord[ 0 ] > coordOther[ 0 ] ) a = true;
			if( coord[ 0 ] < coordOther[ 0 ] ) b = true;
			if( coord[ 1 ] > coordOther[ 1 ] ) c = true;
			if( coord[ 1 ] < coordOther[ 1 ] ) d = true;
		}

		if( a && b && c && d )
		{
			hull[ coord[ 0 ] * maxX + coord[ 1 ] ] = true;
		}
	}*/

	for( let x = minX; x <= maxX; x++ )
	{
		for( let y = minY; y <= maxY; y++ )
		{
			let closest = Number.MAX_SAFE_INTEGER;
			let closestPoint;
			let distances = [];

			for( const coord of input )
			{
				const distance = Math.abs( coord[ 0 ] - x ) + Math.abs( coord[ 1 ] - y );

				distances.push( distance );

				if( closest > distance )
				{
					closest = distance;
					closestPoint = coord;
				}
			}

			distances = distances.sort( ( a, b ) => a - b );
			closestPoint = closestPoint[ 0 ] * maxX + closestPoint[ 1 ];

			if( x === maxX || x === minX || y === minY || y === maxY )
			{
				closestPoint = null;
			}

			grid[ x * maxX + y ] =
			{
				distance: distances.reduce( ( a, b ) => a + b, 0 ),
				closest: distances[ 1 ] > distances[ 0 ] ? closestPoint : null,
			};
		}
	}

	console.log( grid.reduce( ( a, point ) =>
	{
		if( !point || !point.closest )
		{
			return a;
		}

		a[ point.closest ] = 1 + ( a[ point.closest ] || 0 );

		return a;
	}, [] ).sort( ( a, b ) => b - a ) );

	return [ 0, 0 ];
};
