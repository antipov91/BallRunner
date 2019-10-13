using System;
using System.Collections.Generic;
using BallRunner.Services;
using Entitas;
using UnityEngine;
using Random = System.Random;

#region BoardType
public enum BoardType
{
    Forward = 0,
    Up = 1,
    Down = 2,
    Back = 3,
    Left = 4,
    Right = 5
}
#endregion

namespace BallRunner.Systems
{
    public class PathBuildingSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;
        private readonly IBoardFactory boardFactory;

        private readonly BoardType[][] possibleBoards;
        private Random rand;
        
        public PathBuildingSystem(Contexts contexts, IBoardFactory boardFactory) : base(contexts.game)
        {
            this.contexts = contexts;
            this.boardFactory = boardFactory;
            
            rand = new Random();
            
            possibleBoards = new BoardType[6][];
            possibleBoards[0] = new[] {BoardType.Forward, BoardType.Up, BoardType.Down, BoardType.Left, BoardType.Right};
            possibleBoards[1] = new[] {BoardType.Up, BoardType.Forward, BoardType.Back, BoardType.Left, BoardType.Right};
            possibleBoards[2] = new[] {BoardType.Down, BoardType.Forward, BoardType.Back, BoardType.Left, BoardType.Right};
            possibleBoards[3] = new[] {BoardType.Back, BoardType.Up, BoardType.Down, BoardType.Left, BoardType.Down};
            possibleBoards[4] = new[] {BoardType.Left, BoardType.Forward, BoardType.Up, BoardType.Down, BoardType.Back};
            possibleBoards[5] = new[] {BoardType.Right, BoardType.Forward, BoardType.Up, BoardType.Down, BoardType.Back};
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PushBoards.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasPushBoards && entity.isPathCreator && entity.pushBoards.value > 0;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                for (int i = 0; i < entity.pushBoards.value; i++)
                {
                    var nextBoardType = GetNextBoardType(entity);
                    var nextPosition = GetNextBoardPosition(entity);
                    var nextRotation = GetNextBoardRotation(entity);
                    UpdateDirectPathDelay(entity);

                    var boardEntity = boardFactory.CreateBoard(contexts, nextBoardType);
                    boardEntity.ReplacePosition(nextPosition);
                    boardEntity.ReplaceRotation(nextRotation);
                    boardEntity.isSyncTransform = true;

                    UpdateBoardChain(entity, boardEntity);
                }

                var count = entity.hasCountBoards ? entity.countBoards.value : 0;
                entity.ReplaceCountBoards(count + entity.pushBoards.value);
                entity.RemovePushBoards();
            }
        }

        private BoardType GetNextBoardType(GameEntity entity)
        {
            if (entity.hasLastBoardId == false)
                return BoardType.Forward;

            var currentBoardType = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value).board.value;
            if (entity.hasDirectPathDelay)
                return currentBoardType;
            return possibleBoards[(int) currentBoardType][rand.Next(5)];
        }

        private Vector3 GetNextBoardPosition(GameEntity entity)
        {
            var currentPosition = Vector3.zero;
            var currentRotation = Vector3.zero;
            var currentBoardType = BoardType.Forward;

            if (entity.hasLastBoardId)
            {
                var lastBoardEntity = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value);
                currentPosition = lastBoardEntity.position.value;
                currentRotation = lastBoardEntity.rotation.value;
                currentBoardType = lastBoardEntity.board.value;
            }
            
            var boardSize = contexts.meta.configsEntity.pathConfig.instance.BoardSize;
            switch (currentBoardType)
            {
                case BoardType.Back:
                    return currentPosition + Quaternion.Euler(currentRotation) * Vector3.back * boardSize;
                case BoardType.Down:
                    return currentPosition + Quaternion.Euler(currentRotation) * Vector3.down * boardSize;
                case BoardType.Forward:
                    return currentPosition + Quaternion.Euler(currentRotation) * Vector3.forward * boardSize;
                case BoardType.Left:
                    return currentPosition + Quaternion.Euler(currentRotation) * Vector3.left * boardSize;
                case BoardType.Right:
                    return currentPosition + Quaternion.Euler(currentRotation) * Vector3.right * boardSize;
                case BoardType.Up:
                    return currentPosition + Quaternion.Euler(currentRotation) * Vector3.up * boardSize;
                default:
                    throw new NotImplementedException();
            }
        }

        private Vector3 GetNextBoardRotation(GameEntity entity)
        {
            if (entity.hasLastBoardId)
            {
                var lastBoardEntity = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value);
                return lastBoardEntity.rotation.value;
            }
            return Vector3.zero;
        }
        
        private void UpdateDirectPathDelay(GameEntity entity)
        {
            if (entity.hasDirectPathDelay == false)
            {
                entity.ReplaceDirectPathDelay(rand.Next(5) + 5);
            }
            else
            {
                entity.ReplaceDirectPathDelay(entity.directPathDelay.value - 1);
                if (entity.directPathDelay.value <= 0)
                    entity.RemoveDirectPathDelay();
            }
        }

        private void UpdateBoardChain(GameEntity entity, GameEntity boardEntity)
        {
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
        }
    }
}