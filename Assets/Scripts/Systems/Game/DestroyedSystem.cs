using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class DestroySystem : ReactiveSystem<GameEntity>
    {
        public DestroySystem(Contexts contexts) : base(contexts.game)  { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Destroy);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroy;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasMonoView)
                    entity.monoView.instance.Destroy();
                
                entity.Destroy();
            }
        }
    }
}