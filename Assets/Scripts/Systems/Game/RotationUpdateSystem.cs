using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class RotationUpdateSystem : ReactiveSystem<GameEntity>
    {
        public RotationUpdateSystem(Contexts contexts) : base(contexts.game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Rotation);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasRotation && entity.hasTransformView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
                entity.transformView.instance.Rotation = entity.rotation.value;
        }
    }
}