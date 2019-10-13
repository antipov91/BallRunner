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
                boardEntity.ReplaceBoard(BoardType.Forward);
                boardFactoryMock.Setup(x => x.CreateBoard(It.IsAny<Contexts>(), It.IsAny<BoardType>())).Returns(boardEntity);
                
                pathBuildingSystem = new PathBuildingSystem(contexts, boardFactoryMock.Object);
                
                contexts.game.isPathCreator = true;

                var configsInitializationSystem = new ConfigsInitializationSystem(contexts);
                configsInitializationSystem.Initialize();
            };

            context["Added to the path creator push board component with value 1"] = () =>
            {
                before = () =>
                {
                    pathCreatorEntity = contexts.game.pathCreatorEntity;
                    pathCreatorEntity.ReplacePushBoards(1);
                    pathBuildingSystem.Execute();
                };
                
                it["Must create entity with board component"] = () =>
                {
                    IGroup<GameEntity> entities = contexts.game.GetGroup(GameMatcher.Board);
                    entities.count.should_be(1);
                };

                it["Must add count boards component"] = () =>
                {
                    pathCreatorEntity.hasCountBoards.should_be_true();
                    pathCreatorEntity.countBoards.value.should_be(1);
                };

                it["Must add first board id component"] = () =>
                {
                    pathCreatorEntity.hasFirstBoardId.should_be_true();
                };

                it["Must add last board id component"] = () =>
                {
                    pathCreatorEntity.hasLastBoardId.should_be_true();
                };

                it["Must add board component to board entity"] = () =>
                {
                    boardEntity.hasBoard.should_be_true();
                };
                
                it["Must add position component to board entity"] = () =>
                {
                    boardEntity.hasPosition.should_be_true();
                };

                it["Must add board id component to board entity"] = () =>
                {
                    boardEntity.hasBoardId.should_be_true();
                };
            };

            new Each<BoardType, Vector3, Vector3, Vector3>()
            {
                {BoardType.Forward, Vector3.zero, Vector3.zero, Vector3.forward},
                {BoardType.Back, Vector3.zero, Vector3.zero, Vector3.back},
                {BoardType.Left, Vector3.zero, Vector3.zero, Vector3.left},
                {BoardType.Right, new Vector3(10, 0, 1), Vector3.zero, new Vector3(11, 0, 1)},
                {BoardType.Right, new Vector3(10, 0, 1), Vector3.right, new Vector3(10.7071f, 0, 0.2928f)},
            }.Do((boardType, position, rotation, expected) =>
            {
                context["Given path creator entity with some board entities and push board component with value 1." +
                        "And add board entity with board component with value {0}, position component with {1} and rotation component with value {2}, must be add position component with value {3} to next board entity".With(boardType, position, rotation, expected)] = () =>
                    {
                        before = () =>
                        {
                            var lastBoardEntity = contexts.game.CreateEntity();
                            lastBoardEntity.ReplaceBoard(boardType);
                            lastBoardEntity.ReplacePosition(position);
                            lastBoardEntity.ReplaceRotation(rotation);
                            lastBoardEntity.ReplaceBoardId(1);
                            
                            pathCreatorEntity = contexts.game.pathCreatorEntity;
                            pathCreatorEntity.ReplaceLastBoardId(1);
                            pathCreatorEntity.ReplaceDirectPathDelay(5);
                            pathCreatorEntity.ReplacePushBoards(1);
                            
                            pathBuildingSystem.Execute();
                        };

                        it["Add position component with value {0} to board entity".With(expected)] = () =>
                        {
                            boardEntity.position.value.should_be(expected);
                        };
                    }; 
            });
        }
    }
}