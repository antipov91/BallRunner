using  System.Collections.Generic;
using Entitas;

namespace BallRunner.Commands
{
    public class CommandBuffer : ICommandBuffer
    {
        private Queue<Command> commands = new Queue<Command>();
        private Command currentCommand;

        public void Add(Command command)
        {
            commands.Enqueue(command);
        }

        public void Execute()
        {
            if (ReferenceEquals(currentCommand, null))
            {
                if (commands.Count == 0)
                    return;

                currentCommand = commands.Dequeue();
                currentCommand.Initialize();
            }
           
            currentCommand.Execute();

            if (currentCommand.IsComplete)
            {
                currentCommand.Release();
                currentCommand = null;
            }
        }

        public bool IsEmpty()
        {
            return commands.Count == 0 && currentCommand == null;
        }
    }
}