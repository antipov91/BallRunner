using BallRunner.Systems;
using BallRunner.Services;
using BallRunner.Views;
using Entitas;
using Moq;
using NSpec;

namespace Tests.Game
{
    public class describe_InstantiateSystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            InstantiateSystem instantiateSystem = null;
            Mock<IGameService> gameServiceMock = null;
            Mock<IMonoView> monoViewMock = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts = new Contexts();
                gameServiceMock = new Mock<IGameService>();
                monoViewMock = new Mock<IMonoView>();

                gameServiceMock.Setup(x => x.Instantiate(It.IsAny<string>())).Returns(monoViewMock.Object);
                instantiateSystem = new InstantiateSystem(contexts, gameServiceMock.Object);
            };

            context["Given entity with asset component without monoView component"] = () => 
            { 
                before = () =>
                {
                    entity = contexts.game.CreateEntity();
                    entity.AddAsset("prefabName");
                    instantiateSystem.Execute();
                };

                it["Should invoke instantiate"] = () =>
                {
                    gameServiceMock.Verify(x => x.Instantiate(It.IsAny<string>()), Times.Once);
                };

                it["Should invoke initialize from monoView"] = () =>
                {
                    monoViewMock.Verify(x => x.Initialize(It.IsAny<Contexts>(), It.IsAny<IEntity>()), Times.Once);
                };

                it["Must create a monoView component"] = () =>
                {
                    entity.hasMonoView.should_be_true();
                };

                it["Must remove an asset component"] = () =>
                {
                    entity.hasAsset.should_be_false();
                };
            };

            context["Given entity with asset component and game object with transformView"] = () => 
            { 
                before = () =>
                {
                    entity = contexts.game.CreateEntity();
                    entity.AddAsset("prefabName");
                    var transformViewMock = new Mock<ITransformView>();
                    monoViewMock.Setup(x => x.GetViewComponent<ITransformView>()).Returns(transformViewMock.Object);
                    instantiateSystem.Execute();
                };
                
                it["Must create a transformView component"] = () =>
                {
                    entity.transformView.should_not_be_null();
                };
            };
            
            context["Given entity with asset component and monoView component"] = () => 
            { 
                before = () =>
                {
                    entity = contexts.game.CreateEntity();
                    entity.AddAsset("prefabName");
                    entity.AddMonoView(monoViewMock.Object);
                    instantiateSystem.Execute();
                };

                it["Nothing to do"] = () =>
                {
                    entity.hasAsset.should_be_true();
                    entity.hasMonoView.should_be_true();
                };
            };
        }
    }
}