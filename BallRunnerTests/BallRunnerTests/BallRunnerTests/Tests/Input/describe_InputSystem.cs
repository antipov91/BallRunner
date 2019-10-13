using Systems.InputSystems;
using NSpec;
using BallRunner.Services;
using BallRunner.Systems;
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
                contexts.input.inputEntity.should_not_be_null();
            };

            it["Must add right component"] = () =>
            {
                contexts.input.inputEntity.hasRight.should_be_true();
            };

            it["Must add left component"] = () =>
            {
                contexts.input.inputEntity.hasLeft.should_be_true();
            };

            it["Must add jump component"] = () =>
            {
                contexts.input.inputEntity.hasJump.should_be_true();
            };
        }

        public void when_execute()
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

            new Each<bool, bool, bool>()
            {
                {false, true, true},
                {true, false, false}
            }.Do((isUp, isDown, isPressed) =>
            {
                context["Given key button state: isUp: {0}, isDown: {1}, isPressed: {2}".With(isUp, isDown, isPressed)] = () =>
                    {
                        before = () =>
                        {
                            KeyButton keyButton = new KeyButton(() => isUp, () => isDown, () => isPressed);
                            inputServiceMock.SetupGet(x => x.LeftButton).Returns(keyButton);
                            inputServiceMock.SetupGet(x => x.JumpButton).Returns(keyButton);
                            inputServiceMock.SetupGet(x => x.RightButton).Returns(keyButton);
                    
                            inputSystem.Execute();
                        };

                        it["Right component must have a value: isUp: {0}, isDown: {1}, isPressed: {2}".With(isUp, isDown, isPressed)] = () =>
                        {
                            contexts.input.inputEntity.right.isUp.should_be(isUp);
                            contexts.input.inputEntity.right.isPressed.should_be(isPressed);
                            contexts.input.inputEntity.right.isDown.should_be(isDown);
                        };
                
                        it["Left component must have a value: isUp: {0}, isDown: {1}, isPressed: {2}".With(isUp, isDown, isPressed)] = () =>
                        {
                            contexts.input.inputEntity.left.isUp.should_be(isUp);
                            contexts.input.inputEntity.left.isPressed.should_be(isPressed);
                            contexts.input.inputEntity.left.isDown.should_be(isDown);
                        };
                
                        it["Jump component must have a value: isUp: {0}, isDown: {1}, isPressed: {2}".With(isUp, isDown, isPressed)] = () =>
                        {
                            contexts.input.inputEntity.jump.isUp.should_be(isUp);
                            contexts.input.inputEntity.jump.isPressed.should_be(isPressed);
                            contexts.input.inputEntity.jump.isDown.should_be(isDown);
                        };  
                    };
            });
        }
    }
}