using BallRunner.Systems;
using NSpec;
using UnityEngine;

namespace Tests.Game
{
    public class describe_AroundRotationSpawnSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            AroundRotationSpawnSystem aroundRotationSpawnSystem = null;
            GameEntity firstEntity = null;
            GameEntity secondEntity = null;
            
            before = () =>
            {
                contexts = new Contexts();
                aroundRotationSpawnSystem = new AroundRotationSpawnSystem(contexts);

                var configInitializationSystem = new ConfigsInitializationSystem(contexts);
                configInitializationSystem.Initialize();
                
                firstEntity = contexts.game.CreateEntity();
                firstEntity.ReplaceAroundRotationEmitter(1f, Vector3.zero, Vector3.forward, 90f);
                firstEntity.ReplaceNextBoardId(1);
                
                secondEntity = contexts.game.CreateEntity();
                secondEntity.ReplaceBoardId(1);
                
                contexts.time.ReplaceDeltaTime(0.1f);
            };

            context["Spawn time has not come"] = () =>
            {
                before = () =>
                {
                    aroundRotationSpawnSystem.Execute();
                };
                
                it["Must update time"] = () =>
                {
                    firstEntity.aroundRotationEmitter.time.should_be(0.9f);
                };

                it["Should not add around rotation component"] = () =>
                {
                    firstEntity.hasAroundRotation.should_be_false();
                };

                it["Should not add angular velocity component"] = () =>
                {
                    firstEntity.hasAngularVelocity.should_be_false();
                };

                it["Should not add around rotation emitter component to second entity"] = () =>
                {
                    secondEntity.hasAroundRotationEmitter.should_be_false();
                };
            };

            context["Spawn time has come"] = () => 
            { 
                before = () =>
                {
                    contexts.time.ReplaceDeltaTime(2f);
                    aroundRotationSpawnSystem.Execute();
                };

                it["Must remove around rotation emitter component"] = () =>
                {
                    firstEntity.hasAroundRotationEmitter.should_be_false();
                };

                it["Must add around rotation component"] = () =>
                {
                    firstEntity.hasAroundRotation.should_be_true();
                    
                    firstEntity.aroundRotation.point.should_be(Vector3.zero);
                    firstEntity.aroundRotation.axis.should_be(Vector3.forward);
                    firstEntity.aroundRotation.deltaAngle.should_be(90f);
                };

                it["Must add angular velocity component"] = () =>
                {
                    firstEntity.hasAngularVelocity.should_be_true();
                };

                it["Must add around rotation emitter component to second entity"] = () =>
                {
                    secondEntity.hasAroundRotationEmitter.should_be_true();
                    
                    secondEntity.aroundRotationEmitter.point.should_be(Vector3.zero);
                    secondEntity.aroundRotationEmitter.axis.should_be(Vector3.forward);
                    secondEntity.aroundRotationEmitter.deltaAngle.should_be(90f);
                };
            };
        }
    }
}