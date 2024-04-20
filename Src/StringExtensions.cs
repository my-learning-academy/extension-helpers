namespace extension.helpers
{
    public static partial class StringExtensions
    {
        /*
             <summary>
              Counts the number of words in the source string
             </summary>

             <param name="sourceString">The string to count words in.</param>
             <returns>The word count. If the source string is null, empty, or consists only of white-space characters, returns 0.</returns>
         */
        public static int WordCount(this string? sourceString)
        {
            if (string.IsNullOrWhiteSpace(sourceString))
            {
                return 0;
            }
            return sourceString.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /*
             <summary>
              Returns a collection of matches found in the source string
             </summary>

             <param name="sourceString">The string to search for matches.</param>
             <param name="regexPattern">The regular expression pattern to match. If this is null, 
                the method will determine whether to use Unicode or ASCII word matching based on the content of the source string.</param>

             <returns>A MatchCollection containing all matches found. 
                      If a regexPattern is provided, it will return the matches for that pattern. Otherwise, 
                      it will return matches for either Unicode or ASCII words, depending on the content of the source string.

                     * words("fred, barney, & pebbles")
                     * // => ["fred", "barney", "pebbles"]
                 
                     * words("fred, barney, & pebbles", "[^, ]+"")
                     * // => ["fred", "barney", "&", "pebbles"]
             </returns>
        */
        public static MatchCollection Words(this string sourceString, Regex? regexPattern)
        {
            if (regexPattern is null)
            {
                var result = HasUnicodeWord(sourceString) ? UnicodeWords(sourceString) : AsciiWords(sourceString);
                return result;
            }
            return regexPattern.Matches(sourceString);
        }

        /*
             <summary>
              This method returns a collection of ASCII word matches found in the input string
             </summary>

            <param name="input">The string to search for ASCII word matches.</param>

             <returns>
                A MatchCollection containing all ASCII word matches found in the input string.
             </returns>
         */
        public static MatchCollection AsciiWords(this string sourceString)
        {
            var regex = new Regex(RegularExpressionExtensions.asciiCharacterExp);
            return regex.Matches(sourceString);
        }

        public static MatchCollection UnicodeWords(this string inputString) => RegularExpressionExtensions.unicodeWordsRegex.Matches(inputString);

        public static bool HasUnicodeWord(this string input)
        {
            var regex = new Regex(RegularExpressionExtensions.characters);
            return regex.IsMatch(input);
        }

        public static void ToCamelCase(this string? sourceString)
        {
            if (string.IsNullOrWhiteSpace(sourceString))
            {
                return;
            }
        }
    }
}
