using System.ComponentModel;
using System.Runtime.CompilerServices;
using Utilities;

const string Url = "2024/day/11/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
List<string> stones = contents.Split().Where(s => s.Length > 0).ToList();

for (int i = 0;i<25; i++)
{
    stones = Blink(stones);
}

Console.WriteLine($"After 25 blinks there are {stones.Count} stones");



List<string> Blink(List<string> stones)
{
    return stones.SelectMany(ProcessStone).ToList();
}

List<string> ProcessStone(string stone)
{
    if (stone == "0")
        return new List<string>() {"1"};
    else if (stone.Length % 2 == 0)
        return new List<string>() { 
            TrimLeadingZeros(stone.Substring(0, stone.Length / 2)), 
            TrimLeadingZeros(stone.Substring(stone.Length / 2, stone.Length / 2))
        };
    else
        return new List<string>() { (long.Parse(stone) * 2024).ToString() };
}

string TrimLeadingZeros(string s) => long.Parse(s).ToString();