using Systems.InputSystems;
using UnityEngine;

namespace BallRunner.Services
{
    public class UnityInputService : IInputService
    {
        public KeyButton RightButton { get; private set; }
        public KeyButton LeftButton { get; private set; }
        public KeyButton JumpButton { get; private set; }

        public UnityInputService()
        {
            RightButton = new KeyButton(() => Input.GetKeyUp(KeyCode.RightArrow), () => Input.GetKeyDown(KeyCode.RightArrow), () => Input.GetKey(KeyCode.RightArrow));
            LeftButton = new KeyButton(() => Input.GetKeyUp(KeyCode.LeftArrow), () => Input.GetKeyDown(KeyCode.LeftArrow), () => Input.GetKey(KeyCode.LeftArrow));
            JumpButton = new KeyButton(() => Input.GetKeyUp(KeyCode.UpArrow), () => Input.GetKeyDown(KeyCode.UpArrow), () => Input.GetKey(KeyCode.UpArrow));
        }
    }
}