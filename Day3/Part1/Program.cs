using System.Text.RegularExpressions;
using Utilities;

const string Url = "2024/day/3/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

Regex regex = new Regex("mul\\((\\d+),(\\d+)\\)");
var matches = regex.Matches(contents);
int sumOfProducts = matches.Aggregate(0, (sum, match) => sum + (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)));

Console.WriteLine($"Sum of {matches.Count} mul products is {sumOfProducts}");