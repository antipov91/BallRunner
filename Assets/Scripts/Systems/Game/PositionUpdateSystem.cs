using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class PositionUpdateSystem : ReactiveSystem<GameEntity>
    {
        public PositionUpdateSystem(Contexts contexts) : base(contexts.game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Position);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasPosition && entity.hasTransformView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
                entity.transformView.instance.Position = entity.position.value;
        }
    }
}