using Utilities;

public struct Region
{
    [Flags]
    private enum SideEnum
    {
        None = 0,
        Top = 1 << 0,
        Bottom = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
    }

    public static readonly char EdgeOfMapType = (char)0;

    public char Type { get; }
    public List<Point> Points { get; }

    public int Area => Points.Count;

    public Region(char type)
    {
        Type = type;
        Points = new List<Point>();
    }

    public int CalculateNumberOfSides(Map map)
    {
        List<Point> pointsOnTopPerimeter = new List<Point>();
        List<Point> pointsOnRightPerimeter = new List<Point>();
        List<Point> pointsOnBottomPerimeter = new List<Point>();
        List<Point> pointsOnLeftPerimeter = new List<Point>();
        foreach (Point point in Points)
        {
            SideEnum sides = GetPerimeterSides(point, map);
            if ((sides & SideEnum.Top) == SideEnum.Top)
                pointsOnTopPerimeter.Add(point);

            if ((sides & SideEnum.Right) == SideEnum.Right)
                pointsOnRightPerimeter.Add(point);

            if ((sides & SideEnum.Bottom) == SideEnum.Bottom)
                pointsOnBottomPerimeter.Add(point);

            if ((sides & SideEnum.Left) == SideEnum.Left)
                pointsOnLeftPerimeter.Add(point);
        }

        int numberOfSides = 0;
        numberOfSides += CountSides(pointsOnTopPerimeter, SideEnum.Top);
        numberOfSides += CountSides(pointsOnRightPerimeter, SideEnum.Right);
        numberOfSides += CountSides(pointsOnBottomPerimeter, SideEnum.Bottom);
        numberOfSides += CountSides(pointsOnLeftPerimeter, SideEnum.Left);

        return numberOfSides;
    }

    private SideEnum GetPerimeterSides(Point point, Map map)
    {
        SideEnum sides = SideEnum.None;

        Point left = new Point(point.X - 1, point.Y);
        if (!DoesRegionAtPointMatch(left, map))
            sides |= SideEnum.Left;

        Point above = new Point(point.X, point.Y - 1);
        if (!DoesRegionAtPointMatch(above, map))
            sides |= SideEnum.Top;
        
        Point right = new Point(point.X + 1, point.Y);
        if (!DoesRegionAtPointMatch(right, map))
            sides |= SideEnum.Right;

        Point below = new Point(point.X, point.Y + 1);
        if (!DoesRegionAtPointMatch(below, map))
            sides |= SideEnum.Bottom;
        
        return sides;
    }

    private int CountSides(List<Point> pointsOnPerimeter, SideEnum side)
    {
        if (side == SideEnum.Top || side == SideEnum.Bottom)
            return CountSides(pointsOnPerimeter, p => p.Y, p => p.X);
        else
            return CountSides(pointsOnPerimeter, p => p.X, p => p.Y);
    }

    private int CountSides(List<Point> pointsOnPerimeter, Func<Point, int> getGroupingFunc, Func<Point, int> getIndexFunc)
    {
        if (pointsOnPerimeter.Count == 0)
            return 0;

        int count = 0;
        var grouped = pointsOnPerimeter.GroupBy(getGroupingFunc);
        foreach (var group in grouped)
        {
            int lastIndex = -2;
            foreach (var point in group.OrderBy(getIndexFunc))
            {
                int index = getIndexFunc(point);
                if (index != lastIndex + 1)
                    count++;
                
                lastIndex = index;
            }
        }

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