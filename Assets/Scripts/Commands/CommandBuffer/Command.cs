using System;

namespace BallRunner.Commands
{
    public abstract class Command
    {
        public bool IsComplete { get; protected set; }

        public Command()
        {
            IsComplete = false;
        }
        
        public virtual void Initialize()
        {
            
        }
        public abstract void Execute();

        public virtual void Release()
        {
            
        }
    }
}