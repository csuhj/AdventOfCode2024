using Utilities;

const string Url = "2024/day/10/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
char[,] array = ArrayHelper.ReadIntoArray(contents);

Dictionary<Point, int> trailHeadToScoreMap = new Dictionary<Point, int>();
Dictionary<Point, int> trailHeadToRatingMap = new Dictionary<Point, int>();
for (int i = 0; i < array.GetLength(0); i++)
{
    for (int j=0; j < array.GetLength(1); j++)
    {
        Point point = new Point(i, j);
        if (IsTrailHead(point, array))
        {
            var scoreAndRating = GetNumberOfSummitsAndTrailsAvailableFromPoint(point, array);
            trailHeadToScoreMap.Add(point, scoreAndRating.Item1);
            trailHeadToRatingMap.Add(point, scoreAndRating.Item2);
        }
    }
}

Console.WriteLine($"Of {trailHeadToScoreMap.Count} trail heads in the {array.GetLength(0)} x {array.GetLength(1)} map, the total score was {trailHeadToScoreMap.Values.Sum()} and the total rating was {trailHeadToRatingMap.Values.Sum()}");


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

(int, int) GetNumberOfSummitsAndTrailsAvailableFromPoint(Point point, char[,] array)
{
    HashSet<Point> summits = new HashSet<Point>();
    int numberOfValidTrailsToASummit = RecordSummitsAvailableFromPoint(summits, point, array);
    return (summits.Count, numberOfValidTrailsToASummit);
}

int RecordSummitsAvailableFromPoint(HashSet<Point> summits, Point point, char[,] array)
{
    int numberOfValidTrailsToASummit = 0;
    int currentHeight = GetHeightAtPoint(point, array);

    Point north = new Point(point.X - 1, point.Y);
    numberOfValidTrailsToASummit += RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, north, array);

    Point south = new Point(point.X + 1, point.Y);
    numberOfValidTrailsToASummit += RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, south, array);

    Point east = new Point(point.X, point.Y - 1);
    numberOfValidTrailsToASummit += RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, east, array);

    Point west = new Point(point.X, point.Y + 1);
    numberOfValidTrailsToASummit += RecordSummitsAvailableMovingToNewPoint(summits, currentHeight, west, array);

    return numberOfValidTrailsToASummit;
}

int RecordSummitsAvailableMovingToNewPoint(HashSet<Point> summits, int currentHeight, Point newPoint, char[,] array)
{
    int newHeight = GetHeightAtPoint(newPoint, array);

    if (newHeight != currentHeight + 1)
        return 0;

    if (newHeight == 9)
    {
        summits.Add(newPoint);
        return 1;
    }
    else
    {
        return RecordSummitsAvailableFromPoint(summits, newPoint, array);
    }
}