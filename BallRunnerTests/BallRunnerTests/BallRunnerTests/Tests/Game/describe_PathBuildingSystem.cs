using BallRunner.Services;
using BallRunner.Systems;
using Entitas;
using Moq;
using NSpec;
using UnityEngine;

namespace Tests.Game
{
    public class describe_PathBuildingSystem  : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            PathBuildingSystem pathBuildingSystem = null;
            Mock<IBoardFactory> boardFactoryMock = null;
            GameEntity pathCreatorEntity = null;
            GameEntity boardEntity = null;

            before = () =>
            {
                contexts = new Contexts();
                boardFactoryMock = new Mock<IBoardFactory>();
                boardEntity = contexts.game.CreateEntity();
                boardEntity.isBoard = true;
                boardFactoryMock.Setup(x => x.CreateBoard(It.IsAny<Contexts>())).Returns(boardEntity);
                
                pathBuildingSystem = new PathBuildingSystem(contexts, boardFactoryMock.Object);

                contexts.game.isPathCreator = true;

                var configsInitializationSystem = new ConfigsInitializationSystem(contexts);
                configsInitializationSystem.Initialize();
            };

            context["Adds one board"] = () =>
            {
                before = () =>
                {
                    pathCreatorEntity = contexts.game.pathCreatorEntity;
                    pathBuildingSystem.Execute();
                };
                
                it["Must create entity with board component"] = () =>
                {
                    IGroup<GameEntity> entities = contexts.game.GetGroup(GameMatcher.Board);
                    entities.count.should_be(1);
                };

                it["Must add board component to board entity"] = () =>
                {
                    boardEntity.isBoard.should_be_true();
                };
                
                it["Must add position component to board entity"] = () =>
                {
                    boardEntity.hasPosition.should_be_true();
                };

                it["Must add board id component to board entity"] = () =>
                {
                    boardEntity.hasBoardId.should_be_true();
                };

                it["Must add sync transform component"] = () =>
                {
                    boardEntity.isSyncTransform.should_be_true();
                };
                
                it["Must add new board id component"] = () =>
                {
                    pathCreatorEntity.hasNewBoardId.should_be_true();
                };
            };
        }
    }
}