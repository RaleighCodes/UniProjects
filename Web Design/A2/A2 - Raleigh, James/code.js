//James Raleigh
//10150801
//A2
//SENG 503
//H. Pereria


    // Removes all empty strings from an array
	// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/trim
    // https://stackoverflow.com/questions/35476948/remove-empty-or-whitespace-strings-from-array-javascript
    // https://stackoverflow.com/questions/281264/remove-empty-elements-from-an-array-in-javascript
	// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/filter

function lineFilter(strings) {
    "use strict";
	return strings.filter(function (string) {
        // if (string != "" && string != "\/W\") better way to do this, just gut all whitespace
        // interestingly, needs the if statement to hold the string.trim() conditional otherwise it doesn't cull 
        // lines that have spaces in them.
        if (string.trim()) 
            return string; 
    })
}

    // Change string array to lower case, use helper array to remove duplicate words. 
    // https://stackoverflow.com/questions/1960473/get-all-unique-values-in-an-array-remove-duplicates
function uniqueWordList(words) {
    "use strict";
	var uniqueWords = [];
    //for (let i=0; i < words.length; i++) HAHAHHAHAHAHA WHY WOULD YOU USE THIS BURN IT WITH FIRE
	for (let word of words) {
		var cleanedWord = word.toLowerCase();
		if(uniqueWords.indexOf(cleanedWord) === -1){
			uniqueWords.push(cleanedWord)
		}
	}
	return uniqueWords;
}


    // Simple loop thru the textLines to find the longest
    // https://stackoverflow.com/questions/2167602/optimum-way-to-compare-strings-in-javascript
    // https://docs.microsoft.com/en-us/scripting/javascript/reference/length-property-string-javascript
function maxLineLength(textLines){
	var maxLength = 0;
	for(var line of textLines){
		if(line.length > maxLength){
			maxLength = line.length
		}
	}
	return maxLength;
}

    // Get average word length
    // Sum the words together
    // Get the total number of words
    // Divide for solution
function averageWordLength(indWords){
    var wordSum = 0;
	var wordNum = indWords.length;
	for(var word of indWords){
		wordSum += word.length
	}
	return (wordSum/wordNum);
}

    // Get all palindromes in text
    // palisd = [w for w in uniqueWords if len(w) > 2 and w == w[::-1]]
    // https://medium.freecodecamp.org/two-ways-to-check-for-palindromes-in-javascript-64fea8191fd7?gi=8fd761d76342
    // https://codepen.io/jeffreypoland/pen/iuItG
function palindromeChecker(uniqueWords){
    // checks word length, splits, reverses, and joins to check if words are same once reversed. 
	var palindromes = [];
	for(var word of uniqueWords){
		if( word.length > 2 && word === word.split("").reverse().join("")){
			palindromes.push(word);
		} 
	}
	return palindromes;
}

    // calculate and sort words by length and alphabetization
	// https://www.w3schools.com/jsref/jsref_sort.asp
    // https://stackoverflow.com/questions/10630766/sort-an-array-based-on-the-length-of-each-element
function findLongest(uniqueWords){
	uniqueWords.sort(function(a,b){
		return b.length - a.length || (a > b ? 1 : -1);
	})
	longestWords = uniqueWords.slice(0,10);  
	return longestWords
}

    // Get most common words 
    // https://stackoverflow.com/questions/3579486/sort-a-javascript-array-by-frequency-and-then-filter-repeats
    // REF: Sort array of strings by word/phrase frequency in JavaScript - https://gist.github.com/trucy/3344398
    // http://pietschsoft.com/post/2015/09/05/JavaScript-Basics-How-to-create-a-Dictionary-with-KeyValue-pairs
function findMostFreq(indWords){
    var lowerCaseWords = []
	for(let word of indWords){
		var convertedWord = word.toLowerCase();
		lowerCaseWords.push(convertedWord)
	} 
	// Use a dictionary to pair all words with corresponding frequencies
    // If the word is in the dictionary already, increment counter. 
    // https://stackoverflow.com/questions/9396569/javascript-what-is-property-in-hasownproperty
	var dictionary = [];
	for(let word of lowerCaseWords){
	  	if(dictionary.hasOwnProperty(word)){
			dictionary[word]++;
	  	}
        else{
			dictionary[word] = 1;
	  	}
	}
	// Cast dictionary into 2d array, by (word, freq)
	var wordFreqDict = [];
	for(var word in dictionary){
	  	wordFreqDict.push([word, dictionary[word]]);
	}
	// Sort frequency and by alphabetically
    // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort
    // https://stackoverflow.com/questions/5435228/sort-an-array-with-arrays-in-it-by-string
	wordFreqDict.sort(function(a, b) {
	  	return b[1] - a[1] || (a > b ? 1 : -1);
	});
	
    // Reformat string appearance
    // Same as used in sol.py
	var wordAndFreq = [];
	for(let elem of wordFreqDict){
	  	string = elem[0] + "(" + elem[1] + ")"
	  	wordAndFreq.push(string);
	}
    // Slice to get top 10 words
    wordAndFreq = wordAndFreq.slice(0,10);
	return wordAndFreq; 
}

//Main function
function getStats(txt) {
        // Splits the lines according to new line char
    var txtLines = txt.split(/\n/);
	   // Splits words by whitespace, cleans the whitespace that is at the front of the words.
	var indWords = lineFilter(txt.split(/\W+/));
        // Runs a trim process to get rid of any white space or new line characters
	var nonEmptyLines = lineFilter(txtLines);
        // Turns everything into lower case and removes duplicates
	var uniqueWords = uniqueWordList(indWords);
	return {
		nChars: txt.length,
		nWords: indWords.length, 
		nLines: txtLines.length,
		nNonEmptyLines: nonEmptyLines.length,
		maxLineLength: maxLineLength(txtLines),
		averageWordLength: averageWordLength(indWords),
		palindromes: palindromeChecker(uniqueWords),
		longestWords: findLongest(uniqueWords),
        mostFrequentWords: findMostFreq(indWords)
    };
}
