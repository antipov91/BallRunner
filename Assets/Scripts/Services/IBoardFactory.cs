namespace BallRunner.Services
{
    public interface IBoardFactory
    {
        GameEntity CreateBoard(Contexts contexts, BoardType type);
    }
}