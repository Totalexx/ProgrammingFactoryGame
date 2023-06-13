using System;
using RobotEntity.RobotAction;
using UnityEngine;
using Util;

namespace Programming.RobotAction
{
    public class MoveAction : IRobotAction
    {
        private readonly Vector3 target;
        private readonly RobotController robot;
        private readonly Action onAchieved;
        private readonly float smoothTime = 0.1f;
        private Vector3 velocity;

        public MoveAction(RobotController robotController, Vector3 target, Action onAchieved)
        {
            this.target = target;
            this.onAchieved = onAchieved;
            robot = robotController;
        }
        
        public IRobotAction Run()
        {
            robot.transform.position = Vector3.SmoothDamp(
                robot.transform.position, 
                target,
                ref velocity,
                smoothTime,
                robot.RobotSpeed,
                Time.deltaTime
            );

            if (!robot.transform.position.EqualsEsp(target))
                return this;
            
            robot.transform.position = target;
            return new InvokeAction(onAchieved);
        }
        
        
    }
}