using BallRunner.Systems;
using BallRunner.Views;
using NSpec;
using Moq;
using UnityEngine;

namespace Tests.Game
{
    public class describe_RotationUpdateSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            RotationUpdateSystem rotationUpdateSystem = null;
            Mock<ITransformView> transformViewMock = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts = new Contexts();
                rotationUpdateSystem = new RotationUpdateSystem(contexts);
                transformViewMock = new Mock<ITransformView>();
                entity = contexts.game.CreateEntity();
            };

            new Each<Vector3, Vector3>
            {
                {Vector3.back, Vector3.back},
                {Vector3.forward, Vector3.forward},
                {Vector3.up, Vector3.up}
            }.Do((given, expected) =>
            {
                it["Must update rotation value to {0}".With(expected)] = () =>
                {
                    entity.AddRotation(given);
                    entity.AddTransformView(transformViewMock.Object);
                    rotationUpdateSystem.Execute();
                    transformViewMock.VerifySet(x => x.Rotation = expected, Times.Once());
                };
            });
        }
    }
}