using BallRunner.Services;

namespace BallRunner.Systems
{
    public class MetaSystems : Feature
    {
        public MetaSystems(Contexts contexts, UnityServices service)
        {
            Add(new ConfigsInitializationSystem(contexts));
        }
    }
}