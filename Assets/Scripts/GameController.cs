using BallRunner.Systems;
using BallRunner.Services;
using UnityEngine;

namespace BallRunner
{
    public class GameController : MonoBehaviour
    {
        private Entitas.Systems systems;
        
        private void Awake()
        {
            var services = new UnityServices();
            var contexts = Contexts.sharedInstance;
            systems = CreateSystems(contexts, services);
            systems.Initialize();
        }

        private Entitas.Systems CreateSystems(Contexts contexts, UnityServices services)
        {
            return new Feature()
                .Add(new MetaSystems(contexts, services))
                .Add(new InputSystems(contexts, services))
                .Add(new TimeSystems(contexts, services))
                .Add(new GameSystems(contexts, services));
        }

        private void Update()
        {
            systems.Execute();
        }
    }
}