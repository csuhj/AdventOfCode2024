using System.Diagnostics.CodeAnalysis;
namespace Utilities;

public struct VectorL
{
    public long X { get; set; }
    public long Y { get; set; }

    public VectorL(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector point &&
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