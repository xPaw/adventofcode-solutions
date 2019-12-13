const IntCode = require( '../intcode.js' );

module.exports = ( input ) =>
{
	input = input
		.split( ',' )
		.map( x => +x );

	let part1 = 0;
	let part2 = 0;
	const phases = permutator( [ 0, 1, 2, 3, 4 ] );

	for( const phase of phases )
	{
		let signal = 0;

		for( let i = 0; i < 5; i++ )
		{
			const ioInput = [ phase[ i ], signal ];
			signal = new IntCode( input ).execute( ioInput ).pop();
		}

		if( part1 < signal )
		{
			part1 = signal;
		}
	}

	return [ part1, part2 ];
};

function permutator(inputArr)
{
	let result = [];

	const permute = (arr, m = []) =>
	{
		if (arr.length === 0)
		{
			result.push(m)
		}
		else
		{
			for (let i = 0; i < arr.length; i++)
			{
				let curr = arr.slice();
				let next = curr.splice(i, 1);
				permute(curr.slice(), m.concat(next))
			}
		}
	}

	permute(inputArr)

	return result;
}
