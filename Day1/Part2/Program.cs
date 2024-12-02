using System.Text.RegularExpressions;
using Utilities;

const string Url = "2024/day/1/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

List<int> list1 = new List<int>();
Dictionary<int, int> numberToCount = new Dictionary<int, int>();

Regex regex = new Regex("(\\d+)\\s+(\\d+)");
foreach (var line in contents.Split("\n"))
{
    if (string.IsNullOrWhiteSpace(line))
        continue;

    var match = regex.Match(line);
    if (!match.Success)
        throw new Exception($"Line {line} doesn't match regex");
    
    list1.Add(int.Parse(match.Groups[1].Value));

    int number = int.Parse(match.Groups[2].Value);
    if (!numberToCount.ContainsKey(number))
        numberToCount[number] = 1;
    else
        numberToCount[number] = numberToCount[number] + 1;
}

int accumulatedSimilarity = 0;
foreach (var number in list1)
{
    if (numberToCount.TryGetValue(number, out int count))
        accumulatedSimilarity += number * count;
}
Console.WriteLine($"Accumulated similarity over {list1.Count} pairs is {accumulatedSimilarity}");