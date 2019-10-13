using BallRunner.Services;
using Entitas;

namespace BallRunner.Systems
{
    public class InputSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly IInputService inputService;
        private readonly Contexts contexts;
        private InputEntity inputEntity;
        public InputSystem(Contexts contexts, IInputService inputService)
        {
            this.contexts = contexts;
            this.inputService = inputService;
        }
        
        public void Initialize()
        {
            contexts.input.isInput = true;
            inputEntity = contexts.input.inputEntity;
            inputEntity.ReplaceLeft(false, false, false);
            inputEntity.ReplaceRight(false, false, false);
            inputEntity.ReplaceJump(false, false, false);
        }

        public void Execute()
        {
            inputEntity.ReplaceLeft(inputService.LeftButton.IsUp, inputService.LeftButton.IsDown, inputService.LeftButton.IsPressed);
            inputEntity.ReplaceRight(inputService.RightButton.IsUp, inputService.RightButton.IsDown, inputService.RightButton.IsPressed);
            inputEntity.ReplaceJump(inputService.JumpButton.IsUp, inputService.JumpButton.IsDown, inputService.JumpButton.IsPressed);
        }
    }
}