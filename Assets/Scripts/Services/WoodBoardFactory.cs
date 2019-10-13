namespace BallRunner.Services
{
    public class WoodBoardFactory : IBoardFactory
    {
        public GameEntity CreateBoard(Contexts contexts, BoardType type)
        {
            var entity = contexts.game.CreateEntity();
            entity.AddAsset("Prefabs/Cube");
            entity.AddBoard(type);
            return entity;
        }
    }
}