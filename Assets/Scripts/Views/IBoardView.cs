using UnityEngine;

namespace BallRunner.Views
{
    public interface IBoardView : IView
    {
        Vector3 GetDirection();
    }
}