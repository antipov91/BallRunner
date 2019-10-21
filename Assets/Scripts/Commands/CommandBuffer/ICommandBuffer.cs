using System;

namespace BallRunner.Commands
{
    public interface ICommandBuffer

    {
    void Add(Command command);
    void Execute();
    bool IsEmpty();
    }
}