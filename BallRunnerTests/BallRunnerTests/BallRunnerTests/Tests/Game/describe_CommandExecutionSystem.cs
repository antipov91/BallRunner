using BallRunner.Commands;
using BallRunner.Systems;
using Moq;
using NSpec;

namespace Tests.Game
{
    public class describe_CommandExecutionSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            CommandExecutionSystem commandExecutionSystem = null;
            Mock<ICommandBuffer> commandBufferMock = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts = new Contexts();
                commandExecutionSystem = new CommandExecutionSystem(contexts);
                commandBufferMock = new Mock<ICommandBuffer>();
                commandBufferMock.Setup(x => x.IsEmpty()).Returns(false);
                
                entity = contexts.game.CreateEntity();
                entity.ReplaceCommandBuffer(commandBufferMock.Object);
                
                commandExecutionSystem.Execute();
            };

            it["Must call execute method"] = () =>
            {
                commandBufferMock.Verify(x => x.Execute(), Times.Once);
            };

            context["Given empty command buffer"] = () => 
            { 
                before = () =>
                {
                    commandBufferMock = new Mock<ICommandBuffer>();
                    commandBufferMock.Setup(x => x.IsEmpty()).Returns(true);
                    entity.ReplaceCommandBuffer(commandBufferMock.Object);
                    
                    commandExecutionSystem.Execute();
                };

                it["Must remove command buffer component"] = () =>
                {
                    entity.hasCommandBuffer.should_be_false();
                };

                it["Should not call a method"] = () =>
                {
                    commandBufferMock.Verify(x => x.Execute(), Times.Never);
                };
            };
        }
    }
}