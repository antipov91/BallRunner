using BallRunner.Systems;
using NSpec;

namespace Tests.Game
{
    public class describe_DestroySystem : nspec
    {
        public void when_execute()
        {
            Contexts contexts = null;
            DestroySystem destroySystem = null;
            GameEntity entity = null;

            before = () =>
            {
                contexts = new Contexts();
                destroySystem = new DestroySystem(contexts);
            };

            context["Given entity with destroy component"] = () =>
            {
                before = () =>
                {
                    entity = contexts.game.CreateEntity();
                    //entity.isDestroy = true;
                };
                
                
            };
            //it["Given "]
        }
    }
}