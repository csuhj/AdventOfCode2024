using Utilities;

const string Url = "2024/day/10/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
char[,] array = ArrayHelper.ReadIntoArray(contents);

Dictionary<Point, int> trailHeadToScoreMap = new Dictionary<Point, int>();
for (int i = 0; i < array.GetLength(0); i++)
{
    for (int j=0; j < array.GetLength(1); j++)
    {
        Point point = new Point(i, j);
        if (IsTrailHead(point, array))
        {
            trailHeadToScoreMap.Add(point, GetNumberOfSummitsAvailableFromPoint(point, array));
        }
    }
}

Console.WriteLine($"Of {trailHeadToScoreMap.Count} trail heads in the {array.GetLength(0)} x {array.GetLength(1)} map, the total score was {trailHeadToScoreMap.Values.Sum()}");


int GetHeightAtPoint(Point point, char[,] array)
{
    if (point.X < 0 || point.X >= array.GetLength(0) || point.Y < 0 || point.Y >= array.GetLength(1))
        return -1;

    return int.Parse($"{array[point.X, point.Y]}");
}

bool IsTrailHead(Point point, char[,] array)
{
    return GetHeightAtPoint(point, array) == 0;
}

int GetNumberOfSummitsAvailableFromPoint(Point point, char[,] array)
{
    HashSet<Point> summits = new HashSet<Point>();
    RecordSummitsAvailableFromPoint(summits, point, array);
    return summits.Count;
}

void RecordSummitsAvailableFromPoint(HashSet<Point> summits, Point point, char[,] array)
{
    int currentHeight = GetHeightAtPoint(point, array);

    Point north = new Point(point.X - 1, point.Y);
    RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, north, array);

    Point south = new Point(point.X + 1, point.Y);
    RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, south, array);

    Point east = new Point(point.X, point.Y - 1);
    RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, east, array);

    Point west = new Point(point.X, point.Y + 1);
    RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, west, array);
}

void RecordSummitsAvailableMovingToNewPoint(HashSet<Point> summits, int currentHeight, Point newPoint, char[,] array)
{
    int newHeight = GetHeightAtPoint(newPoint, array);

    if (newHeight != currentHeight + 1)
        return;

    if (newHeight == 9)
    {
        summits.Add(newPoint);
    }
    else
    {
        RecordSummitsAvailableFromPoint(summits, newPoint, array);
    }
}