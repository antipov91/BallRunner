using BallRunner.Commands;
using Entitas;

[Game]
public sealed class CommandBufferComponent : IComponent
{
    public ICommandBuffer instance;
}