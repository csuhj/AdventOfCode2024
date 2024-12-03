using System.Text.RegularExpressions;
using Utilities;

const string Url = "2024/day/3/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

Regex regex = new Regex("mul\\((\\d+),(\\d+)\\)|do\\(\\)|don't\\(\\)");
var matches = regex.Matches(contents);

bool enabled = true;
int enabledMulInstances = 0;
int sumOfProducts = matches.Aggregate(0, (sum, match) => {
    if (match.Groups[0].Value == "do()")
    {
        enabled = true;
        return sum;
    }
    else if (match.Groups[0].Value == "don't()")
    {
        enabled = false;
        return sum;
    }
    else
    {
        enabledMulInstances += enabled? 1 : 0;
        return enabled?
            sum + (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value)) :
            sum;
    }
});

Console.WriteLine($"Sum of {enabledMulInstances} enabled mul products is {sumOfProducts}");