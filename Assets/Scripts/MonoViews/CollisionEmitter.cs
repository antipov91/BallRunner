using Entitas.Unity;
using UnityEngine;

namespace BallRunner.Views
{
    public class CollisionEmitter : MonoBehaviour
    {
        private MonoView<GameEntity> view;

        private void Start()
        {
            view = GetComponent<MonoView<GameEntity>>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var link = collision.gameObject.GetEntityLink();
            if (!ReferenceEquals(link, null))
            {
                if (link.entity is GameEntity)
                    view.Entity.ReplaceCollision((GameEntity)link.entity);
            }
        }
    }
}