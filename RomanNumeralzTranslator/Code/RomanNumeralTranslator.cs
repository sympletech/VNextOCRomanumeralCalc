using System;
using System.Collections.Generic;

namespace RomanNumeralzTranslator
{
    public class RomanNumeralTranslator
    {
        private readonly static Dictionary<string, int> subtractors = new Dictionary<string, int>
        {
            {"IV", 4}, 
            {"IX", 9}, 
            {"XL", 40},
            {"XC", 90},
        };

        private static readonly Dictionary<char, int> lookupValues = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
        };

        public static int Translate(string roman)
        {
            roman = roman.ToUpperInvariant();

            int result = 0;

            foreach (var pair in subtractors)
            {
                if (roman.Contains(pair.Key))
                {
                    result += pair.Value;
                    roman = roman.Replace(pair.Key, "");
                }
            }

            foreach (var c in roman)
            {
                if (!lookupValues.ContainsKey(c))
                {
                    throw new ArgumentException(c + " not a valid Roman numeral");
                }
                result += lookupValues[c];
            }

            return result;
        }
    }
}