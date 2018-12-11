window.AdventOfCode.Day11 = ( input ) =>
{
	input = +input;

	const calculatePowerLevel = ( x, y ) =>
	{
		const rackID = x + 10;
		const power = ( rackID * y + input ) * rackID;
		const hundreds = Math.floor( ( power % 1000 ) / 100 );
		return hundreds - 5;
	};

	const part1 = () =>
	{
		let bestCoordinate = [];
		let bestPowerLevel = 0;

		for( let x = 1; x <= 300; x++ )
		{
			for( let y = 1; y <= 300; y++ )
			{
				let powerLevel = 0;

				for( let x2 = 0; x2 < 3; x2++ )
				{
					for( let y2 = 0; y2 < 3; y2++ )
					{
						powerLevel += calculatePowerLevel( x + x2, y + y2 );
					}
				}

				if( powerLevel > bestPowerLevel )
				{
					bestPowerLevel = powerLevel;
					bestCoordinate = [ x, y ];
				}
			}
		}

		return bestCoordinate.join( ',' );
	};

	const part2 = () =>
	{
		let bestCoordinate = [];
		let bestPowerLevel = 0;

		for( let x = 1; x <= 300; x++ )
		{
			for( let y = 1; y <= 300; y++ )
			{
				const maxSize = Math.min( 300 - x, 300 - y );
				let powerLevel = 0;

				for( let size = 0; size <= maxSize; size++ )
				{
					powerLevel += calculatePowerLevel( x + size, y + size );

					for( let x2 = 0; x2 < size; x2++ )
					{
						powerLevel += calculatePowerLevel( x + x2, y + size );
					}

					for( let y2 = 0; y2 < size; y2++ )
					{
						powerLevel += calculatePowerLevel( x + size, y + y2 );
					}

					if( powerLevel > bestPowerLevel )
					{
						bestPowerLevel = powerLevel;
						bestCoordinate = [ x, y, size + 1 ];
					}
				}
			}
		}

		return bestCoordinate.join( ',' );
	};

	return [ part1(), part2() ];
};
