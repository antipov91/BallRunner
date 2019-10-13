using BallRunner.Systems;
using BallRunner.Views;
using Moq;
using NSpec;
using UnityEngine;

namespace Tests.Game
{
    public class describe_PositionUpdateSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            PositionUpdateSystem positionUpdateSystem = null;
            Mock<ITransformView> transformViewMock = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts = new Contexts();
                positionUpdateSystem = new PositionUpdateSystem(contexts);
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
                it["Must update position value to {0}".With(expected)] = () =>
                {
                    entity.AddPosition(given);
                    entity.AddTransformView(transformViewMock.Object);
                    positionUpdateSystem.Execute();
                    transformViewMock.VerifySet(x => x.Position = expected, Times.Once());
                };
            });
        }
    }
}