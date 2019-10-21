using Entitas;

namespace BallRunner.Systems
{
    public class SyncTransformSystem : IExecuteSystem
    {
        private IGroup<GameEntity> entitiesGroup;

        public SyncTransformSystem(Contexts contexts)
        {
            entitiesGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TransformView, GameMatcher.SyncTransform));
        }

        public void Execute()
        {
            foreach (var entity in entitiesGroup.GetEntities())
            {
                if (entity.hasPosition)
                    entity.position.value = entity.transformView.instance.Position;
                else
                    entity.ReplacePosition(entity.transformView.instance.Position);
                
                if (entity.hasRotation)
                    entity.rotation.value = entity.transformView.instance.Rotation;
                else
                    entity.ReplaceRotation(entity.transformView.instance.Rotation);
            }
        }
    }
}