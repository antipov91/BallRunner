using UnityEngine;

namespace BallRunner.Views
{
    public class MonoRigidBodyTransformView : MonoBehaviour, ITransformView
    {
        public Vector3 Position
        {
            get
            {
                if (ReferenceEquals(rigidBody, null))
                    return transform.position;
                
                return rigidBody.position;
            }
            set { transform.position = value; }
        }

        public Vector3 Rotation
        {
            get { return transform.rotation.eulerAngles;}
            set { transform.rotation = Quaternion.Euler(value);}
        }

        private Rigidbody rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        public void RotateAround(Vector3 point, Vector3 axis, float angle)
        {
            transform.RotateAround(point, axis, angle);
        }
    }
}