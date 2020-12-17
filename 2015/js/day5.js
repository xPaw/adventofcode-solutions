window.AdventOfCode.Day5 = function( input )
{
	input = input.split( '\n' );

	// It contains at least three vowels (aeiou only),
	// like aei, xazegov, or aeiouaeiouaeiou.
	const regexVowels = /[aeiou]/g;

	// It contains at least one letter that appears twice in a row,
	// like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
	const regexDoubleLetter = /(.)\1+/;

	// It does not contain the strings ab, cd, pq, or xy,
	// even if they are part of one of the other requirements.
	const regexNaughtyParts = /(ab|cd|pq|xy)/;

	// It contains a pair of any two letters that appears at least
	// twice in the string without overlapping, like xyxy (xy) or
	// aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
	const regexRepeat = /(..).*\1/;

	// It contains at least one letter which repeats with exactly one
	// letter between them, like xyx, abcdefeghi (efe), or even aaa.
	const regexSpaced = /(.).\1/;

	const PartOneFilter = function( word )
	{
		const match = word.match( regexVowels );

		return ( match !== null && match.length >= 3
			&& regexDoubleLetter.test( word )
			&& !regexNaughtyParts.test( word ) );
	};

	const PartTwoFilter = function( word )
	{
		return regexRepeat.test( word ) && regexSpaced.test( word );
	};

	return [ input.filter( PartOneFilter ).length, input.filter( PartTwoFilter ).length ];
};
