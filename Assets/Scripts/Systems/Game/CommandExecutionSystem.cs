using Entitas;

namespace BallRunner.Systems
{
    public class CommandExecutionSystem : IExecuteSystem
    {
        private IGroup<GameEntity> entitiesGroup;

        public CommandExecutionSystem(Contexts contexts)
        {
            entitiesGroup = contexts.game.GetGroup(GameMatcher.CommandBuffer);
        }
        
        public void Execute()
        {
            foreach (var entity in entitiesGroup.GetEntities())
            {
                if (entity.commandBuffer.instance.IsEmpty())
                    entity.RemoveCommandBuffer();
                else
                    entity.commandBuffer.instance.Execute();
            }
        }
    }
}