window.AdventOfCode.Day2 = function( input )
{
	input = input.split( '\n' );
	
	// Part 1
	let keypad =
	[
		[ 1, 2, 3 ],
		[ 4, 5, 6 ],
		[ 7, 8, 9 ],
	];
	
	let Clamp = function( i )
	{
		return i < 0 ? 0 : i > 2 ? 2 : i;
	}
	
	let position = { x: 1, y: 1 };
	let part1 = "";
	
	for( let line of input )
	{
		for( let i of line )
		{
			switch( i )
			{
				case 'L': position.x--; break;
				case 'R': position.x++; break;
				case 'U': position.y--; break;
				case 'D': position.y++; break;
			}
			
			position.x = Clamp( position.x );
			position.y = Clamp( position.y );
		}
		
		part1 += keypad[ position.y ][ position.x ];
	}
	
	// Part 2
	keypad =
	[
		[ null, null,   1, null,  null ],
		[ null,    2,   3,    4,  null ],
		[    5,    6,   7,    8,     9 ],
		[ null,  'A',  'B',  'C', null ],
		[ null, null,  'D', null, null ],
	];
	
	let IsValidPosition = function()
	{
		if( !keypad[ position.y ] )
		{
			return false;
		}
		
		return !!keypad[ position.y ][ position.x ];
	};
	
	position = { x: 0, y: 2 };
	let part2 = "";
	
	for( let line of input )
	{
		for( let i of line )
		{
			const currentPosition = { x: position.x, y: position.y };
			
			switch( i )
			{
				case 'L': position.x--; break;
				case 'R': position.x++; break;
				case 'U': position.y--; break;
				case 'D': position.y++; break;
			}
			
			if( !IsValidPosition() )
			{
				position = currentPosition;
			}
		}
		
		part2 += keypad[ position.y ][ position.x ];
	}
	
	return [ part1, part2 ];
};
