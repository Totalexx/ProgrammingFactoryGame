namespace Programming
{
    public class RobotCommands
    {
        public static void Debug()
        {
            UnityEngine.Debug.Log("so can i just log?? :(((");
        }
        public static void MoveTo(MoveDirection direction)
        {
            var robot = UnityEngine.GameObject.Find("Robot-1").GetComponent<Robot>();
            robot.MoveTo(direction);
        }
    }
}