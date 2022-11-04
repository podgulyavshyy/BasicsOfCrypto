string text = File.ReadAllText("otpfile1.txt");
var letterFrequencyDict = new SortedDictionary<char, int>();
foreach (var c in text.Where(c => !char.IsPunctuation(c) && c != '\0' && c != ' '))
{
    if (!letterFrequencyDict.ContainsKey(c))
    {
        letterFrequencyDict.Add(c, 1);
    }
    else
    {
        letterFrequencyDict[c]++;
    }
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

Decrypt(3, toCompare);

SortedDictionary<char, int> Decrypt(int key, Dictionary<char, double> letterFrequencyDict)
{
    var shiftedLetterFreqDict = new SortedDictionary<char, int>();
    foreach (var pair in letterFrequencyDict)
    {
        const string albet = "abcdefghijklmnopqrstuvwxyz";
        int i = 0;
        while (pair.Key != albet[i])
        {
            i++;
        }

        key = i - key;
        while (key < 0)
        {
            key += 25;
        }

        while (key > 25)
        {
            key -= 25;
        }

        shiftedLetterFreqDict.Add(albet[key], (int) pair.Value);
    }

    return shiftedLetterFreqDict;
}