module.exports = ( input ) =>
{
	input = input.split( '\n' ).map( x => x.match( /(e|se|sw|w|nw|ne)/g ) );

	const doublewidth_directions =
	{
		e : [ +2,  0 ],
		se: [ +1, -1 ],
		sw: [ -1, -1 ],
		w : [ -2,  0 ],
		nw: [ -1, +1 ],
		ne: [ +1, +1 ],
	};

	function hash( x, y )
	{
		return x * 10000000 + y;
	}

	function getNeighbors( baseX, baseY )
	{
		return Object.values( doublewidth_directions ).map( ( [ x, y ] ) =>
		{
			return {
				x: baseX + x,
				y: baseY + y,
			};
		} );
	}

	let visited = new Map();

	for( const path of input )
	{
		let x = 0;
		let y = 0;

		for( const dir of path )
		{
			const [ offX, offY ] = doublewidth_directions[ dir ];
			x += offX;
			y += offY;
		}

		const key = hash( x, y );

		if( visited.has( key ) )
		{
			visited.delete( key );
		}
		else
		{
			visited.set( key, { x, y } );
		}
	}

	const part1 = visited.size;

	for( let day = 0; day < 100; day++ )
	{
		const tileCounts = new Map();

		for( const coord of visited.values() )
		{
			const neighbors = getNeighbors( coord.x, coord.y );

			for( const tile of neighbors )
			{
				const key = hash( tile.x, tile.y );
				const obj = tileCounts.get( key );

				if( obj )
				{
					obj.occupied++;
				}
				else
				{
					tileCounts.set( key, {
						occupied: 1,
						x: tile.x,
						y: tile.y,
					} );
				}
			}
		}

		const newVisited = new Map();

		for( const [ key, coord ] of tileCounts )
		{
			if( coord.occupied === 2 || ( coord.occupied === 1 && visited.has( key ) ) )
			{
				newVisited.set( key, { x: coord.x, y: coord.y } );
			}
		}

		visited = newVisited;
	}

	const part2 = visited.size;

	return [ part1, part2 ];
};
