using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace BallRunner.Views
{
    public class MonoView<T> : MonoBehaviour, IMonoView
    {
        public Contexts Contexts { get; private set; }
        public T Entity { get; private set; }

        private EntityLink entityLink;

        void IMonoView.Initialize(Contexts contexts, IEntity entity)
        {
            Contexts = contexts;
            Entity = (T) entity;
            entityLink = gameObject.AddComponent<EntityLink>();
            entityLink.Link(entity);
        }

        public T1 GetViewComponent<T1>() where T1 : IView
        {
            return GetComponent<T1>();
        }

        void IMonoView.Destroy()
        {
            entityLink.Unlink();
            Destroy(entityLink);
            Destroy(gameObject);
        }
    }
}