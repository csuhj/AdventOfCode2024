﻿using Utilities;

const string Url = "2024/day/8/input";
const string CookieFilePath = "../../session-cookie.txt";

string contents = await DownloadHelper.DownloadInput(CookieFilePath, Url);
char[,] array = ArrayHelper.ReadIntoArray(contents);

Dictionary<char, List<Point>> antennaToLocationsMap = FindAntennas(array);

HashSet<Point> antinodeLocations = new HashSet<Point>(antennaToLocationsMap
    .Select(kvp => GetPairs(kvp.Value))
    .SelectMany(pairs => pairs)
    .Select(pair => GetAntinodes(pair.Item1, pair.Item2, array))
    .SelectMany(antinodes => antinodes));

Console.WriteLine($"The total number of locations with one or more antinodes on the map (including resonant freqs - for {antennaToLocationsMap.Count} different antenna frequencies) are {antinodeLocations.Count()}");




Dictionary<char, List<Point>> FindAntennas(char[,] array)
{
    var antennaToLocationsMap = new Dictionary<char, List<Point>>();

    for (int x=0; x<array.GetLength(0); x++)
    {
        for (int y=0; y<array.GetLength(1); y++)
        {
            if (array[x,y] == '.')
                continue;

            if (!antennaToLocationsMap.TryGetValue(array[x,y], out List<Point> locations))
            {
                locations = new List<Point>();
                antennaToLocationsMap[array[x,y]] = locations;
            }
            locations.Add(new Point(x,y));
        }
    }

    return antennaToLocationsMap;
}

List<(Point, Point)> GetPairs(List<Point> locations)
{
    var pairs = new List<(Point,Point)>();

    for (int i=0; i<locations.Count - 1; i++)
        for (int j=i + 1; j<locations.Count; j++)
            pairs.Add((locations[i],locations[j]));
    
    return pairs;
}

HashSet<Point> GetAntinodes(Point p1, Point p2, char[,] array)
{
    int xDiff = p2.X - p1.X;
    int yDiff = p2.Y - p1.Y;

    var antinodes = new HashSet<Point>();
    Point point = new Point(p1.X, p1.Y);
    while (InsideGrid(point, array.GetLength(0), array.GetLength(1)))
    {
        antinodes.Add(point);
        point = new Point(point.X - xDiff, point.Y - yDiff);
    }

    point = new Point(p1.X, p1.Y);
    while (InsideGrid(point, array.GetLength(0), array.GetLength(1)))
    {
        antinodes.Add(point);
        point = new Point(point.X + xDiff, point.Y + yDiff);
    }

    return antinodes;
}

bool InsideGrid(Point point, int width, int height) {
    return point.X >= 0 && 
        point.X < width &&
        point.Y >= 0 && 
        point.Y < height;
}