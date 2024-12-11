using Utilities;

const string Url = "2024/day/6/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
char[,] array = ArrayHelper.ReadIntoArray(contents);

var guard = FindGuard(array);
if (guard == null)
    throw new Exception("Couldn't find guard to start with!");

HashSet<Point> pointsCovered = new HashSet<Point>();

Console.WriteLine("3, 1 = " + array[3,1]);
Console.WriteLine("1, 3 = " + array[1,3]);

while(InsideGrid(guard, array.GetLength(0), array.GetLength(1))) {
    if (array[guard.Location.X, guard.Location.Y] == '#') {
        guard.MoveBackwardsOneStep();
        guard.Turn();
    } else {
        pointsCovered.Add(guard.Location);
        guard.MoveForwardOneStep();
    }
}

Console.WriteLine($"The total number of unique locations the guard visited was {pointsCovered.Count}");

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