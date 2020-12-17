// https://github.com/dankogai/js-combinatorics
/* eslint-disable */
(function(h,l){"function"===typeof define&&define.amd?define([],l):"object"===typeof exports?module.exports=l():h.Combinatorics=l()})(this,function(){var h=function(b,a){var c,d=1;b<a&&(c=b,b=a,a=c);for(;a--;)d*=b--;return d},l=function(b,a){return h(b,a)/h(a,a)},t=function(b){return h(b,b)},u=function(b,a){var c=1;if(a)c=t(a);else{for(a=1;c<b;c*=++a);c>b&&(c/=a--)}for(var d=[0];a;c/=a--)d[a]=Math.floor(b/c),b%=c;return d},f=function(b,a){Object.keys(a).forEach(function(c){Object.defineProperty(b,
c,{value:a[c]})})},g=function(b,a){Object.defineProperty(b,a,{writable:!0})},n=function(b){var a,c=[];for(this.init();a=this.next();)c.push(b?b(a):a);this.init();return c},m={toArray:n,map:n,forEach:function(b){var a;for(this.init();a=this.next();)b(a);this.init()},filter:function(b){var a,c=[];for(this.init();a=this.next();)b(a)&&c.push(a);this.init();return c}},q=function(b,a,c){a||(a=b.length);if(1>a)throw new RangeError;if(a>b.length)throw new RangeError;var d=(1<<a)-1,e=l(b.length,a),p=1<<b.length;
a=function(){return e};b=Object.create(b.slice(),{length:{get:a}});g(b,"index");f(b,{valueOf:a,init:function(){this.index=d},next:function(){if(!(this.index>=p)){for(var b=0,a=this.index,c=[];a;a>>>=1,b++)a&1&&c.push(this[b]);a=this.index;b=a&-a;a+=b;this.index=a|((a&-a)/b>>1)-1;return c}}});f(b,m);b.init();return"function"===typeof c?b.map(c):b},r=function(b){b=b.slice();var a=t(b.length);b.index=0;b.next=function(){if(!(this.index>=a)){for(var b=this.slice(),d=u(this.index,this.length),e=[],p=this.length-
1;0<=p;--p)e.push(b.splice(d[p],1)[0]);this.index++;return e}};return b},v=function(b){for(var a=0,c=1;c<=b;c++)var d=h(b,c),a=a+d;return a},w=Array.prototype.slice,n=Object.create(null);f(n,{C:l,P:h,factorial:t,factoradic:u,cartesianProduct:function(){if(!arguments.length)throw new RangeError;var b=w.call(arguments),a=b.reduce(function(b,a){return b*a.length},1),c=function(){return a},d=b.length,b=Object.create(b,{length:{get:c}});if(!a)throw new RangeError;g(b,"index");f(b,{valueOf:c,dim:d,init:function(){this.index=
0},get:function(){if(arguments.length===this.length){for(var b=[],a=0;a<d;a++){var c=arguments[a];if(c>=this[a].length)return;b.push(this[a][c])}return b}},nth:function(a){for(var b=[],c=0;c<d;c++){var f=this[c].length,g=a%f;b.push(this[c][g]);a-=g;a/=f}return b},next:function(){if(!(this.index>=a)){var b=this.nth(this.index);this.index++;return b}}});f(b,m);b.init();return b},combination:q,permutation:function(b,a,c){a||(a=b.length);if(1>a)throw new RangeError;if(a>b.length)throw new RangeError;
var d=h(b.length,a),e=Object.create(b.slice(),{length:{get:function(){return d}}});g(e,"cmb");g(e,"per");f(e,{valueOf:function(){return d},init:function(){this.cmb=q(b,a);this.per=r(this.cmb.next())},next:function(){var a=this.per.next();if(!a){a=this.cmb.next();if(!a)return;this.per=r(a);return this.next()}return a}});f(e,m);e.init();return"function"===typeof c?e.map(c):e},permutationCombination:function(b,a){var c=v(b.length),d=Object.create(b.slice(),{length:{get:function(){return c}}});g(d,"cmb");
g(d,"per");g(d,"nelem");f(d,{valueOf:function(){return c},init:function(){this.nelem=1;this.cmb=q(b,this.nelem);this.per=r(this.cmb.next())},next:function(){var a=this.per.next();if(!a){a=this.cmb.next();if(!a){this.nelem++;if(this.nelem>b.length)return;this.cmb=q(b,this.nelem);a=this.cmb.next();if(!a)return}this.per=r(a);return this.next()}return a}});f(d,m);d.init();return"function"===typeof a?d.map(a):d},power:function(b,a){var c=1<<b.length,d=function(){return c},e=Object.create(b.slice(),{length:{get:d}});
g(e,"index");f(e,{valueOf:d,init:function(){e.index=0},nth:function(a){if(!(a>=c)){for(var b=0,d=[];a;a>>>=1,b++)a&1&&d.push(this[b]);return d}},next:function(){return this.nth(this.index++)}});f(e,m);e.init();return"function"===typeof a?e.map(a):e},baseN:function(b,a,c){a||(a=b.length);if(1>a)throw new RangeError;var d=b.length,e=Math.pow(d,a),h=function(){return e},k=Object.create(b.slice(),{length:{get:h}});g(k,"index");f(k,{valueOf:h,init:function(){k.index=0},nth:function(c){if(!(c>=e)){for(var f=
[],g=0;g<a;g++){var h=c%d;f.push(b[h]);c-=h;c/=d}return f}},next:function(){return this.nth(this.index++)}});f(k,m);k.init();return"function"===typeof c?k.map(c):k},VERSION:"0.5.0"});return n});
/* eslint-enable */
/* globals Combinatorics */

window.AdventOfCode.Day17 = function( input )
{
	input = input.split( '\n' ).map( Number );

	let smallestAmountRequired = Number.MAX_VALUE;
	const partOne = Combinatorics.power( input ).filter( function( list )
	{
		const sum = list.reduce( function( a, b )
		{
			return a + b;
		}, 0 );

		if( sum === 150 )
		{
			if( smallestAmountRequired > list.length )
			{
				smallestAmountRequired = list.length;
			}

			return true;
		}

		return false;
	} );

	const partTwo = partOne.filter( function( a )
	{
		return a.length === smallestAmountRequired;
	} );

	return [ partOne.length, partTwo.length ];
};
