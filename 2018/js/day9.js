window.AdventOfCode.Day9 = ( input ) =>
{
	input = input.match( /([0-9]+)/g );

	const players = +input[ 0 ];
	const lastMarble = +input[ 1 ];

	const placeMarble = ( points, marble ) =>
	{
		const obj =
		{
			points: points,
			prev: marble,
			next: marble.next,
		};
		marble.next.prev = obj;
		marble.next = obj;
		return obj;
	};

	const play = function( players, lastMarble )
	{
		const scores = Array( players ).fill( 0 );
		let player = 0;
		let current =
		{
			points: 0,
		};
		current.next = current;
		current.prev = current;

		for( let marble = 1; marble <= lastMarble; marble++ )
		{
			if( marble % 23 !== 0 )
			{
				current = placeMarble( marble, current.next );
			}
			else
			{
				current = current.prev.prev.prev.prev.prev.prev;

				scores[ player ] += marble + current.prev.points;

				current.prev.prev.next = current;
				current.prev = current.prev.prev;
			}

			player = ++player % players;
		}

		return Math.max( ...scores );
	};

	return [
		play( players, lastMarble ),
		play( players, lastMarble * 100 )
	];
};
