module.exports = ( input ) =>
{
	const MATCH_STRING = 0;
	const MATCH_RECURSE = 1;
	const rules = [];
	let part1 = 0;
	let part2 = 0;

	input = input.split( '\n\n' );

	function parseRule( line )
	{
		const [ ruleId, rulesToMatch ] = line.split( ': ', 2 );

		if( rulesToMatch[ 0 ] === '"' )
		{
			rules[ ruleId ] =
			{
				type: MATCH_STRING,
				match: rulesToMatch[ 1 ],
			};
			return;
		}

		const ruleLists = [];

		for( const matches of rulesToMatch.split( ' | ' ) )
		{
			ruleLists.push( matches.split( ' ' ).map( n => parseInt( n, 10 ) ) );
		}

		rules[ ruleId ] =
		{
			type: MATCH_RECURSE,
			rules: ruleLists,
		};
	}

	for( const line of input[ 0 ].split( '\n' ) )
	{
		parseRule( line );
	}

	input = input[ 1 ];

	function recurseRules( ruleId, r11hack = 0 )
	{
		const rule = rules[ ruleId ];

		if( rule.type !== MATCH_RECURSE )
		{
			return rule.match;
		}

		const r = [];

		for( const subRules of rule.rules )
		{
			const t = [];

			for( const subRuleId of subRules )
			{
				// Part2 recursion hack
				if( part1 > 0 && subRuleId === 11 && ++r11hack === 10 )
				{
					return '';
				}

				t.push( recurseRules( subRuleId, r11hack ) );
			}

			r.push( t.join( '' ) );
		}

		return '(?:' + r.join( '|' ) + ')';
	}

	part1 = input.match( new RegExp( '^' + recurseRules( 0 ) + '$', 'gm' ) ).length;

	//
	// Part 2
	//

	// parseRule( '8: 42 | 42 8' );
	rules[ 8 ] =
	{
		type: MATCH_STRING,
		match: recurseRules( 42 ) + '+',
	};

	parseRule( '11: 42 31 | 42 11 31' );

	part2 = input.match( new RegExp( '^' + recurseRules( 0 ) + '$', 'gm' ) ).length;

	return [ part1, part2 ];
};
