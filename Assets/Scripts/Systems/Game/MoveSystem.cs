using Entitas;

namespace BallRunner.Systems
{
    public class MoveSystem : IExecuteSystem
    {
        private IGroup<GameEntity> entitiesGroup;

        public MoveSystem(Contexts contexts)
        {
            entitiesGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.LinearVelocity,
                GameMatcher.Direction));
        }
        
        public void Execute()
        {
            foreach (var entity in entitiesGroup.GetEntities())
            {
                var position = entity.position.value + entity.direction.value.normalized * entity.linearVelocity.value;
                entity.ReplacePosition(position);
            }
        }
    }
}