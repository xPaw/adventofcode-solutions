(function()
{
	window.AdventOfCode.Day2 = function( input )
	{
		input = input.split( '\n' );
		
		var completeArea = 0;
		var completeRibbon = 0;
		
		for( var i = 0; i < input.length; i++ )
		{
			// They have a list of the dimensions (length l, width w, and height h)
			// of each present, and only want to order exactly as much as they need.
			var present = input[ i ].split( 'x' );
			
			var sides =
			[
				present[ 0 ] * present[ 1 ],
				present[ 1 ] * present[ 2 ],
				present[ 2 ] * present[ 0 ]
			];
			
			// Find the surface area of the box, which is 2*l*w + 2*w*h + 2*h*l.
			var area =
				2 * sides[ 0 ] +
				2 * sides[ 1 ] +
				2 * sides[ 2 ];
			
			// The elves also need a little extra paper for each present:
			// the area of the smallest side.
			completeArea += area + Math.min.apply( Math, sides );
			
			// The ribbon required to wrap a present is the shortest distance
			// around its sides, or the smallest perimeter of any one face.
			// Each present also requires a bow made out of ribbon as well;
			// the feet of ribbon required for the perfect bow is equal to
			// the cubic feet of volume of the present.
			var sortedDimensions = present.sort( function( a, b ) { return a - b; } );
			
			// Plus symbol before each number is to cast the value to a number.
			// Javascript's sort() is pretty dumb and forces the values to be strings.
			var ribbon =
				+sortedDimensions[ 0 ] + +sortedDimensions[ 0 ] +
				+sortedDimensions[ 1 ] + +sortedDimensions[ 1 ];
			
			var bow =
				+sortedDimensions[ 0 ] *
				+sortedDimensions[ 1 ] *
				+sortedDimensions[ 2 ];
			
			completeRibbon += ribbon + bow;
		}
		
		var result =
		{
			wrappingPaper: completeArea,
			ribbon: completeRibbon,
		};
		
		return result;
	};
	
	document.getElementById( 'day2-textarea' ).addEventListener( 'change', function()
	{
		var input = this.value.trim();
		
		if( input.length === 0 )
		{
			return;
		}
		
		var solution = window.AdventOfCode.Day2( input );
		
		document.getElementById( 'day2-partone' ).textContent = solution.wrappingPaper;
		document.getElementById( 'day2-parttwo' ).textContent = solution.ribbon;
	}, false );
}());
