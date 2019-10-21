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

            contexts.game.isBall = true;
            var ballEntity = contexts.game.ballEntity;
            ballEntity.ReplaceAsset("Prefabs/Ball");
            ballEntity.isSyncTransform = true;
            ballEntity.ReplaceDirection(Vector3.forward);
            ballEntity.ReplacePosition(new Vector3(0f, 1.5f, 0f));
            ballEntity.ReplaceLinearVelocity(5.2f);
            ballEntity.ReplaceRotation(Vector3.zero);
        }
    }
}