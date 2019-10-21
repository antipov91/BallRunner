using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class PathRemoveNewBoardSystem : ReactiveSystem<GameEntity>
    {
        public PathRemoveNewBoardSystem(Contexts contexts) : base(contexts.game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.NewBoardId.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasNewBoardId;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
                entity.RemoveNewBoardId();
        }
    }
}