using Utilities;

const string Url = "2024/day/5/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

string[] lines = contents.Split("\n").ToArray();
string[] ruleLines = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();
string[] pageUpdateLines = lines.Skip(ruleLines.Length + 1).TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();

List<Rule> rules = ruleLines.Select(l => Rule.TryParseRule(l, out Rule? rule) ? rule : null).Where(r => r != null).ToList();
List<List<int>> pageUpdates = pageUpdateLines.Select(l => l.Split(",").Select(int.Parse).ToList()).ToList();

rules = rules.OrderBy(r => r.InitialPage).ToList();

List<List<int>> correctPageUpdates = pageUpdates.Where(pu => rules.All(r => r.MatchesRule(pu))).ToList();
int sumOfMiddleNumberFromCorrectPageUpdates = correctPageUpdates
    .Sum(pu => pu[pu.Count / 2]);

Console.WriteLine($"Sum of middle number of {correctPageUpdates.Count} correct page updates, given {rules.Count} rules is {sumOfMiddleNumberFromCorrectPageUpdates}");

List<List<int>> incorrectPageUpdates = pageUpdates.Where(pu => !rules.All(r => r.MatchesRule(pu))).ToList();
int sumOfMiddleNumberFromCorrectedPageUpdates = incorrectPageUpdates
    .Select(pu => CreateCorrectedUpdate(pu, rules))
    .Sum(pu => pu[pu.Count / 2]);

Console.WriteLine($"Sum of middle number of {incorrectPageUpdates.Count} corrected page updates, given {rules.Count} rules is {sumOfMiddleNumberFromCorrectedPageUpdates}");



static List<int> CreateCorrectedUpdate(List<int> pageUpdate, List<Rule> rules)
{
    List<int> correctedPageUpdate = new List<int>(pageUpdate);
    foreach (var rule in rules)
    {
        while (!rule.MatchesRule(correctedPageUpdate) && ShiftValuesBackOne(correctedPageUpdate, rule.InitialPage))
            ;
    }

    if (!rules.All(r => r.MatchesRule(correctedPageUpdate)))
        throw new Exception($"Attempted to fix page update, but still doesn't meet the rules");

    return correctedPageUpdate;
}

static bool ShiftValuesBackOne(List<int> correctedPageUpdate, int initialPage)
{
    int initialIndex = correctedPageUpdate.IndexOf(initialPage);
    if (initialIndex < 1)
        return false;

    int valueToSwap = correctedPageUpdate[initialIndex - 1];
    correctedPageUpdate[initialIndex - 1] = initialPage;
    correctedPageUpdate[initialIndex] = valueToSwap;
    return true;
}