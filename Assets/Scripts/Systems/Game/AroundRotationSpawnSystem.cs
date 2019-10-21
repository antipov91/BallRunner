using Entitas;

namespace BallRunner.Systems
{
    public class AroundRotationSpawnSystem : IExecuteSystem
    {
        private readonly Contexts contexts;
        private IGroup<GameEntity> entitiesGroup;

        public AroundRotationSpawnSystem(Contexts contexts)
        {
            this.contexts = contexts;
            entitiesGroup = contexts.game.GetGroup(GameMatcher.AroundRotationEmitter);
        }
        
        public void Execute()
        {
            var spawnTime = contexts.meta.configsEntity.pathConfig.instance.PathRotationDelay;

            foreach (var entity in entitiesGroup.GetEntities())
            {
                var time = entity.aroundRotationEmitter.time - contexts.time.deltaTime.value;
                if (time > 0)
                {
                    entity.ReplaceAroundRotationEmitter(time, entity.aroundRotationEmitter.point, entity.aroundRotationEmitter.axis, entity.aroundRotationEmitter.deltaAngle);
                }
                else
                {
                    if (entity.hasNextBoardId)
                    {
                        var nextEntity = contexts.game.GetEntityWithBoardId(entity.nextBoardId.value);
                        if (nextEntity.hasAroundRotationEmitter == false)
                            nextEntity.ReplaceAroundRotationEmitter(spawnTime, entity.aroundRotationEmitter.point, entity.aroundRotationEmitter.axis, entity.aroundRotationEmitter.deltaAngle);
                    }
                    
                    entity.ReplaceAroundRotation(entity.aroundRotationEmitter.point, entity.aroundRotationEmitter.axis, entity.aroundRotationEmitter.deltaAngle);
                    entity.ReplaceAngularVelocity(contexts.meta.configsEntity.pathConfig.instance.PathRotationSpeed);
                    entity.RemoveAroundRotationEmitter();
                }
            }
        }
    }
}