using UnityEngine;

namespace BallRunner.Views
{
    public class MonoBallTransformView : MonoBehaviour, ITransformView, IEntityLinker<GameEntity>, ILinearVelocityListener, IDirectionListener
    {
        public Vector3 Position
        {
            get { return rb.position; }
            set { rb.MovePosition(value); }
        }

        public Vector3 Rotation
        {
            get { return rb.rotation.eulerAngles; }
            set { rb.MoveRotation(Quaternion.Euler(value)); }
        }

        public Vector3 Direction { get; set; }

        private Rigidbody rb;
        private float velocity = 0f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Direction = Vector3.zero;
        }
        
        public void RotateAround(Vector3 point, Vector3 axis, float angle)
        {
            transform.RotateAround(point, axis, angle);
        }

        public void LocalRotate(Vector3 angles)
        {
            transform.localRotation = Quaternion.Euler(angles);
        }

        public void Link(Contexts contexts, GameEntity entity)
        {
            entity.AddDirectionListener(this);
            entity.AddLinearVelocityListener(this);

            if (entity.hasDirection)
                Direction = entity.direction.value;
            if (entity.hasLinearVelocity)
                velocity = entity.linearVelocity.value;
        }

        public void OnLinearVelocity(GameEntity entity, float value)
        {
            velocity = value;
        }

        public void OnDirection(GameEntity entity, Vector3 value)
        {
            Direction = value;
        }
        
        public void Unlink(Contexts contexts, GameEntity entity)
        {
            entity.RemoveDirectionListener(this);
            entity.RemoveLinearVelocityListener(this);
        }
        
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + Direction.normalized * velocity * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Direction, Vector3.up), 4f * Time.deltaTime);
        }
    }
}