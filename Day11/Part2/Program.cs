using Utilities;

const string Url = "2024/day/11/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
List<string> stones = contents.Split().Where(s => s.Length > 0).ToList();

Dictionary<string, long> stoneToCountMap = new Dictionary<string, long>();

foreach (var stone in stones)
{
    IncrementStoneCount(stoneToCountMap, stone, 1);
}
for (int i = 0;i<75; i++)
{
    stoneToCountMap = Blink(stoneToCountMap);
    Console.WriteLine($"After {i+1} blinks there are {stoneToCountMap.Values.Sum()} stones");
}

Dictionary<string, long> Blink(Dictionary<string, long> stoneToCountMap)
{
    Dictionary<string, long> newStoneToCountMap = new Dictionary<string, long>();
    foreach (var stoneAndCount in stoneToCountMap.ToList())
    {
        string stone = stoneAndCount.Key;
        long count = stoneAndCount.Value;
        List<string> newStones = ProcessStone(stone);
        foreach (var newStone in newStones)
            IncrementStoneCount(newStoneToCountMap, newStone, count);
    }
    return newStoneToCountMap;
}

void IncrementStoneCount(Dictionary<string, long> stoneToCountMap, string stone, long increment)
{
    if (stoneToCountMap.TryGetValue(stone, out long count))
        stoneToCountMap[stone] = count + increment;
    else
        stoneToCountMap[stone] = increment; 
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