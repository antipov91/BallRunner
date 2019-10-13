using BallRunner.Services;

namespace BallRunner.Systems
{
    public class InputSystems : Feature
    {
        public InputSystems(Contexts contexts, UnityServices services)
        {
            Add(new InputSystem(contexts, services.InputService));
        }
    }
}