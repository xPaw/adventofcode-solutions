window.AdventOfCode.Day4 = ( input ) =>
{
	input = input
		.split( '\n' )
		.sort();

	const guards = [];
	let currentGuard;
	let asleep;

	for( const line of input )
	{
		let result;

		if( result = /Guard #([0-9]+)/.exec( line ) )
		{
			currentGuard = +result[ 1 ];

			if( !guards[ currentGuard ] )
			{
				guards[ currentGuard ] = [];
			}
		}
		else if( result = /:([0-9]+)] falls asleep/.exec( line ) )
		{
			asleep = +result[ 1 ];
		}
		else if( result = /:([0-9]+)] wakes up/.exec( line ) )
		{
			const awake = +result[ 1 ];

			for( let i = asleep; i < awake; i++ )
			{
				guards[ currentGuard ][ i ] = 1 + ( guards[ currentGuard ][ i ] || 0 );
			}
		}
	}

	let highestMinutesGuard = 0;
	let highestMinutes = 0;
	let highestMinute = 0;
	let frequentMinuteGuard = 0;
	let frequentMinutes = 0;
	let frequentMinute = 0;

	guards.map( ( minutes, guard ) =>
	{
		let currentHighestMinute = 0;
		let currentHighestMinuteCount = 0;
		let totalMinutes = 0;

		minutes.map( ( count, minute ) =>
		{
			totalMinutes += count;

			if( currentHighestMinuteCount < count )
			{
				currentHighestMinuteCount = count;
				currentHighestMinute = minute;
			}
		} );

		if( totalMinutes > highestMinutes )
		{
			highestMinutesGuard = guard;
			highestMinutes = totalMinutes;
			highestMinute = currentHighestMinute;
		}

		if( currentHighestMinuteCount > frequentMinutes )
		{
			frequentMinuteGuard = guard;
			frequentMinutes = currentHighestMinuteCount;
			frequentMinute = currentHighestMinute;
		}
	} );

	return [ highestMinutesGuard * highestMinute, frequentMinuteGuard * frequentMinute ];
};
