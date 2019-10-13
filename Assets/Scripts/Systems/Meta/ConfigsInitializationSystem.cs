using BallRunner.Configs;
using Entitas;

namespace BallRunner.Systems
{
    public class ConfigsInitializationSystem : IInitializeSystem
    {
        private readonly Contexts contexts;

        public ConfigsInitializationSystem(Contexts contexts)
        {
            this.contexts = contexts;
        }
        
        public void Initialize()
        {
            contexts.meta.isConfigs = true;
            var entity = contexts.meta.configsEntity;
            
            entity.ReplacePathConfig(new PathConfig());
        }
    }
}