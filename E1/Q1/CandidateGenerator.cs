using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public static class CandidateGenerator
    {
        public static readonly char[] Alphabet =
            Enumerable.Range('a', 'z' - 'a' + 1)
                      .Select(c => (char)c)
                      .ToArray();

        public static string[] GetCandidates(string word)
        {
            List<string> candidates = new List<string>();
            for (int i = 0; i <= word.Length; i++)
            {
                for (int j = 0; j < Alphabet.Length; j++)
                {
                    candidates.Add(Insert(word, i, Alphabet[j]));
                }
            }
            for (int i = 0; i < word.Length; i++)
            {
                for (int j = 0; j < Alphabet.Length; j++)
                {
                    candidates.Add(Substitute(word, i, Alphabet[j]));
                }
            }
            for (int i = 0; i < word.Length; i++)
            {
                candidates.Add(Delete(word, i));
            }
            return candidates.ToArray();
        }

        public static string Insert(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length+1];
            for (int i = 0, j = 0; i < wordChars.Length; i++, j++)
            {
                if (i == pos)
                {
                    newWord[pos] = c;
                    j++;
                }
                newWord[j] = wordChars[i];
            }
            if (pos == wordChars.Length)
                newWord[wordChars.Length] = c;
            return new string(newWord);
        }

        public static string Delete(string word, int pos)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length-1];
            for (int i = 0, j = 0; j < newWord.Length; i++, j++)
            {
                if (i == pos)
                    i++;
                newWord[j] = wordChars[i];
            }
            return new string(newWord);
        }

        public static string Substitute(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length];
            for (int i = 0; i < newWord.Length; i++)
            {
                if (i == pos)
                    newWord[pos] = c;
                else
                    newWord[i] = wordChars[i];
            }
            return new string(newWord);
        }

    }
}
