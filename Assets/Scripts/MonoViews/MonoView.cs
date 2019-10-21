using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace BallRunner.Views
{
    public abstract class MonoView<T> : MonoBehaviour, IMonoView
    {
        public Contexts Contexts { get; private set; }
        public T Entity { get; private set; }

        private EntityLink entityLink;
        private IEntityLinker<T>[] entityLinkers;

        void IMonoView.Initialize(Contexts contexts, IEntity entity)
        {
            Contexts = contexts;
            Entity = (T) entity;
            entityLink = gameObject.AddComponent<EntityLink>();
            entityLink.Link(entity);

            entityLinkers = GetComponents<IEntityLinker<T>>();
            foreach (var entityLinker in entityLinkers)
                entityLinker.Link(Contexts, Entity);
        }

        public T1 GetViewComponent<T1>() where T1 : IView
        {
            return GetComponent<T1>();
        }

        void IMonoView.Destroy()
        {
            foreach (var entityLinker in entityLinkers)
                entityLinker.Unlink(Contexts, Entity);
            
            entityLink.Unlink();
            Destroy(entityLink);
            Destroy(gameObject);
        }
    }
}