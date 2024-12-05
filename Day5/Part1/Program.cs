using Utilities;

const string Url = "2024/day/5/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

string[] lines = contents.Split("\n").ToArray();
string[] ruleLines = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();
string[] pageUpdateLines = lines.Skip(ruleLines.Length + 1).TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();

List<Rule> rules = ruleLines.Select(l => Rule.TryParseRule(l, out Rule? rule) ? rule : null).Where(r => r != null).ToList();
List<List<int>> pageUpdates = pageUpdateLines.Select(l => l.Split(",").Select(int.Parse).ToList()).ToList();

List<List<int>> correctPageUpdates = pageUpdates.Where(pu => rules.All(r => r.MatchesRule(pu))).ToList();

int sumOfMiddleNumberFromCorrectPageUpdates = 0;
foreach (List<int> correctPageUpdate in correctPageUpdates)
{
    if (correctPageUpdate.Count % 2 == 0)
        throw new Exception($"There are an even number of page numbers in update {correctPageUpdate}");

    int middleIndex = (int)Math.Floor(correctPageUpdate.Count / 2.0);
    sumOfMiddleNumberFromCorrectPageUpdates += correctPageUpdate[middleIndex];
}

Console.WriteLine($"Sum of middle number of {correctPageUpdates.Count} correctly ordered page updates (from a total of {pageUpdates.Count}), given {rules.Count} rules is {sumOfMiddleNumberFromCorrectPageUpdates}");
