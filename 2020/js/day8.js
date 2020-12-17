module.exports = ( input ) =>
{
	input = input
		.split( '\n' )
		.map( x =>
		{
			const instruction = x.match( /^(?<ins>[a-z]+) (?<num>[-+][0-9]+)/ ).groups;
			instruction.num = parseInt( instruction.num, 10 );
			instruction.seen = false;

			return instruction;
		} );

	function run()
	{
		let cursor = 0;
		let accumulator = 0;

		while( cursor < input.length )
		{
			const instruction = input[ cursor ];

			if( instruction.seen )
			{
				return {
					infiniteLoop: true,
					accumulator,
				};
			}

			instruction.seen = true;

			switch( instruction.ins )
			{
				case 'nop': cursor++; break;
				case 'acc': cursor++; accumulator += instruction.num; break;
				case 'jmp': cursor += instruction.num; break;
			}
		}

		return {
			infiniteLoop: false,
			accumulator,
		};
	}

	const part1 = run().accumulator;
	let part2 = 0;

	outer:
	for( let i = input.length - 1; i >= 0; i-- )
	{
		input.forEach( x => x.seen = false );

		const originalInstruction = input[ i ].ins;

		switch( originalInstruction )
		{
			case 'acc': continue outer;
			case 'nop': input[ i ].ins = 'jmp'; break;
			case 'jmp': input[ i ].ins = 'nop'; break;
		}

		const result = run();

		if( !result.infiniteLoop )
		{
			part2 = result.accumulator;
			break;
		}

		input[ i ].ins = originalInstruction;
	}

	return [ part1, part2 ];
};
