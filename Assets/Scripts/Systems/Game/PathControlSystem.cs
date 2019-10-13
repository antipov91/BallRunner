using Entitas;

namespace BallRunner.Systems
{
    public class PathControlSystem : IExecuteSystem
    {
        private readonly Contexts contexts;

        public PathControlSystem(Contexts contexts)
        {
            this.contexts = contexts;
        }
        
        public void Execute()
        {
            var inputEntity = contexts.input.inputEntity;
            if (inputEntity.left.isDown)
                contexts.game.pathCreatorEntity.ReplacePathRotation(-90);
            if (inputEntity.right.isDown)
                contexts.game.pathCreatorEntity.ReplacePathRotation(90);
        }
    }
}