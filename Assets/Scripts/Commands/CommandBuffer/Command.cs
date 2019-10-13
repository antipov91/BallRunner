namespace BallRunner.Commands
{
    public interface ICommand
    {
        void Initialize();
        void Execute();
        void Release();
    }
}