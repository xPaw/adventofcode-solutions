window.AdventOfCode.Day14 = function( input )
{
	var seconds = 2503;
	
	input = input.split( '\n' ).map( function( a )
	{
		var match = a.match( /^(.+) can fly (\d+) km\/s for (\d+) seconds, but then must rest for (\d+) seconds/ );
		
		return {
			speed: +match[ 2 ],
			time: +match[ 3 ],
			mustRest: +match[ 4 ],
			travellingFor: 0,
			distance: 0,
			resting: 0,
			points: 0
		};
	} );
	
	var farthestDeer = 0;
	var mostPoints = 0;
	
	while( seconds-- > 0 )
	{
		var i, deer, leading = 0;
		
		for( i = 0; i < input.length; i++ )
		{
			deer = input[ i ];
			
			if( deer.resting )
			{
				deer.resting--;
			}
			else
			{
				deer.distance += deer.speed;
				
				if( ++deer.travellingFor === deer.time )
				{
					deer.resting = deer.mustRest;
					deer.travellingFor = 0;
				}
			}
			
			if( leading < deer.distance )
			{
				leading = deer.distance;
			}
		}
		
		for( i = 0; i < input.length; i++ )
		{
			deer = input[ i ];
			
			if( deer.distance === leading )
			{
				deer.points++;
			}
			
			if( !seconds )
			{
				if( farthestDeer < deer.distance )
				{
					farthestDeer = deer.distance;
				}
				
				if( mostPoints < deer.points )
				{
					mostPoints = deer.points;
				}
			}
		}
	}
	
	return [ farthestDeer, mostPoints ];
};
