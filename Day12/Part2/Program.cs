using Utilities;

const string Url = "2024/day/12/input";
const string CookieFilePath = "../../../../../session-cookie.txt";

string exampleContents1 = 
    "AAAA\n"+
    "BBCD\n"+
    "BBCC\n"+
    "EEEC";

string exampleContents2 =
    "OOOOO\n"+
    "OXOXO\n"+
    "OOOOO\n"+
    "OXOXO\n"+
    "OOOOO\n";

string exampleContents3=
    "EEEEE\n"+
    "EXXXX\n"+
    "EEEEE\n"+
    "EXXXX\n"+
    "EEEEE\n";

string exampleContents4=
    "AAAAAA\n"+
    "AAABBA\n"+
    "AAABBA\n"+
    "ABBAAA\n"+
    "ABBAAA\n"+
    "AAAAAA\n";

string exampleContents5 = 
    "RRRRIICCFF\n"+
    "RRRRIICCCF\n"+
    "VVRRRCCFFF\n"+
    "VVRCCCJFFF\n"+
    "VVVVCJJCFE\n"+
    "VVIVCCJJEE\n"+
    "VVIIICJJEE\n"+
    "MIIIIIJJEE\n"+
    "MIIISIJEEE\n"+
    "MMMISSJEEE\n";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

Dictionary<string, string> nameToContentsMap = new Dictionary<string, string>() {
    {"Example 1", exampleContents1},
    {"Example 2", exampleContents2},
    {"Example 3", exampleContents3},
    {"Example 4", exampleContents4},
    {"Example 5", exampleContents5},
    {"Real Data", contents},
};

foreach (var name in nameToContentsMap.Keys)
{
    char[,] array = ArrayHelper.ReadIntoArray(nameToContentsMap[name]);

    Map map = new Map(array.GetLength(0), array.GetLength(1));
    map.ReadArrayIntoMap(array);

    int totalFenceCost = 0;
    foreach (Region region in map.Regions)
    {
        int area = region.Area;
        int numberOfSides = region.CalculateNumberOfSides(map);
        totalFenceCost += area * numberOfSides;
    }

    Console.WriteLine($"For {name}, there are {map.Regions.Count} regions of {map.Regions.Select(r => r.Type).Distinct().Count()} different plot types, with a total fence cost (after bulk discount) of {totalFenceCost}");
}




