public class Rule
{
    public int InitialPage { get; set; }
    public int SubsequentPage { get; set; }

    public static bool TryParseRule(string ruleString, out Rule? rule)
    {
        string[] ruleParts = ruleString.Split("|");
        if (ruleParts.Length != 2)
        {
            rule = null;
            return false;
        }

        if (!int.TryParse(ruleParts[0], out int initialPage) || !int.TryParse(ruleParts[1], out int subsequentPage))
        {
            rule = null;
            return false;
        }

        rule = new Rule()
        {
            InitialPage = initialPage,
            SubsequentPage = subsequentPage
        };
        return true;
    }

    public bool MatchesRule(List<int> pageNumbers)
    {
        int initialPageIndex = pageNumbers.IndexOf(InitialPage);
        int subsequentPageIndex = pageNumbers.IndexOf(SubsequentPage);
        return initialPageIndex == -1 || subsequentPageIndex == -1 || initialPageIndex < subsequentPageIndex;
    }
}