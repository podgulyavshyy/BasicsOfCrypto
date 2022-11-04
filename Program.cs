Decrypt("otpfile1.txt");

int Decrypt(string filepath){
	var text = File.ReadAllText(filepath);
	var letterFrequencyDict = new SortedDictionary<char, int>();
	var totalLetters = 0;
	foreach (var c in text.Where(c => !char.IsPunctuation(c) && c != '\0' && c != ' ' && c != '\n'))
	{
		if (!letterFrequencyDict.ContainsKey(c)) { letterFrequencyDict.Add(c, 1);}
		else { letterFrequencyDict[c]++;}
		totalLetters++;
	}

	Dictionary<char, double> toCompare = new Dictionary<char, double>
	{

		{'e', 11.1607},
		{'a', 8.4966},
		{'r', 7.5809},
		{'i', 7.5448},
		{'o', 7.1635},
		{'t', 6.9509},
		{'n', 6.6544},
		{'s', 5.7351},
		{'l', 5.4893},
		{'c', 4.5388},
		{'u', 3.6308},
		{'d', 3.3844},
		{'p', 3.3844},
		{'m', 3.0129},
		{'h', 3.0034},
		{'g', 2.4705},
		{'b', 2.0720},
		{'f', 1.8121},
		{'y', 1.7779},
		{'w', 1.2899},
		{'k', 1.1016},
		{'v', 1.0074},
		{'x', 0.2902},
		{'z', 0.2722},
		{'j', 0.1965},
		{'q', 0.1962}
	};

	foreach (var pair in toCompare)
	{
		toCompare[pair.Key] = pair.Value * totalLetters / 100;
	}
 
	var keyDifferenceDict = new SortedDictionary<int,int>();
	for (var i = 1; i <= 26; i++)
	{
		keyDifferenceDict.Add(i,Analyze(DecryptDictionary(i, letterFrequencyDict),toCompare));
	}
	var answer1 = keyDifferenceDict.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
	var answer = answer1;
	Console.WriteLine("first expected key: " + answer + " with difference = " + keyDifferenceDict[answer]);
	keyDifferenceDict.Remove(answer);
	answer = keyDifferenceDict.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
	Console.WriteLine("second expected key: " + answer + " with difference = " + keyDifferenceDict[answer]);
	keyDifferenceDict.Remove(answer);
	answer = keyDifferenceDict.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
	Console.WriteLine("third expected key: " + answer + " with difference = " + keyDifferenceDict[answer]);
	keyDifferenceDict.Remove(answer);
	return answer1;
}


SortedDictionary<char,int> DecryptDictionary(int key, SortedDictionary<char, int> letterFrequencyDict)
{
	var shiftedLetterFreqDict = new SortedDictionary<char,int>();
	foreach (var pair in letterFrequencyDict)
	{
		shiftedLetterFreqDict.Add(DecryptChar(pair.Key, key), pair.Value);
	}

	return shiftedLetterFreqDict;
}

char DecryptChar(char a, int key){
	const string albet = "abcdefghijklmnopqrstuvwxyz";
	var i = 0;
	while(a != albet[i]){
		i++;
	}
	key = i - key;
	while(key < 0){
		key += 25;
	}
	while(key > 25){
		key -= 25;
	}
	return albet[key];
}

int Analyze(SortedDictionary<char, int> dictionary, Dictionary<char, double> toCompare)
{
	var diff = 0;
	foreach (var pair in dictionary)
	{
		diff += Math.Abs(pair.Value - (int)Math.Floor(toCompare[pair.Key]));
	}
 
	return diff;
}