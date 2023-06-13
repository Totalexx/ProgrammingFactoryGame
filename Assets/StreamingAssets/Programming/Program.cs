using Programming.Api;

public class Program
{
    public static void Main()
    {
        var robot = new Robot("Robot");
        robot.MoveTo(MoveDirection.Up);
    }
}