using BallRunner.Services;
using DesperateDevs.Utils;
using Entitas;
using UnityEngine;
using Random = System.Random;

namespace BallRunner.Systems
{
    public class PathBuildingSystem : IExecuteSystem
    {
        private readonly Contexts contexts;
        private readonly IBoardFactory boardFactory;
        private IGroup<GameEntity> entitiesGroup;

        private readonly Vector3[] possibleTurns;
        private Random rand;

        public PathBuildingSystem(Contexts contexts, IBoardFactory boardFactory)
        {
            this.contexts = contexts;
            this.boardFactory = boardFactory;
            entitiesGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PathCreator));
            
            rand = new Random();
            possibleTurns = new[] {90 * Vector3.left, 90 * Vector3.right, 90 * Vector3.up, 90 * Vector3.down};
        }
        
        public void Execute()
        {
            foreach (var entity in entitiesGroup.GetEntities())
            {
                if (entity.hasCountBoards && entity.countBoards.value >= contexts.meta.configsEntity.pathConfig.instance.MaxCountBoards)
                    continue;

                if (entity.hasLastBoardId)
                {
                    var lastBoardEntity = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value);
                    if (lastBoardEntity.hasBoardView == false || lastBoardEntity.hasAroundRotation)
                        continue;
                }
                
                var position = GetNextBoardPosition(entity);
                var rotation = GetNextBoardRotation(entity);
                var turn = GetNextTurn(entity);
                var id = GetNextBoardId(entity);
                UpdateDirectPathDelay(entity);
                    
                var boardEntity = boardFactory.CreateBoard(contexts);
                boardEntity.isBoard = true;
                boardEntity.ReplacePosition(position);
                boardEntity.ReplaceRotation(rotation);
                boardEntity.ReplaceLocalRotation(turn);
                boardEntity.ReplaceBoardId(id);
                boardEntity.isSyncTransform = true;
                
                entity.ReplaceNewBoardId(boardEntity.boardId.value);
            }
        }
        
        private Vector3 GetNextTurn(GameEntity entity)
        {
            if (entity.hasLastBoardId == false || entity.hasDirectPathDelay)
                return Vector3.zero;

            return possibleTurns[rand.Next(4)];
        }

        private Vector3 GetNextBoardPosition(GameEntity entity)
        {
            var direction = Vector3.zero;
            var position = Vector3.zero;
            
            if (entity.hasLastBoardId)
            {
                var lastBoardEntity = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value);
                position = lastBoardEntity.position.value;
                direction = lastBoardEntity.boardView.instance.GetDirection();
            }

            var boardSize = contexts.meta.configsEntity.pathConfig.instance.BoardSize;
            return position + direction.normalized * boardSize;
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

        private int GetNextBoardId(GameEntity entity)
        {
            if (entity.hasLastBoardId == false)
                return 0;
            
            var lastBoardEntity = contexts.game.GetEntityWithBoardId(entity.lastBoardId.value);
            return lastBoardEntity.boardId.value + 1;
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
    }
}