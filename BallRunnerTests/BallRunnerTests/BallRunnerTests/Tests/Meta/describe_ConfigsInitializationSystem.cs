using BallRunner.Configs;
using BallRunner.Systems;
using NSpec;

namespace Tests.Meta
{
    public class describe_ConfigsInitializationSystem : nspec
    {
        public void when_initialized()
        {
            Contexts contexts = null;
            ConfigsInitializationSystem configsInitializationSystem = null;

            before = () =>
            {
                contexts = new Contexts();
                configsInitializationSystem = new ConfigsInitializationSystem(contexts);
                configsInitializationSystem.Initialize();
            };

            it["Must create config component"] = () =>
            {
                contexts.meta.configsEntity.should_not_be_null();
            };

            it["Must add path config component"] = () =>
            {
                contexts.meta.configsEntity.hasPathConfig.should_be_true();
            };
        }
    }
}