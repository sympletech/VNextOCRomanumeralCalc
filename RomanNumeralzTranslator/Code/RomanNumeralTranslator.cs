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
                    //result += pair.Value;
                    roman = roman.Replace(pair.Key, ";" + pair.Value);
                }
            }

            foreach (var c in roman)
            {
                if (lookupValues.ContainsKey(c))
                {
                    roman = roman.Replace(c.ToString(), ";" + lookupValues[c]);
                }
            }

            string[] tokens = roman.Split(';');
            ThrowOnInvalidToken(tokens[0]);
            int lastToken = int.MaxValue;

            for (int i = 1; i < tokens.Length; i++)
            {
                var token = tokens[i];

                int j = 0;
                if (int.TryParse(token, out j))
                {
                    if (j > lastToken )
                    {
                        throw new ArgumentException("Invalid");
                    }
                    bool b = IsPowerofTen(j);
                    if (b && j == lastToken)
                    {
                        throw new ArgumentException("Invalid");
                    }
                    lastToken = j;
                    result += j;

                }
                else
                {
                    ThrowOnInvalidToken(token);
                }
            }
            return result;
        }
  
        private static bool IsPowerofTen(int j)
        {
            double baseCheck = Math.Log10(j);
            bool b = baseCheck != Math.Floor(baseCheck);
            return b;
        }
  
        private static void ThrowOnInvalidToken(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(string.Format("{0} not a valid Roman numeral", token));
            }
        }
    }
}