using UnityEngine;

namespace BallRunner.Views
{
    public interface ITransformView : IView
    {
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
        void RotateAround(Vector3 point, Vector3 axis, float angle);
    }
}