using System;
using System.Text;
using Random = UnityEngine.Random;

namespace Util
{
    public static class NameGenerator
    {
        private static readonly string[] consonants = { 
            "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", 
            "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        static readonly string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        
        public static string Generate()
        {
            var name = new StringBuilder();
            var nameLength = Random.Range(3, 6);
            for (var i = 0; i < nameLength; i+=2)
            {
                name.Append(consonants[Random.Range(0, consonants.Length)]);
                name.Append(vowels[Random.Range(0, vowels.Length)]);
            }

            name[0] = Char.ToUpper(name[0]);
            
            return name.ToString();
        }
    }
}