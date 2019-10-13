using Entitas;
using UnityEngine;

namespace BallRunner.Views
{
    public interface IMonoView : IView
    {
        void Initialize(Contexts contexts, IEntity entity);

        T GetViewComponent<T>() where T : IView;
        
        void Destroy();
    }
}