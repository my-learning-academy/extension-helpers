namespace extension.helpers
{
    public static partial class RegularExpressionExtensions
    {

        // Used to compose unicode character classes.
        static readonly string astralRange = "\\ud800-\\udfff";
        static readonly string comboMarksRange = "\\u0300-\\u036f";
        static readonly string comboHalfMarksRange = "\\ufe20-\\ufe2f";
        static readonly string comboSymbolsRange = "\\u20d0-\\u20ff";
        static readonly string comboMarksExtendedRange = "\\u1ab0-\\u1aff";
        static readonly string comboMarksSupplementRange = "\\u1dc0-\\u1dff";
        static readonly string comboRange = comboMarksRange + comboHalfMarksRange + comboSymbolsRange + comboMarksExtendedRange + comboMarksSupplementRange;
        static readonly string dingbatRange = "\\u2700-\\u27bf";
        static readonly string lowerCaseRange = "a-z\\xdf-\\xf6\\xf8-\\xff";
        static readonly string mathOperatorRange = "\\xac\\xb1\\xd7\\xf7";
        static readonly string nonCharacterRange = "\\x00-\\x2f\\x3a-\\x40\\x5b-\\x60\\x7b-\\xbf";
        static readonly string punctuationRange = "\\u2000-\\u206f";
        static readonly string spaceRange = " \\t\\x0b\\f\\xa0\\ufeff\\n\\r\\u2028\\u2029\\u1680\\u180e\\u2000\\u2001\\u2002\\u2003\\u2004\\u2005\\u2006\\u2007\\u2008\\u2009\\u200a\\u202f\\u205f\\u3000";
        static readonly string upperCaseRange = "A-Z\\xc0-\\xd6\\xd8-\\xde";
        static readonly string variableRange = "\\ufe0e\\ufe0f";
        static readonly string breakRange = mathOperatorRange + nonCharacterRange + punctuationRange + spaceRange;
        internal static readonly string characters = "\\[a-z][A-Z]|[A-Z]{2}[a-z]|[0-9][a-zA-Z]|[a-zA-Z][0-9]|[^a-zA-Z0-9 ]\\";
        internal static readonly string asciiCharacterExp = "\\[^\\x00-\\x2f\\x3a-\\x40\\x5b-\\x60\\x7b-\\x7f]+\\";

        // Used to compose unicode capture groups.
        static readonly string apostrophe = "['\u2019]";
        static readonly string breakCharacter = $"[{breakRange}]";
        static readonly string comboCharacter = $"[{comboRange}]";
        static readonly string digit = "\\d";
        static readonly string dingbatCharacter = $"[{dingbatRange}]";
        static readonly string lowerCaseCharacter = $"[{lowerCaseRange}]";
        static readonly string miscellaneousCharacter = $"[^{astralRange}{breakRange + digit + dingbatRange + lowerCaseRange + upperCaseRange}]";
        static readonly string fitzpatrickModifier = "\\ud83c[\\udffb-\\udfff]";
        static readonly string modifier = $"(?:{comboCharacter}|{fitzpatrickModifier})";
        static readonly string nonAstralCharacter = $"[^{astralRange}]";
        static readonly string regionalIndicatorSymbol = "(?:\\ud83c[\\udde6-\\uddff]){2}";
        static readonly string surrogatePair = "[\\ud800-\\udbff][\\udc00-\\udfff]";
        static readonly string upperCaseCharacter = $"[{upperCaseRange}]";
        static readonly string zeroWidthJoiner = "\\u200d";

        // Used to compose unicode regexes.
        static readonly string miscellaneousLowerCase = $"(?:{lowerCaseCharacter}|{miscellaneousCharacter})";
        static readonly string miscellaneousUpperCase = $"(?:{upperCaseCharacter}|{miscellaneousCharacter})";
        static readonly string optionalContractionLowerCase = $"(?:{apostrophe}(?:d|ll|m|re|s|t|ve))?";
        static readonly string optionalContractionUpperCase = $"(?:{apostrophe}(?:D|LL|M|RE|S|T|VE))?";
        static readonly string optionalModifier = $"{modifier}?";
        static readonly string optionalVariable = $"[{variableRange}]?";
        static readonly string optionalJoin = $"(?:{zeroWidthJoiner}(?:{string.Join("|", new string[] { nonAstralCharacter, regionalIndicatorSymbol, surrogatePair })}){optionalVariable + optionalModifier})*";
        static readonly string ordinalLowerCase = "\\d*(?:1st|2nd|3rd|(?![123])\\dth)(?=\\b|[A-Z_])";
        static readonly string ordinalUpperCase = "\\d*(?:1ST|2ND|3RD|(?![123])\\dTH)(?=\\b|[a-z_])";
        static readonly string sequence = optionalVariable + optionalModifier + optionalJoin;
        static readonly string emoji = $"(?:{string.Join("|", new string[] { dingbatCharacter, regionalIndicatorSymbol, surrogatePair })}){sequence}";

        // Used to compose unicode regexes.
        static readonly string upperCaseOptionalLowerCasePlusOptionalContractionLowerCasePattern = $"{upperCaseCharacter}?{lowerCaseCharacter}+{optionalContractionLowerCase}(?={string.Join("|", new string[] { breakCharacter, upperCaseCharacter, "$" })})";
        static readonly string miscellaneousUpperCasePlusOptionalContractionUpperCasePattern = $"{miscellaneousUpperCase}+{optionalContractionUpperCase}(?={string.Join("|", new string[] { breakCharacter, upperCaseCharacter + miscellaneousLowerCase, "$" })})";
        static readonly string upperCaseOptionalMiscellaneousLowerCasePlusOptionalContractionLowerCasePattern = $"{upperCaseCharacter}?{miscellaneousLowerCase}+{optionalContractionLowerCase}";
        static readonly string upperCasePlusOptionalContractionUpperCasePattern = $"{upperCaseCharacter}+{optionalContractionUpperCase}";
        static readonly string unicodeWordsRegexPattern = string.Join("|", new string[] { upperCaseOptionalLowerCasePlusOptionalContractionLowerCasePattern, miscellaneousUpperCasePlusOptionalContractionUpperCasePattern, upperCaseOptionalMiscellaneousLowerCasePlusOptionalContractionLowerCasePattern, upperCasePlusOptionalContractionUpperCasePattern, ordinalUpperCase, ordinalLowerCase, $"{digit}+", emoji });

        internal static readonly Regex unicodeWordsRegex = new(unicodeWordsRegexPattern, RegexOptions.Compiled);
    }
}
