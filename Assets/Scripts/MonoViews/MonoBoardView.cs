using UnityEngine;

namespace BallRunner.Views
{
    public class MonoBoardView : MonoBehaviour, IBoardView
    {
        public Vector3 GetDirection()
        {
            return transform.forward;
        }
    }
}