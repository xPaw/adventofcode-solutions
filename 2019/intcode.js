module.exports = class IntCode
{
	powers = [ 0, 100, 1000, 10000 ]; // Math.pow is slow
	memory;
	halted = false;
	base = 0;
	cursor = 0;

	constructor( input )
	{
		this.memory = [ ...input ]; // make a copy
	}

	getAddress( n )
	{
		const mode = ~~( this.memory[ this.cursor ] / this.powers[ n ] ) % 10;
		let offset = this.cursor + n;

		switch( mode )
		{
			case 0: offset = this.memory[ offset ]; break;
			//case 1: offset = offset; break;
			case 2: offset = this.base + this.memory[ offset ]; break;
		}

		return offset;
	}

	getValue( n )
	{
		return this.memory[ this.getAddress( n ) ] || 0;
	}

	execute( ioInput )
	{
		let output = [];

	halt:
		while( true )
		{
			const opcode = this.memory[ this.cursor ] % 100;

			switch( opcode )
			{
				case 1: // add
				{
					this.memory[ this.getAddress( 3 ) ] = this.getValue( 1 ) + this.getValue( 2 );
					this.cursor += 4;

					break;
				}
				case 2: // multiply
				{
					this.memory[ this.getAddress( 3 ) ] = this.getValue( 1 ) * this.getValue( 2 );
					this.cursor += 4;

					break;
				}
				case 3: // input
				{
					if( !ioInput.length )
					{
						break halt;
					}

					this.memory[ this.getAddress( 1 ) ] = ioInput.shift();
					this.cursor += 2;

					break;
				}
				case 4: // output
				{
					output.push( this.getValue( 1 ) );
					this.cursor += 2;

					break;
				}
				case 5: // jump-if-true
				{
					if( this.getValue( 1 ) !== 0 )
					{
						this.cursor = this.getValue( 2 );
					}
					else
					{
						this.cursor += 3;
					}

					break;
				}
				case 6: // jump-if-false
				{
					if( this.getValue( 1 ) === 0 )
					{
						this.cursor = this.getValue( 2 );
					}
					else
					{
						this.cursor += 3;
					}

					break;
				}
				case 7: // less than
				{
					this.memory[ this.getAddress( 3 ) ] = this.getValue( 1 ) < this.getValue( 2 ) ? 1 : 0;
					this.cursor += 4;

					break;
				}
				case 8: // equals
				{
					this.memory[ this.getAddress( 3 ) ] = this.getValue( 1 ) === this.getValue( 2 ) ? 1 : 0;
					this.cursor += 4;

					break;
				}
				case 9: // modify relative base
				{
					this.base += this.getValue( 1 );
					this.cursor += 2;

					break;
				}
				case 99:
				{
					this.halted = true;
					break halt;
				}
			}
		}

		return output;
	}
}
