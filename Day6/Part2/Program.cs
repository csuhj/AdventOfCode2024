using Utilities;

const string Url = "2024/day/6/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
char[,] array = ArrayHelper.ReadIntoArray(contents);

var guard = FindGuard(array);
if (guard == null)
    throw new Exception("Couldn't find guard to start with!");

HashSet<Guard> guardPositionsCovered = new HashSet<Guard>();
HashSet<Point> loopingObstacleLocations = new HashSet<Point>();

while(InsideGrid(guard, array.GetLength(0), array.GetLength(1)))
{
    if (IsNextStepObstacle(guard)) {
        guard.Turn();
    } else {
        if (LookaheadForCoveredPosition(guard.Clone().Turn(), guardPositionsCovered))
            loopingObstacleLocations.Add(guard.Clone().MoveForwardOneStep().Location);

        guardPositionsCovered.Add(guard.Clone());
        guard.MoveForwardOneStep();
    }
}

Console.WriteLine($"The total number of unique locations the guard visited was {guardPositionsCovered.Select(g => g.Location).Distinct().Count()}");
Console.WriteLine($"The number of possible places that you could place a single obstacle to induce a loop in the guard's walk is {loopingObstacleLocations.Count}");

bool LookaheadForCoveredPosition(Guard guard, HashSet<Guard> guardPositionsCovered) {
    HashSet<Guard> lookheadGuardPositionsCovered = new HashSet<Guard>();
    
    while(InsideGrid(guard, array.GetLength(0), array.GetLength(1)))
    {
        if (IsNextStepObstacle(guard)) {
            guard.Turn();
        } else {
            if (lookheadGuardPositionsCovered.Contains(guard))
                return true;

            if (guardPositionsCovered.Contains(guard))
                return true;

            lookheadGuardPositionsCovered.Add(guard.Clone());
            guard.MoveForwardOneStep();
        }
    } 
    return false;
}

bool IsNextStepObstacle(Guard guard) {
    var hypotheticalGuard = guard.Clone();
    hypotheticalGuard.MoveForwardOneStep();
    return InsideGrid(hypotheticalGuard, array.GetLength(0), array.GetLength(1)) && array[hypotheticalGuard.Location.X, hypotheticalGuard.Location.Y] == '#';
}

Guard? FindGuard(char[,] array) {
    for (int x=0; x<array.GetLength(0); x++)
        for (int y=0; y<array.GetLength(1); y++)
            if (array[x,y] == '^')
                return new Guard {Location = new Point(x, y), Direction = DirectionEnum.Up};
            else if (array[x,y] == '>')
                return new Guard {Location = new Point(x, y), Direction = DirectionEnum.Right};
            else if (array[x,y] == 'v')
                return new Guard {Location = new Point(x, y), Direction = DirectionEnum.Down};
            else if (array[x,y] == '<')
                return new Guard {Location = new Point(x, y), Direction = DirectionEnum.Left};

    return null;
}

bool InsideGrid(Guard guard, int width, int height) {
    return guard.Location.X >= 0 && 
        guard.Location.X < width &&
        guard.Location.Y >= 0 && 
        guard.Location.Y < height;
}