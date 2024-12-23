using Utilities;

public struct Region
{
    public static readonly char EdgeOfMapType = (char)0;

    public char Type { get; }
    public List<Point> Points { get; }

    public int Area => Points.Count;

    public Region(char type)
    {
        Type = type;
        Points = new List<Point>();
    }

    public int CalculatePerimiter(Map map)
    {
        int perimeter = 0;
        foreach (Point point in Points)
        {
            perimeter += 4 - GetNumberOfSurroundingPointsInSameRegion(point, map);
        }

        return perimeter;
    }

    private int GetNumberOfSurroundingPointsInSameRegion(Point point, Map map)
    {
        int count = 0;

        Point left = new Point(point.X - 1, point.Y);
        if (DoesRegionAtPointMatch(left, map))
            count++;

        Point above = new Point(point.X, point.Y - 1);
        if (DoesRegionAtPointMatch(above, map))
            count++;
        
        Point right = new Point(point.X + 1, point.Y);
        if (DoesRegionAtPointMatch(right, map))
            count++;
        
        Point below = new Point(point.X, point.Y + 1);
        if (DoesRegionAtPointMatch(below, map))
            count++;
        
        return count;
    }

    private bool DoesRegionAtPointMatch(Point point, Map map)
    {
        Region? region = map.GetRegionAtPoint(point);
        if (region == null)
            return false;

        if (region.Value.Type != Type)
            return false;

        return true;
    }
}