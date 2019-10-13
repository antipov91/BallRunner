using BallRunner.Configs;
using Entitas;

namespace BallRunner.Systems
{
    public class ConfigInitializationSystem : IInitializeSystem
    {
        private readonly Contexts contexts;

        public ConfigInitializationSystem(Contexts contexts)
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