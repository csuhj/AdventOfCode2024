using System.Diagnostics.CodeAnalysis;
namespace Utilities;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Point point &&
            X == point.X &&
            Y == point.Y;
    }

    public override int GetHashCode()
    {
        return X * Y;
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}