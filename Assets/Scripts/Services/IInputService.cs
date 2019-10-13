using Systems.InputSystems;

namespace BallRunner.Services
{
    public interface IInputService
    {
        KeyButton RightButton { get; }
        KeyButton LeftButton { get; }
        KeyButton JumpButton { get; }
    }
}