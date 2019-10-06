namespace BallRunner.AppServices
{
    public class Services
    {
        public IGameService GameService { get; }
        public IInputService InputService { get; }

        public Services()
        {
            GameService = new UnityGameService();
            InputService = new UnityInputService();
        }
    }
}