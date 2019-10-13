using System.Collections.Generic;
using BallRunner.Services;
using BallRunner.Views;
using Entitas;

namespace BallRunner.Systems
{
    public class InstantiateSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;
        private readonly IGameService gameService;

        public InstantiateSystem(Contexts contexts, IGameService gameService) : base(contexts.game)
        {
            this.contexts = contexts;
            this.gameService = gameService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Asset.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAsset && !entity.hasMonoView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var monoView = gameService.Instantiate(entity.asset.value);
                entity.ReplaceMonoView(monoView);
                InstantiateViews(entity);
                entity.RemoveAsset();
                monoView.Initialize(contexts, entity);
            }
        }
        
        private void InstantiateViews(GameEntity entity)
        {
            var monoView = entity.monoView.instance;
            var transformView = monoView.GetViewComponent<ITransformView>();
            if (!ReferenceEquals(transformView, null))
                entity.ReplaceTransformView(transformView);
        }
    }
}