using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace BallRunner.Systems
{
    public class BallDirectionSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;
        
        public BallDirectionSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Collision.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isBall && entity.hasCollision && entity.collision.targetInstance.hasBoardView &&
                   entity.collision.targetInstance.hasPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var collisionEntity = entity.collision.targetInstance;

                var boarSize = contexts.meta.configsEntity.pathConfig.instance.BoardSize;
                var endPoint = collisionEntity.position.value +
                               collisionEntity.boardView.instance.GetDirection().normalized * boarSize / 2f;
                var direction = new Vector3(endPoint.x - entity.position.value.x, 0, endPoint.z - entity.position.value.z);
                entity.ReplaceDirection(direction.normalized);

                if (contexts.game.pathCreatorEntity.firstBoardId.value != collisionEntity.boardId.value)
                {
                    int count = collisionEntity.boardId.value - contexts.game.pathCreatorEntity.firstBoardId.value;
                    contexts.game.pathCreatorEntity.ReplaceRemoveBoards(count);
                }
                
                entity.RemoveCollision();
            }
        }
    }
}