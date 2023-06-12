using System;

namespace RobotEntity.RobotAction
{
    public class InvokeAction : IRobotAction
    {
        private readonly Action action; 
        
        public InvokeAction(Action action)
        {
            this.action = action;
        }
        
        public IRobotAction Run()
        {
            action.Invoke();
            return new NoAction();
        }
    }
}