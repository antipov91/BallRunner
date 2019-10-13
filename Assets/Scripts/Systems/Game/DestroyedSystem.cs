using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class DestroyedSystem : ReactiveSystem<GameEntity>
    {
        public DestroyedSystem(Contexts contexts) : base(contexts.game)  { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Destroyed);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroyed;
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