public class Guard
{
    public Point Location { get; set; }
    public DirectionEnum Direction { get; set; }

    public void MoveForwardOneStep() {
        (int, int) movementVector = GetMovementVector();
        Location = new Point(Location.X + movementVector.Item1, Location.Y + movementVector.Item2);
    }

    public void MoveBackwardsOneStep() {
        (int, int) movementVector = GetMovementVector();
        Location = new Point(Location.X - movementVector.Item1, Location.Y - movementVector.Item2);
    }

    public void Turn() {
       Direction = (DirectionEnum) ((((int)Direction) + 1) % 4);
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