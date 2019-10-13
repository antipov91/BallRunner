using Entitas;
using UnityEngine;

namespace BallRunner.Systems
{
    public class SceneInitializationSystem : IInitializeSystem
    {
        private readonly Contexts contexts;

        public SceneInitializationSystem(Contexts contexts)
        {
            this.contexts = contexts;
        }
        
        public void Initialize()
        {
            contexts.game.isPathCreator = true;
            var pathCreatorEntity = contexts.game.pathCreatorEntity;
            pathCreatorEntity.ReplacePushBoards(contexts.meta.configsEntity.pathConfig.instance.MaxCountBoards - 1);

            contexts.game.isBall = true;
            var ballEntity = contexts.game.ballEntity;
            ballEntity.ReplaceAsset("Prefabs/Ball");
            ballEntity.isSyncTransform = true;
            ballEntity.ReplaceDirection(Vector3.forward);
            ballEntity.ReplacePosition(new Vector3(0f, 2.5f, 1f));
            ballEntity.ReplaceLinearVelocity(0.2f);
            ballEntity.ReplaceRotation(Vector3.zero);
        }
    }
}