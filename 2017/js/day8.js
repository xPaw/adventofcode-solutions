window.AdventOfCode.Day8 = function( input )
{
	input = input.split( /\n/ );

	const Token =
	{
		Variable: 0,
		Modifier: 1,
		Value: 2,
		ConditionVariable: 4,
		Condition: 5,
		ConditionValue: 6,
	};

	const registers = {};
	let part2 = 0;

	const IsConditionSatisfied = ( variable, condition, value ) =>
	{
		if( condition === '>'  )return variable > value;
		if( condition === '<'  )return variable < value;
		if( condition === '>=' )return variable >= value;
		if( condition === '<=' )return variable <= value;
		if( condition === '==' )return variable == value;
		if( condition === '!=' )return variable != value;

	};

	input.forEach( ( line ) =>
	{
		line = line.split( ' ' );

		if( IsConditionSatisfied(
			registers[ line[ Token.ConditionVariable ] ] || 0,
			line[ Token.Condition ],
			line[ Token.ConditionValue ]
		) )
		{
			const variable = line[ Token.Variable ];
			let value = registers[ variable ] || 0;

			if( line[ Token.Modifier ] === 'inc' )
			{
				value += +line[ Token.Value ];
			}
			else
			{
				value -= +line[ Token.Value ];
			}

			if( part2 < value )
			{
				part2 = value;
			}

			registers[ variable ] = value;
		}
	} );

	return [ Math.max( ...Object.values( registers ) ), part2 ];
};
