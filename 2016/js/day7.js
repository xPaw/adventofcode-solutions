window.AdventOfCode.Day7 = function( input )
{
	input = input.split( '\n' );

	function HasAbba( message )
	{
		for( let i = 0; i < message.length - 3; i++ )
		{
			if( message[ i ] !== message[ i + 1 ]
			&&  message[ i ] === message[ i + 3 ]
			&&  message[ i + 1 ] === message[ i + 2 ] )
			{
				return true;
			}
		}

		return false;
	}

	function GetBabs( message )
	{
		const babs = [];

		for( let i = 0; i < message.length - 2; i++ )
		{
			if( message[ i ] !== message[ i + 1 ]
			&&  message[ i ] === message[ i + 2 ] )
			{
				babs.push( message[ i + 1 ] + message[ i ] + message[ i + 1 ] );
			}
		}

		return babs;
	}

	let supportsTLS = 0;
	let supportsSSL = 0;

	for( let address of input )
	{
		address += "]"; // To process the ending part

		let part = "";
		let isValidTLS = true;
		let hasAbba = false;
		let insideBracket = false;
		let babsToLookFor = [];
		const hypernets = [];

		for( const char of address )
		{
			if( char === '[' || char === ']' )
			{
				if( HasAbba( part ) )
				{
					if( insideBracket )
					{
						isValidTLS = false;
					}

					hasAbba = true;
				}

				if( insideBracket )
				{
					hypernets.push( part );
				}
				else
				{
					babsToLookFor = babsToLookFor.concat( GetBabs( part ) );
				}

				part = "";
				insideBracket = char === '[';
			}
			else
			{
				part += char;
			}
		}

		if( hasAbba && isValidTLS )
		{
			supportsTLS++;
		}

		hypernetLoop:
		for( const hypernet of hypernets )
		{
			for( const bab of babsToLookFor )
			{
				if( hypernet.includes( bab ) )
				{
					supportsSSL++;
					break hypernetLoop;
				}
			}
		}
	}

	return [ supportsTLS, supportsSSL ];
};
