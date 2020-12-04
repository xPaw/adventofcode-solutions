module.exports = ( input ) =>
{
	input = input.split( '\n\n' ).map( parse );

	function parse( document )
	{
		return document
			.split( /\s/ )
			.map( field => field.split( ':' ) );
	}

	const eyeColors = new Set( [ 'amb', 'blu', 'brn', 'gry', 'grn', 'hzl', 'oth' ] );
	const validators =
	{
		byr: value => value >= 1920 && value <= 2002,
		iyr: value => value >= 2010 && value <= 2020,
		eyr: value => value >= 2020 && value <= 2030,
		hcl: value => /^#[0-9a-f]{6}$/.test( value ),
		ecl: value => eyeColors.has( value ),
		pid: value => value.length === 9,
		cid: () => true,
		hgt: value =>
		{
			value = value.match( /^(?<height>[0-9]+)(?<unit>cm|in)$/ );

			if( !value )
			{
				return false;
			}

			switch( value.groups.unit )
			{
				case 'cm': return value.groups.height >= 150 && value.groups.height <= 193;
				case 'in': return value.groups.height >= 59 && value.groups.height <= 76;
			}

			return false;
		},
	};

	let part1 = 0;
	let part2 = 0;

	for( const document of input )
	{
		let fields = 0;
		let validationFailed = false;

		for( const [ key, value ] of document )
		{
			if( key === 'cid' )
			{
				continue;
			}

			fields++;

			if( !validators[ key ]( value ) )
			{
				validationFailed = true;
			}
		}

		if( fields === 7 )
		{
			part1++;

			if( !validationFailed )
			{
				part2++;
			}
		}
	}

	return [ part1, part2 ];
};
