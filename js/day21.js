window.AdventOfCode.Day21 = function( input )
{
	'use strict';
	
	class Entity
	{
		constructor( health, damage, armor )
		{
			this.MaxHealth = this.Health = health;
			this.DamagePoints = damage;
			this.ArmorPoints = armor;
		}
		
		get IsAlive()
		{
			return this.Health > 0;
		}
		
		Reset()
		{
			this.Health = this.MaxHealth;
		}
		
		Attack( entity )
		{
			entity.TakeDamage( this.DamagePoints );
		}
		
		TakeDamage( damage )
		{
			// Damage dealt by an attacker each turn is equal to the
			// attacker's damage score minus the defender's armor score.
			damage -= this.ArmorPoints;
			
			// An attacker always does at least 1 damage.
			if( damage < 1 )
			{
				damage = 1;
			}
			
			this.Health -= damage;
		}
	}
	
	class Player extends Entity
	{
		constructor()
		{
			super( 100, 0, 0 );
			
			this.Weapon = null;
			this.Armor = null;
			this.RingLeft = null;
			this.RingRight = null;
		}
		
		get GoldSpent()
		{
			var gold = 0;
			
			if( this.Weapon )
			{
				gold += this.Weapon.Cost;
			}
			
			if( this.Armor )
			{
				gold += this.Armor.Cost;
			}
			
			if( this.RingLeft )
			{
				gold += this.RingLeft.Cost;
			}
			
			if( this.RingRight )
			{
				gold += this.RingRight.Cost;
			}
			
			return gold;
		}
		
		Reset()
		{
			super.Reset();
			
			this.RecalculateStats();
		}
		
		RecalculateStats()
		{
			this.DamagePoints = 0;
			this.ArmorPoints = 0;
			
			if( this.Weapon )
			{
				this.DamagePoints += this.Weapon.Damage;
			}
			
			if( this.Armor )
			{
				this.ArmorPoints += this.Armor.Armor;
			}
			
			if( this.RingLeft )
			{
				this.DamagePoints += this.RingLeft.Damage;
				this.ArmorPoints += this.RingLeft.Armor;
			}
			
			if( this.RingRight )
			{
				this.DamagePoints += this.RingRight.Damage;
				this.ArmorPoints += this.RingRight.Armor;
			}
		}
	}
	
	class Boss extends Entity
	{
		
	}
	
	class Item
	{
		constructor( name, cost, damage, armor )
		{
			//this.Name = name;
			this.Cost = cost;
			this.Damage = damage;
			this.Armor = armor;
		}
	}
	
	class Shop
	{
		constructor()
		{
			this.Weapons =
			[
				new Item( 'Dagger'    ,  8, 4, 0 ),
				new Item( 'Shortsword', 10, 5, 0 ),
				new Item( 'Warhammer' , 25, 6, 0 ),
				new Item( 'Longsword' , 40, 7, 0 ),
				new Item( 'Greataxe'  , 74, 8, 0 ),
			];
			
			this.Armor =
			[
				null, // Armor is optional, stored in shop for easier iteration
				new Item( 'Leather'   ,  13, 0, 1 ),
				new Item( 'Chainmail' ,  31, 0, 2 ),
				new Item( 'Splintmail',  53, 0, 3 ),
				new Item( 'Bandedmail',  75, 0, 4 ),
				new Item( 'Platemail' , 102, 0, 5 ),
			];
			
			this.Rings =
			[
				null, // Rings are optional, stored in shop for easier iteration
				new Item( 'Damage +1 ',  25, 1, 0 ),
				new Item( 'Damage +2 ',  50, 2, 0 ),
				new Item( 'Damage +3 ', 100, 3, 0 ),
				new Item( 'Defense +1',  20, 0, 1 ),
				new Item( 'Defense +2',  40, 0, 2 ),
				new Item( 'Defense +3',  80, 0, 3 ),
			];
		}
	}
	
	class Game
	{
		constructor( input )
		{
			this.Shop = new Shop();
			this.Player = new Player();
			this.Boss = new Boss( +input[ 0 ], +input[ 1 ], +input[ 2 ] );
			
			this.CheapestWin = Number.MAX_VALUE;
			this.CostlyLoss = 0;
		}
		
		Simulate()
		{
			var i = 0;
			
			for( let weapon of this.Shop.Weapons )
			{
				this.Player.Weapon = weapon;
				
				for( let armor of this.Shop.Armor )
				{
					this.Player.Armor = armor;
					
					for( let ringLeft of this.Shop.Rings )
					{
						this.Player.RingLeft = ringLeft;
						
						for( let ringRight of this.Shop.Rings )
						{
							// Can't buy two rings of same type
							if( ringLeft === ringRight )
							{
								continue;
							}
							
							this.Player.RingRight = ringRight;
							
							this.Battle();
						}
					}
				}
			}
		}
		
		Battle()
		{
			this.Player.Reset();
			this.Boss.Reset();
			
			var turn = 0;
			
			while( this.Player.IsAlive && this.Boss.IsAlive )
			{
				if( ++turn % 2 === 0 )
				{
					this.Boss.Attack( this.Player );
				}
				else
				{
					this.Player.Attack( this.Boss );
				}
			}
			
			if( this.Player.IsAlive && this.CheapestWin > this.Player.GoldSpent )
			{
				this.CheapestWin = this.Player.GoldSpent;
			}
			
			if( this.Boss.IsAlive && this.CostlyLoss < this.Player.GoldSpent )
			{
				this.CostlyLoss = this.Player.GoldSpent;
			}
		}
	}
	
	var game = new Game( input.match( /([0-9]+)/g ) );
	
	game.Simulate();
	
	return [ game.CheapestWin, game.CostlyLoss ];
};
