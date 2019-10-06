using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace BallRunner.Views
{
    public class MonoView<T> : MonoBehaviour, IMonoView
    {
        protected Contexts contexts;
        protected T entity;

        private EntityLink entityLink;
        
        void IMonoView.Initialize(Contexts contexts, IEntity entity)
        {
            this.contexts = contexts;
            this.entity = (T) entity;
            entityLink = gameObject.AddComponent<EntityLink>();
            entityLink.Link(entity);
        }

        void IMonoView.Destroy()
        {
            entityLink.Unlink();
            Destroy(entityLink);
        }
    }
}