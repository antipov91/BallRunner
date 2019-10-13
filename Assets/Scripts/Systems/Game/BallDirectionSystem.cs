using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace BallRunner.Systems
{
    public class BallDirectionSystem : IExecuteSystem
    {
        private readonly Contexts contexts;
        private IGroup<GameEntity> entitiesGroup;

        public BallDirectionSystem(Contexts contexts)
        {
            this.contexts = contexts;
            entitiesGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Ball, GameMatcher.Collision));
        }
        
        public void Execute()
        {
            foreach (var entity in entitiesGroup.GetEntities())
            {
                var collisionEntity = entity.collision.targetInstance;
                if (collisionEntity.hasBoard == false || collisionEntity.hasRotation == false || collisionEntity.hasPosition == false)
                    continue;
                
                var dif = new Vector2(entity.position.value.x - collisionEntity.position.value.x, entity.position.value.z - collisionEntity.position.value.z);
                if (dif.magnitude <= 0.2f)
                {
                    var direction = GetAxis(collisionEntity);
                    entity.ReplaceDirection(direction);
                    entity.ReplaceRotation(Quaternion.LookRotation(direction, Vector3.up).eulerAngles);
                }

                if (contexts.game.pathCreatorEntity.firstBoardId.value != collisionEntity.boardId.value)
                {
                    int count = collisionEntity.boardId.value - contexts.game.pathCreatorEntity.firstBoardId.value;
                    contexts.game.pathCreatorEntity.ReplacePushBoards(count);
                }
            }
        }
        
        private Vector3 GetAxis(GameEntity boardEntity)
        {
            var boardType = boardEntity.board.value;
            var rotation = boardEntity.rotation.value;
            switch (boardType)
            {
                case BoardType.Forward:
                    return Quaternion.Euler(rotation) * Vector3.forward;
                case BoardType.Back:
                    return Quaternion.Euler(rotation) * Vector3.back;
                case BoardType.Right:
                    return Quaternion.Euler(rotation) * Vector3.right;
                case BoardType.Left:
                    return Quaternion.Euler(rotation) * Vector3.left;
                case BoardType.Up:
                    return Quaternion.Euler(rotation) * Vector3.up;
                case BoardType.Down:
                    return Quaternion.Euler(rotation) * Vector3.down;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}