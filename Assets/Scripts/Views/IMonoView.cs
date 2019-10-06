using Entitas;

namespace BallRunner.Views
{
    public interface IView
    {
        void Initialize(Contexts contexts, IEntity entity);
        void Destroy();
    }
}