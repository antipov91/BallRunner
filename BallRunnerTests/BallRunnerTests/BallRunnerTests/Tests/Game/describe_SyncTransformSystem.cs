using BallRunner.Systems;
using BallRunner.Views;
using Moq;
using NSpec;
using UnityEngine;

namespace Tests.Game
{
    public class describe_SyncTransformSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            SyncTransformSystem syncTransformSystem = null;
            Mock<ITransformView> transformViewMock = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts= new Contexts();
                syncTransformSystem = new SyncTransformSystem(contexts);
                transformViewMock = new Mock<ITransformView>();
                entity = contexts.game.CreateEntity();
            };

            context["Given entity with transform view component and sync transform component"] = () =>
                {
                    before = () =>
                    {
                        entity.ReplaceTransformView(transformViewMock.Object);
                        transformViewMock.SetupGet(x => x.Position).Returns(Vector3.forward);
                        transformViewMock.SetupGet(x => x.Rotation).Returns(Vector3.down);
                        entity.isSyncTransform = true;
                        syncTransformSystem.Execute();
                    };

                    it["Must update position component"] = () =>
                    {
                        entity.position.value.should_be(Vector3.forward);
                    };

                    it["Must update rotation component"] = () =>
                    {
                        entity.rotation.value.should_be(Vector3.down);
                    };
                };
        }
    }
}