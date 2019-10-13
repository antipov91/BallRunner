namespace BallRunner.Configs
{
    public class PathConfig
    {
        public int MaxCountBoards { get; set; }
        public float BoardSize { get; set; }
        public float PathRotationSpeed { get; set; }
        public float PathRotationDelay { get; set; }

        public PathConfig()
        {
            MaxCountBoards = 200;
            BoardSize = 1f;
            PathRotationSpeed = 400f;
            PathRotationDelay = 0.005f;
        }
    }
}