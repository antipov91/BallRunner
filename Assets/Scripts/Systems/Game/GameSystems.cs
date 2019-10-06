using BallRunner.Services;

namespace BallRunner.GameSystems
{
    public class GameSystems : Feature
    {
        public GameSystems(Contexts contexts, UnityServices services)
        {
            Add(new InstantiateSystem(contexts, services.GameService));
            Add(new PositionUpdateSystem(contexts));
            Add(new RotationUpdateSystem(contexts));
        }
    }
}