using BallRunner.Systems;
using BallRunner.Views;
using Moq;
using NSpec;

namespace Tests.Game
{
    public class describe_DestroyedSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            DestroyedSystem destroyedSystem = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts = new Contexts();
                destroyedSystem = new DestroyedSystem(contexts);
            };

            context["Given entity with destroy component"] = () =>
            {
                before = () =>
                {
                    entity = contexts.game.CreateEntity();
                    entity.isDestroyed = true;
                    destroyedSystem.Execute();
                };

                it["Must destroy entity"] = () =>
                {
                    entity.isEnabled.should_be_false();
                };
            };

            context["Given entity with destroy component and monoView component"] = () =>
            {
                Mock<IMonoView> monoViewMock = null;

                before = () =>
                {
                    entity = contexts.game.CreateEntity();
                    entity.isDestroyed = true;
                    monoViewMock = new Mock<IMonoView>();
                    entity.ReplaceMonoView(monoViewMock.Object);
                    destroyedSystem.Execute();
                };

                it["Must destroy entity"] = () =>
                {
                    entity.isEnabled.should_be_false();
                };

                it["Must invoke destroy method"] = () =>
                {
                    monoViewMock.Verify(x => x.Destroy(), Times.Once);
                };
            };
        }
    }
}