using Utilities;

public class Map
{
    private readonly Region[,] regionArray;

    public List<Region> Regions { get; }

    public Map(int width, int height)
    {
        regionArray = new Region[width, height];
        Regions = new List<Region>();
    }

    public void ReadArrayIntoMap(char[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j=0; j < array.GetLength(1); j++)
            {
                Point point = new Point(i, j);
                Region? region = FindMatchingRegionAboveOrToLeft(point, array);
                if (region == null)
                {
                    region = new Region(GetTypeAtPoint(point, array));
                    Regions.Add(region.Value);
                }
                region.Value.Points.Add(point);
                regionArray[i, j] = region.Value;
            }
        }
    }

    public Region? GetRegionAtPoint(Point point)
    {
        if (point.X < 0 || point.X >= regionArray.GetLength(0) || point.Y < 0 || point.Y >= regionArray.GetLength(1))
            return null;
    
        return regionArray[point.X, point.Y];
    }

    private Region? FindMatchingRegionAboveOrToLeft(Point point, char[,] array)
    {
        char currentType = GetTypeAtPoint(point, array);

        Region? matchedLeftRegion = null;
        Region? matchedAboveRegion = null;

        Point left = new Point(point.X - 1, point.Y);
        Region? leftRegion = GetRegionAtPoint(left);
        if (leftRegion != null && leftRegion.Value.Type == currentType)
            matchedLeftRegion = leftRegion;

        Point above = new Point(point.X, point.Y - 1);
        Region? aboveRegion = GetRegionAtPoint(above);
        if (aboveRegion != null && aboveRegion.Value.Type == currentType)
            matchedAboveRegion = aboveRegion;

        if (matchedLeftRegion != null && matchedAboveRegion != null && !matchedLeftRegion.Equals(matchedAboveRegion)) 
        {
            return MergeRegions(matchedLeftRegion.Value, matchedAboveRegion.Value);
        }

        return matchedLeftRegion ?? matchedAboveRegion;
    }

    private Region MergeRegions(Region region1, Region region2)
    {
        foreach (Point point in region1.Points.ToList())
        {
            regionArray[point.X, point.Y] = region2;
            region2.Points.Add(point);
        }
        Regions.Remove(region1);
        return region2;
    }

    private char GetTypeAtPoint(Point point, char[,] array)
    {
        if (point.X < 0 || point.X >= array.GetLength(0) || point.Y < 0 || point.Y >= array.GetLength(1))
            return Region.EdgeOfMapType;
        
        return array[point.X, point.Y];
    }
}