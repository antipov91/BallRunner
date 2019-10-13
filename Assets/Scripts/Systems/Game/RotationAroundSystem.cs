using System;
using Entitas;

namespace BallRunner.Systems
{
    public class RotationAroundSystem : IExecuteSystem
    {
        private readonly Contexts contexts;
        private readonly IGroup<GameEntity> entitiesGroup;

        public RotationAroundSystem(Contexts contexts)
        {
            this.contexts = contexts;
            entitiesGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.AroundRotation,
                GameMatcher.AngularVelocity, GameMatcher.TransformView));
        }

        public void Execute()
        {
            foreach (var entity in entitiesGroup.GetEntities())
            {
                var dir = Math.Sign(entity.aroundRotation.deltaAngle);
                var turningAngleDelta = dir * entity.angularVelocity.value * contexts.time.deltaTime.value;

                if (Math.Abs(turningAngleDelta) > Math.Abs(entity.aroundRotation.deltaAngle))
                    turningAngleDelta = entity.aroundRotation.deltaAngle;

                entity.transformView.instance.RotateAround(entity.aroundRotation.point, entity.aroundRotation.axis, turningAngleDelta);
                entity.ReplaceAroundRotation(entity.aroundRotation.point, entity.aroundRotation.axis, entity.aroundRotation.deltaAngle - turningAngleDelta);

                if (entity.aroundRotation.deltaAngle == 0)
                    entity.RemoveAroundRotation();
            }
        }
    }
}