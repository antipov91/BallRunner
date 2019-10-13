using UnityEngine;

namespace BallRunner.Views
{
    public class MonoTransformView : MonoBehaviour, ITransformView
    {
        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Vector3 Rotation
        {
            get { return transform.rotation.eulerAngles; }
            set { transform.rotation = Quaternion.Euler(value); }
        }

        public void RotateAround(Vector3 point, Vector3 axis, float angle)
        {
            transform.RotateAround(point, axis, angle);
        }
    }
}