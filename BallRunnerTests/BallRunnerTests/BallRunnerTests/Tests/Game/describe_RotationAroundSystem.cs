using BallRunner.Systems;
using BallRunner.Views;
using Moq;
using NSpec;
using UnityEngine;

namespace Tests.Game
{
    public class describe_RotationAroundSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            RotationAroundSystem rotationAroundSystem = null;
            Mock<ITransformView> transformViewMock = null;
            GameEntity entity = null;
            
            before = () =>
            {
                contexts = new Contexts();
                transformViewMock = new Mock<ITransformView>();
                rotationAroundSystem = new RotationAroundSystem(contexts);
                contexts.time.ReplaceDeltaTime(1f);
                
                entity = contexts.game.CreateEntity();
                entity.ReplacePosition(Vector3.zero);
                entity.ReplaceRotation(Vector3.zero);
                entity.ReplaceTransformView(transformViewMock.Object);
            };

            context["Given entity with around component and angular velocity component"] = () =>
            {
                before = () =>
                {
                    entity.ReplaceAroundRotation(Vector3.forward, Vector3.up, 2f);
                    entity.ReplaceAngularVelocity(1f);
                    transformViewMock.SetupGet(x => x.Position).Returns(Vector3.forward);
                    transformViewMock.SetupGet(x => x.Rotation).Returns(Vector3.up);
                    rotationAroundSystem.Execute();
                };

                it["Must invoke RotateAround method"] = () =>
                {
                    transformViewMock.Verify(
                        x => x.RotateAround(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.IsAny<float>()),
                        Times.Once);
                };

                it["Must update rotation around component"] = () =>
                {
                    entity.aroundRotation.deltaAngle.should_be(1f);
                };
            };
            
            context["Given entity with around component and angular velocity component. Delta angle value less then speed"] = () =>
            {
                before = () =>
                {
                    entity.ReplaceAroundRotation(Vector3.forward, Vector3.up, 2f);
                    entity.ReplaceAngularVelocity(10f);
                    transformViewMock.SetupGet(x => x.Position).Returns(Vector3.forward);
                    transformViewMock.SetupGet(x => x.Rotation).Returns(Vector3.up);
                    rotationAroundSystem.Execute();
                };

                it["Must remove rotation around component"] = () =>
                {
                    entity.hasAroundRotation.should_be_false();
                };

                it["Must invoke RotateAround with angle value of 2"] = () =>
                {
                    transformViewMock.Verify(x => x.RotateAround(It.IsAny<Vector3>(), It.IsAny<Vector3>(), It.Is<float>(p => p == 2f)), Times.Once);
                };
            };
        }
    }
}