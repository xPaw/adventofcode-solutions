window.AdventOfCode.Day5 = function( input )
{
	input = input.split( '\n' );
	
	// It contains at least three vowels (aeiou only),
	// like aei, xazegov, or aeiouaeiouaeiou.
	var regexVowels = /[aeiou]/g;
	
	// It contains at least one letter that appears twice in a row,
	// like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
	var regexDoubleLetter = /(.)\1+/;
	
	// It does not contain the strings ab, cd, pq, or xy,
	// even if they are part of one of the other requirements.
	var regexNaughtyParts = /(ab|cd|pq|xy)/;
	
	// It contains a pair of any two letters that appears at least
	// twice in the string without overlapping, like xyxy (xy) or
	// aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
	var regexRepeat = /(..).*\1/;
	
	// It contains at least one letter which repeats with exactly one
	// letter between them, like xyx, abcdefeghi (efe), or even aaa.
	var regexSpaced = /(.).\1/;
	
	var PartOneFilter = function( word )
	{
		var match = word.match( regexVowels );
		
		return (match !== null && match.length >= 3
			&& regexDoubleLetter.test( word )
			&& !regexNaughtyParts.test( word ));
	};
	
	var PartTwoFilter = function( word )
	{
		return regexRepeat.test( word ) && regexSpaced.test( word );
	};
	
	return [ input.filter( PartOneFilter ).length, input.filter( PartTwoFilter ).length ];
};
