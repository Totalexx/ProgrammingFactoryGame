namespace Programming
{
    public class Robot
    {
        private readonly RobotController robot;

        public Robot(string robotName)
        {
            robot = UnityEngine
                .GameObject
                .Find("Robot-" + robotName)
                .GetComponent<RobotController>();
        }
        
        public void MoveTo(MoveDirection direction)
        {
            robot.MoveTo(direction);
        }
        
        public void Debug(string s)
        {
            UnityEngine.Debug.Log(s);
        }
    }
}