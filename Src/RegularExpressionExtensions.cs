namespace extension.helpers
{
    public static partial class RegularExpressionExtensions
    {
        public static List<string> CollectWords(this MatchCollection sourceMatchCollection) => sourceMatchCollection.Select(match => match.Value).ToList();
    }
}
