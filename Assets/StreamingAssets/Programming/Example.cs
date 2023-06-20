using Programming.Api;

public class Example
{
    public static void Start()
    {
        var robot = new Robot("Robot");
        robot.MoveTo(MoveDirection.Up);
    }
}