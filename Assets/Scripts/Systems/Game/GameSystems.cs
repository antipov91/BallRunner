using BallRunner.Services;

namespace BallRunner.Systems
{
    public class GameSystems : Feature
    {
        public GameSystems(Contexts contexts, UnityServices services)
        {
            Add(new GameEventSystems(contexts));
            Add(new SceneInitializationSystem(contexts));
            Add(new PathControlSystem(contexts));
            Add(new PathBuildingSystem(contexts, services.BoardFactory));
            Add(new PathChainSystem(contexts));
            Add(new PathRemoveSystem(contexts));
            Add(new PathRotationSystem(contexts));
            Add(new PathRemoveNewBoardSystem(contexts));
            Add(new InstantiateSystem(contexts, services.GameService));
            Add(new CommandExecutionSystem(contexts));
            Add(new BallDirectionSystem(contexts));
            Add(new PositionUpdateSystem(contexts));
            Add(new RotationUpdateSystem(contexts));
            Add(new RotationAroundSystem(contexts));
            Add(new LocalRotationUpdateSystem(contexts));
            Add(new AroundRotationSpawnSystem(contexts));
            Add(new SyncTransformSystem(contexts));
            Add(new DestroyedSystem(contexts));
        }
    }
}