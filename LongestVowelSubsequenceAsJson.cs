using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class LongestVowelSubsequenceAsJsonSolution
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        
        if (words == null || words.Count == 0)
        {
            return JsonSerializer.Serialize(new List<object>());
        }

        
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        
        List<object> results = new List<object>();
        
        foreach (string word in words)
        {
            var longestVowelInfo = FindLongestVowelSequence(word, vowels);
            
            results.Add(new
            {
                word = word,
                sequence = longestVowelInfo.sequence,
                length = longestVowelInfo.length
            });
        }
        
        return JsonSerializer.Serialize(results);
    }
    
    private static (string sequence, int length) FindLongestVowelSequence(string word, HashSet<char> vowels)
    {
        
        if (string.IsNullOrEmpty(word))
        {
            return ("", 0);
        }
        
        string longestSequence = "";
        int maxLength = 0;
        
        string currentSequence = "";
        
        foreach (char c in word)
        {
            if (vowels.Contains(c))
            {
                
                currentSequence += c;
            }
            else
            {
                
                if (currentSequence.Length > maxLength)
                {
                    maxLength = currentSequence.Length;
                    longestSequence = currentSequence;
                }
                
                currentSequence = "";
            }
        }
        
        
        if (currentSequence.Length > maxLength)
        {
            maxLength = currentSequence.Length;
            longestSequence = currentSequence;
        }
        
        return (longestSequence, maxLength);
    }
}
