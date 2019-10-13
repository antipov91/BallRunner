using BallRunner.Services;

namespace BallRunner.Systems
{
    public class TimeSystems : Feature
    {
        public TimeSystems(Contexts contexts, UnityServices service)
        {
            Add(new TimeSystem(contexts, service.TimeService));
        }
    }
}