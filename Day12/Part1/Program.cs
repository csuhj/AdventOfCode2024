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

string exampleContents3 = 
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

char[,] array = ArrayHelper.ReadIntoArray(contents);

Map map = new Map(array.GetLength(0), array.GetLength(1));
map.ReadArrayIntoMap(array);

int totalFenceCost = 0;
foreach (Region region in map.Regions)
{
    int area = region.Area;
    int perimeter = region.CalculatePerimiter(map);
    totalFenceCost += area * perimeter;
}

Console.WriteLine($"There are {map.Regions.Count} regions of {map.Regions.Select(r => r.Type).Distinct().Count()} different plot types, with a total fence cost is {totalFenceCost}");





