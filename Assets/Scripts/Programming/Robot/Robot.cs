using System;
using System.Threading;
using Programming.MainThread;

namespace Programming
{
    public class Robot
    {
        private RobotController robot;
        private readonly AutoResetEvent _resetEvent = new (false);
        public Robot(string robotName)
        {
            SetRobotController(robotName);
        }

        public void MoveTo(MoveDirection direction)
        {
            Run(() =>
            {
                robot.MoveTo(direction, () => _resetEvent.Set());
            });
            _resetEvent.WaitOne();
        }
        
        public void Debug(string s)
        {
            Run(() => UnityEngine.Debug.Log(s));
        }

        private void SetRobotController(string robotName)
        {
            Run(() =>
            {
                var robotEntity = UnityEngine
                    .GameObject
                    .Find("Robot-" + robotName);
                robot = robotEntity.GetComponent<RobotController>();
            });
        }

        private void Run(Action action)
        {
            MainContextHolder.RunInMain(_ => action.Invoke());
        }
    }
}