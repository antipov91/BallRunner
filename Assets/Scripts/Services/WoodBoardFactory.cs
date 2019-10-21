namespace BallRunner.Services
{
    public class WoodBoardFactory : IBoardFactory
    {
        public GameEntity CreateBoard(Contexts contexts)
        {
            var entity = contexts.game.CreateEntity();
            entity.AddAsset("Prefabs/Cube");
            return entity;
        }
    }
}