using System.Collections.Generic;
using Entitas;

namespace BallRunner.Systems
{
    public class PathChainSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;

        public PathChainSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.NewBoardId.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPathCreator && entity.hasNewBoardId;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var boardEntity = contexts.game.GetEntityWithBoardId(entity.newBoardId.value);
                if (entity.hasLastBoardId == false)
                    boardEntity.ReplaceBoardId(0);
                else
                {
                    var lastBoardEntity = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value);
                    boardEntity.ReplaceBoardId(lastBoardEntity.boardId.value + 1);
                    lastBoardEntity.ReplaceNextBoardId(boardEntity.boardId.value);
                }
                if (entity.hasFirstBoardId == false)
                    entity.ReplaceFirstBoardId(boardEntity.boardId.value);
                entity.ReplaceLastBoardId(boardEntity.boardId.value);
                
                var count = entity.hasCountBoards ? entity.countBoards.value : 0;
                entity.ReplaceCountBoards(count + 1);
            }
        }
    }
}