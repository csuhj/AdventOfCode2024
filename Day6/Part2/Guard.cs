using Utilities;

public class Guard
{
    public Point Location { get; set; }
    public DirectionEnum Direction { get; set; }

    public Guard MoveForwardOneStep() {
        (int, int) movementVector = GetMovementVector();
        Location = new Point(Location.X + movementVector.Item1, Location.Y + movementVector.Item2);
        return this;
    }

    public Guard MoveBackwardsOneStep() {
        (int, int) movementVector = GetMovementVector();
        Location = new Point(Location.X - movementVector.Item1, Location.Y - movementVector.Item2);
        return this;
    }

    public Guard Turn() {
       Direction = (DirectionEnum) ((((int)Direction) + 1) % 4);
        return this;
    }

    public Guard Clone()
    {
        return new Guard() { Location = Location, Direction = Direction };
    }

    public override bool Equals(object? obj)
    {
        return obj is Guard guard &&
            Location.Equals(guard.Location) &&
            Direction == guard.Direction;
    }

    public override int GetHashCode()
    {
        return Location.GetHashCode() * Direction.GetHashCode();
    }

    public override string ToString()
    {
        return $"Guard at {Location} facing {Direction}";
    }

    private (int, int) GetMovementVector() {
        switch (Direction) {
            case DirectionEnum.Up:
                return (0, -1);
            case DirectionEnum.Right:
                return (1, 0);
            case DirectionEnum.Down:
                return (0, 1);
            case DirectionEnum.Left:
                return (-1, 0);
            default:
                throw new ArgumentOutOfRangeException(nameof(Direction)); 
        }
    }
}