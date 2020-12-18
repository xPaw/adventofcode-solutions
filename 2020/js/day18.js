module.exports = ( input ) =>
{
	input = input.split( '\n' );//.map( l => l.replaceAll( ' ', '' ) );

	function createEval1( expr )
	{
		const newExpr = expr.replace( /(\d+)/g, '$1)' );
		return '('.repeat( newExpr.match( /\)/g ).length ) + newExpr;
	}

	function createEval2( expr )
	{
		return '((' + expr
			.replaceAll( '(', '(((' )
			.replaceAll( ')', ')))' )
			.replaceAll( '+', ')+(' )
			.replaceAll( '*', '))*((' )
		+ '))';
	}

	let part1 = 0;

	for( let line of input )
	{
		do
		{
			const parenthesis = /\((?<expr>[^()]+)\)/.exec( line );

			if( !parenthesis )
			{
				break;
			}

			line =
				line.substr( 0, parenthesis.index ) +
				eval( createEval1( parenthesis.groups.expr ) ) +
				line.substr( parenthesis.index + parenthesis.groups.expr.length + 2 );
		}
		while( true ); // eslint-disable-line

		part1 += eval( createEval1( line ) );
	}

	const p2expr = [];

	for( const expr of input )
	{
		p2expr.push( createEval2( expr ) );
	}

	const part2 = eval( p2expr.join( '+' ) );

	return [ part1, part2 ];
};
