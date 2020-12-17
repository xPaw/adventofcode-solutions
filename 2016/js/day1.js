window.AdventOfCode.Day1 = function( input )
{
	input = input.split( ', ' );

	const visited = new Set();
	let visitedTwiceDistance = null;
	let direction = 0;
	const position = { x: 0, y: 0 };

	for( let i of input )
	{
		const turn = i[ 0 ] === 'L' ? -1 : 1;
		const coordinate = direction % 2 === 1 ? 'x' : 'y';
		let travel = +i.substring( 1 );

		direction = ( direction + turn + 4 ) % 4;

		if( direction > 1 )
		{
			travel *= -1;
		}

		const start = { x: position.x, y: position.y };
		position[ coordinate ] += travel;

		if( visitedTwiceDistance === null )
		{
			const end = position;
			let x = start.x;

			xLoop:
			do
			{
				let y = start.y;

				do
				{
					i = x + "." + y;

					if( visited.has( i ) )
					{
						visitedTwiceDistance = Math.abs( x ) + Math.abs( y );
						break xLoop;
					}
					else
					{
						visited.add( i );
					}

					if( y !== end.y )
					{
						direction > 1 ? y-- : y++;
					}
				}
				while( y !== end.y );

				if( x !== end.x )
				{
					direction > 1 ? x-- : x++;
				}
			}
			while( x !== end.x );
		}
	}

	return [ Math.abs( position.x ) + Math.abs( position.y ), visitedTwiceDistance ];
};
