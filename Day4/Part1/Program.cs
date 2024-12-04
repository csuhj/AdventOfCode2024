using Utilities;

const string Url = "2024/day/4/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);

string[] lines = contents.Split("\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

// Map lines into array of columns x rows to make it slightly easier to see how this works
char[,] array = new char[lines[0].Length, lines.Length];
for (int x=0; x<array.GetLength(0); x++)
    for (int y=0; y<array.GetLength(1); y++)
        array[x, y] = lines[y][x];

string stringToFind = "XMAS";

int numberOfInstancesOfString = 0;
for (int x=0; x<array.GetLength(0); x++)
    for (int y=0; y<array.GetLength(1); y++)
        numberOfInstancesOfString += GetNumberOfMatchesFromPoint(array, x, y, stringToFind);

Console.WriteLine($"Total number of instances of \"{stringToFind}\" found in the {array.GetLength(0)} x {array.GetLength(1)} grid is {numberOfInstancesOfString}");




static int GetNumberOfMatchesFromPoint(char[,] array, int x, int y, string stringToFind)
{
    if (stringToFind.Length < 2)
        throw new Exception("Trivial wordsearch finding a single letter - this sux!");

    if (array[x, y] != stringToFind[0])
        return 0;
    
    List<(int, int)> candidateDirections = new List<(int, int)>()
    {
        (-1, 0),
        (-1, -1),
        (0, -1),
        (1, -1),
        (1, 0),
        (1, 1),
        (0, 1),
        (-1, 1)
    };

    for (int charIndex = 1; charIndex < stringToFind.Length; charIndex++)
    {
        FilterCandidateDirections(array, x, y, candidateDirections, charIndex, stringToFind[charIndex]);
        if (candidateDirections.Count == 0)
            return 0;
    }

    return candidateDirections.Count;
}

static void FilterCandidateDirections(char[,] array, int x, int y, List<(int, int)> candidateDirections, int vectorLength, char charToFind)
{
    for(int i=candidateDirections.Count - 1; i>=0; i--)
        if (!FoundCharInDirection(array, x, y, candidateDirections[i], vectorLength, charToFind))
            candidateDirections.RemoveAt(i);
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