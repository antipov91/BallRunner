namespace BallRunner.Services
{
    public class UnityServices
    {
        public IGameService GameService { get; }
        public IInputService InputService { get; }
        public ITimeService TimeService { get; }
        public IBoardFactory BoardFactory { get; }

        public UnityServices()
        {
            GameService = new UnityGameService();
            InputService = new UnityInputService();
            TimeService = new UnityTimeService();
            BoardFactory = new WoodBoardFactory();
        }
    }
}