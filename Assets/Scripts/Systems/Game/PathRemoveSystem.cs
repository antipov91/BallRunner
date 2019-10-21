using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class PathRemoveSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;
        
        public PathRemoveSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.RemoveBoards);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasRemoveBoards && entity.isPathCreator && entity.hasFirstBoardId;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var removeCount = entity.removeBoards.value;
                if (removeCount <= 0)
                    continue;

                for (int i = 0; i < removeCount; i++)
                {
                    var firstBoardEntity = contexts.game.GetEntityWithBoardId(entity.firstBoardId.value);
                    firstBoardEntity.isDestroyed = true;
                    entity.ReplaceFirstBoardId(firstBoardEntity.nextBoardId.value);
                }
                entity.ReplaceCountBoards(entity.countBoards.value - removeCount);
            }
        }
    }
}