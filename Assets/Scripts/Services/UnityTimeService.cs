using UnityEngine;

namespace BallRunner.Services
{
    public class UnityTimeService : ITimeService
    {
        public float DeltaTime
        {
            get { return Time.deltaTime; }
        }
    }
}