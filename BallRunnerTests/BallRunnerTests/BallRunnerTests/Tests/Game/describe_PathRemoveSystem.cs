using System;
using BallRunner.Systems;
using NSpec;

namespace Tests.Game
{
    public class describe_PathRemoveSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            PathRemoveSystem pathRemoveSystem = null;
            GameEntity pathCreatorEntity = null;
            
            before = () =>
            {
                contexts = new Contexts();
                pathRemoveSystem = new PathRemoveSystem(contexts);

                contexts.game.isPathCreator = true;
                pathCreatorEntity = contexts.game.pathCreatorEntity;
            };

            context["Given path creator entity with two board entities"] = () =>
            {
                GameEntity firstBoardEntity = null;
                GameEntity lastBoardEntity = null;
                
                before = () =>
                {
                    pathCreatorEntity.ReplaceFirstBoardId(0);
                    pathCreatorEntity.ReplaceLastBoardId(1);
                    pathCreatorEntity.ReplaceCountBoards(2);

                    firstBoardEntity = contexts.game.CreateEntity();
                    firstBoardEntity.ReplaceBoardId(0);
                    firstBoardEntity.ReplaceNextBoardId(1);
                    
                    lastBoardEntity = contexts.game.CreateEntity();
                    lastBoardEntity.ReplaceBoardId(1);
                        
                    pathCreatorEntity.ReplaceRemoveBoards(1);
                    pathRemoveSystem.Execute();
                };
                
                it["Must be change first board id component to next id"] = () =>
                {
                    pathCreatorEntity.firstBoardId.value.should_be(1);
                };

                it["Must add destroy component to the first board entity"] = () =>
                {
                    firstBoardEntity.isDestroyed.should_be_true();
                };
            };
        }
    }
}