using Utilities;

const string Url = "2024/day/6/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
char[,] array = ArrayHelper.ReadIntoArray(contents);

var guard = FindGuard(array);
if (guard == null)
    throw new Exception("Couldn't find guard to start with!");

var initialGuard = guard.Clone();
HashSet<Point> pointsCovered = new HashSet<Point>();
HashSet<Point> loopingObstacleLocations = new HashSet<Point>();

while(InsideGrid(guard, array.GetLength(0), array.GetLength(1)))
{
    if (IsNextStepObstacle(guard, array)) {
        guard.Turn();
    } else {
        pointsCovered.Add(guard.Location);
        guard.MoveForwardOneStep();
    }
}

Console.WriteLine($"The total number of unique locations the guard visited was {pointsCovered.Count}");

int numberOfPointsTried = 0;
foreach (Point point in pointsCovered)
{
    if (numberOfPointsTried % 100 == 0)
        Console.WriteLine($"Tried {numberOfPointsTried} points so far, found {loopingObstacleLocations.Count} new obstacle locations");

    if (DoesLoopExist(initialGuard.Clone(), point))
        loopingObstacleLocations.Add(point);

    numberOfPointsTried++;
}

Console.WriteLine($"The number of possible places that you could place a single obstacle to induce a loop in the guard's walk is {loopingObstacleLocations.Count}");

if (loopingObstacleLocations.Contains(initialGuard.Location)) {
    Console.WriteLine($"It looks like the guard's starting point {initialGuard.Location} is one of the possible places that you could place an obstacle to induce a loop in the guard's walk, so excluding this the total number of available locations to place an obstacle is {loopingObstacleLocations.Count - 1}");
}

bool DoesLoopExist(Guard guard, Point newObstacle) {
    char[,] arrayWithNewObstacle = (char[,])array.Clone();
    arrayWithNewObstacle[newObstacle.X, newObstacle.Y] = '#';

    HashSet<Guard> positionsCovered = new HashSet<Guard>();
    
    while(InsideGrid(guard, arrayWithNewObstacle.GetLength(0), arrayWithNewObstacle.GetLength(1)))
    {
        if (positionsCovered.Contains(guard))
            return true;

        if (IsNextStepObstacle(guard, arrayWithNewObstacle)) {
            guard.Turn();
        } else {
            positionsCovered.Add(guard.Clone());
            guard.MoveForwardOneStep();
        }
    }
    return false;
}

bool IsNextStepObstacle(Guard guard, char[,] array) {
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