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
                var axis = boardEntity.boardView.instance.GetDirection();
                var angle = entity.pathRotation.value;
                var rotationSpeed = contexts.meta.configsEntity.pathConfig.instance.PathRotationSpeed;
                var delay = contexts.meta.configsEntity.pathConfig.instance.PathRotationDelay;

                boardEntity.ReplaceAroundRotationEmitter(delay, point, axis, angle);
            }
        }
    }
}