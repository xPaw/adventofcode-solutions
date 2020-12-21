module.exports = ( input ) =>
{
	input = input.split( '\n' ).map( l =>
	{
		const m = l.match( /^(?<items>[a-z ]+) \(contains (?<allergens>[a-z ,]+)\)/ );
		return {
			items: new Set( m.groups.items.split( ' ' ) ),
			allergens: new Set( m.groups.allergens.split( ', ' ) ),
		};
	} );

	let part1 = 0;
	let part2 = 0;

	const items = new Set();
	const allergens = new Map();

	for( const food of input )
	{
		for( const item of food.items )
		{
			items.add( item );
		}
	}

	for( const food of input )
	{
		for( const allergen of food.allergens )
		{
			allergens.set( allergen, new Set( items ) );
		}
	}

	for( const [ allergen, items ] of allergens )
	{
		for( const food of input )
		{
			if( food.allergens.has( allergen ) )
			{
				for( const item of items )
				{
					if( !food.items.has( item ) )
					{
						items.delete( item );
					}
				}
			}
		}
	}

	const remainingItems = new Set( items );

	for( const items of allergens.values() )
	{
		for( const item of items )
		{
			remainingItems.delete( item );
		}
	}

	for( const food of input )
	{
		for( const item of food.items )
		{
			if( remainingItems.has( item ) )
			{
				part1++;
			}
		}
	}

	const fieldsItCanBe = [];

	for( const [ allergen, items ] of allergens )
	{
		fieldsItCanBe.push( {
			eliminated: false,
			allergen,
			items,
		} );
	}

	do
	{
		const field = fieldsItCanBe.find( f => !f.eliminated && f.items.size === 1 );

		if( typeof field === 'undefined' )
		{
			break;
		}

		field.eliminated = true;
		field.singularItem = field.items.values().next().value;

		for( const otherField of fieldsItCanBe )
		{
			if( !otherField.eliminated )
			{
				otherField.items.delete( field.singularItem );
			}
		}
	}
	while( true ); // eslint-disable-line

	part2 = fieldsItCanBe
		.sort( ( a, b ) => a.allergen > b.allergen ? 1 : -1 )
		.map( x => x.singularItem )
		.join( ',' );

	return [ part1, part2 ];
};
