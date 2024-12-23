using System.Diagnostics.CodeAnalysis;
namespace Utilities;

public struct PointL
{
    public long X { get; set; }
    public long Y { get; set; }

    public PointL(long x, long y)
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
        return X.GetHashCode() * Y.GetHashCode();
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}