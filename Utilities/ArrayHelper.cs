using System.Net;
namespace Utilities;

public class ArrayHelper
{
    public static char[,] ReadIntoArray(string contents)
    {
        string[] lines = contents.Split("\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

        // Map lines into array of columns x rows to make it slightly easier to see how this works
        char[,] array = new char[lines[0].Length, lines.Length];
        for (int x=0; x<array.GetLength(0); x++)
            for (int y=0; y<array.GetLength(1); y++)
                array[x, y] = lines[y][x];

        return array;
    }
}
