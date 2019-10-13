using BallRunner.Services;
using Entitas;

namespace BallRunner.Systems
{
    public class TimeSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly Contexts contexts;
        private readonly ITimeService timeService;

        public TimeSystem(Contexts contexts, ITimeService timeService)
        {
            this.contexts = contexts;
            this.timeService = timeService;
        }
        
        public void Initialize()
        {
            contexts.time.ReplaceDeltaTime(0);
        }

        public void Execute()
        {
            contexts.time.ReplaceDeltaTime(timeService.DeltaTime);
        }
    }
}