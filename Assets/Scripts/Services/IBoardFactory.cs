namespace BallRunner.Services
{
    public interface IBoardService
    {
        GameEntity CreateBoard(BoardType type);
    }
}