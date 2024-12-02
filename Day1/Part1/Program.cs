using System.Text.RegularExpressions;
using Utilities;

const string Url = "2024/day/1/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

List<int> list1 = new List<int>();
List<int> list2 = new List<int>();

Regex regex = new Regex("(\\d+)\\s+(\\d+)");
foreach (var line in contents.Split("\n"))
{
    if (string.IsNullOrWhiteSpace(line))
        continue;

    var match = regex.Match(line);
    if (!match.Success)
        throw new Exception($"Line {line} doesn't match regex");
    
    list1.Add(int.Parse(match.Groups[1].Value));
    list2.Add(int.Parse(match.Groups[2].Value));
}

list1.Sort();
list2.Sort();
int accumulatedDifference = 0;
for(int i = 0; i<list1.Count; i++)
{
    accumulatedDifference += Math.Abs(list2[i] - list1[i]);
}
Console.WriteLine($"Accumulated difference over {list1.Count} pairs is {accumulatedDifference}");