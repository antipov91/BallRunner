using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class LocalRotationUpdateSystem : ReactiveSystem<GameEntity>
    {
        public LocalRotationUpdateSystem(Contexts contexts) : base(contexts.game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LocalRotation.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasLocalRotation && entity.hasTransformView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.transformView.instance.LocalRotate(entity.localRotation.value);
                entity.RemoveLocalRotation();
            }
        }
    }
}