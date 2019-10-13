using BallRunner.Views;

namespace BallRunner.Services
{
    public interface IGameService
    {
        IMonoView Instantiate(string asset);
    }
}