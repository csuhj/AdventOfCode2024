using Utilities;

const string Url = "2024/day/4/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

string[] lines = contents.Split("\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();


List<(int, int, char)> crossMas1 = new List<(int, int, char)>()
{
    (-1, -1, 'M'),
    (1, 1, 'S'),
    (-1, 1, 'M'),
    (1, -1, 'S'),
};
List<(int, int, char)> crossMas2 = new List<(int, int, char)>()
{
    (-1, -1, 'S'),
    (1, 1, 'M'),
    (-1, 1, 'M'),
    (1, -1, 'S'),
};
List<(int, int, char)> crossMas3 = new List<(int, int, char)>()
{
    (-1, -1, 'M'),
    (1, 1, 'S'),
    (-1, 1, 'S'),
    (1, -1, 'M'),
};
List<(int, int, char)> crossMas4 = new List<(int, int, char)>()
{
    (-1, -1, 'S'),
    (1, 1, 'M'),
    (-1, 1, 'S'),
    (1, -1, 'M'),
};

List<List<(int, int, char)>> crossMases = new List<List<(int, int, char)>>() {
    crossMas1,
    crossMas2,
    crossMas3,
    crossMas4
};

// Map lines into array of columns x rows to make it slightly easier to see how this works
char[,] array = new char[lines[0].Length, lines.Length];
for (int x=0; x<array.GetLength(0); x++)
    for (int y=0; y<array.GetLength(1); y++)
        array[x, y] = lines[y][x];

int numberOfInstancesOfCrossMas = 0;
for (int x=0; x<array.GetLength(0); x++)
    for (int y=0; y<array.GetLength(1); y++)
        numberOfInstancesOfCrossMas += IsCrossMasMatchFromPoint(array, x, y) ? 1 : 0;

Console.WriteLine($"Total number of instances of an \"X-MAS\" found in the {array.GetLength(0)} x {array.GetLength(1)} grid is {numberOfInstancesOfCrossMas}");

bool IsCrossMasMatchFromPoint(char[,] array, int x, int y)
{
    if (array[x, y] != 'A')
        return false;

    foreach (var crossMas in crossMases)
        if(crossMas.Aggregate(true, (stillMatching, directionAndLetter) => stillMatching & FoundDirectionAndChar(array, x, y, directionAndLetter)))
            return true;
    
    return false;
}

static bool FoundDirectionAndChar(char[,] array, int x, int y, (int, int, char) directionAndChar)
{
    return FoundCharInDirection(array, x, y, (directionAndChar.Item1, directionAndChar.Item2), 1, directionAndChar.Item3);
}

static bool FoundCharInDirection(char[,] array, int x, int y, (int, int) directionUnitVector, int vectorLength, char charToFind)
{
    int deltaX = directionUnitVector.Item1 * vectorLength;
    int deltaY = directionUnitVector.Item2 * vectorLength;

    if (x + deltaX < 0 || x + deltaX >= array.GetLength(0))
        return false;

    if (y + deltaY < 0 || y + deltaY >= array.GetLength(1))
        return false;

    return array[x+deltaX, y+deltaY] == charToFind;
}