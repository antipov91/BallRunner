using System;
using System.Collections.Generic;
using BallRunner.Commands;
using Entitas;
using UnityEngine;

namespace BallRunner.Systems
{
    public class PathRotationSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;

        public PathRotationSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PathRotation);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPathCreator && entity.hasFirstBoardId && entity.hasPathRotation;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var boardEntity = contexts.game.GetEntityWithBoardId(entity.firstBoardId.value);
                
                var point = boardEntity.position.value;
                var axis = GetAxis(boardEntity);
                var angle = entity.pathRotation.value;
                var rotationSpeed = contexts.meta.configsEntity.pathConfig.instance.PathRotationSpeed;
                var delay = 0f;

                bool hasNextBoard = true;
                while (hasNextBoard)
                {
                    var rotateCommand =
                        new AroundRotationCommand(contexts, boardEntity, point, axis, angle, rotationSpeed);
                    var delayCommand = new DelayCommand(contexts, delay);
                    if (boardEntity.hasCommandBuffer == false)
                        boardEntity.ReplaceCommandBuffer(new CommandBuffer());
                    boardEntity.commandBuffer.instance.Add(delayCommand);
                    boardEntity.commandBuffer.instance.Add(rotateCommand);
                    delay += contexts.meta.configsEntity.pathConfig.instance.PathRotationDelay;

                    hasNextBoard = boardEntity.hasNextBoardId;
                    if (hasNextBoard)
                        boardEntity = contexts.game.GetEntityWithBoardId(boardEntity.nextBoardId.value);
                };
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