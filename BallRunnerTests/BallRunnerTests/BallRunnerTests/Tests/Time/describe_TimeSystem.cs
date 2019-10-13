using BallRunner.Services;
using BallRunner.Systems;
using Moq;
using NSpec;

namespace Tests.Time
{
    public class describe_TimeSystem : nspec
    {
        public void when_initialized()
        {
            Contexts contexts = null;
            Mock<ITimeService> timeServiceMock = null;
            TimeSystem timeSystem = null;

            before = () =>
            {
                contexts = new Contexts();
                timeServiceMock = new Mock<ITimeService>();
                timeSystem = new TimeSystem(contexts, timeServiceMock.Object);
                timeSystem.Initialize();
            };

            it["Delta time component must be 0"] = () =>
            {
                contexts.time.deltaTime.should_not_be(0f);
            };
        }

        public void when_execute()
        {
            Contexts contexts = null;
            Mock<ITimeService> timeServiceMock = null;
            TimeSystem timeSystem = null;

            before = () =>
            {
                contexts = new Contexts();
                timeServiceMock = new Mock<ITimeService>();
                timeSystem = new TimeSystem(contexts, timeServiceMock.Object);
                timeSystem.Initialize();
            };

            new Each<float, float>
            {
                {1f, 1f},
                {2f, 2f},
                {1.5f, 1.5f}
            }.Do((given, expected) =>
            {
                it["Delta time component must be {0}".With(expected)] = () =>
                {
                    timeServiceMock.SetupGet(x => x.DeltaTime).Returns(given);
                    timeSystem.Execute();
                    contexts.time.deltaTime.value.should_be(expected);
                };
            });
        }
    }
}