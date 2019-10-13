using Entitas;
using UnityEngine;

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
                entity.ReplacePosition(entity.transformView.instance.Position);
                entity.ReplaceRotation(entity.transformView.instance.Rotation);
            }
        }
    }
}