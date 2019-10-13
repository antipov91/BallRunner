using Entitas;

namespace BallRunner.Systems
{
    public class CameraSystem : IExecuteSystem
    {
        private readonly Contexts contexts;
        
        public CameraSystem(Contexts contexts)
        {
            contexts = this.contexts;
        }
        
        public void Execute()
        {
            
        }
    }
}