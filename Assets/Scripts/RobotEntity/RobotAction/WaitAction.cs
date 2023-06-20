using System;
using UnityEngine;

namespace RobotEntity.RobotAction
{
    public class WaitAction : IRobotAction
    {
        private DateTime _time = DateTime.Now;
        private readonly Action action; 

        public WaitAction(int milliseconds, Action afterAction)
        {
            _time = _time.AddMilliseconds(milliseconds);
            action = afterAction;
        }
        
        public IRobotAction Run()
        {
            if (_time > DateTime.Now)
                return this;
            return new InvokeAction(action);
        }
    }
}