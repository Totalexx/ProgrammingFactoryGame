using Programming.Api;

public class Program
{
    public static void Main()
    {
        var robot = new Robot("firstRobot");
        robot.MoveTo(MoveDirection.UP);
    }
}