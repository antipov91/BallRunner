using NSpec;
using BallRunner.Services;
using BallRunner.GameSystems;
using Moq;

namespace Tests.Input
{
    public class describe_InputSystem : nspec
    {
        public void when_initialized()
        {
            Contexts contexts = null;
            Mock<IInputService> inputServiceMock = null;
            InputSystem inputSystem = null;

            before = () =>
            {
                contexts = new Contexts();
                inputServiceMock = new Mock<IInputService>();
                inputSystem = new InputSystem(contexts, inputServiceMock.Object);
                inputSystem.Initialize();
            };

            it["Must create an input entity"] = () =>
            {
                contexts.input.inputEntity.should_be_null();
            };
        }
    }
}