using BallRunner.Views;
using UnityEngine;

namespace BallRunner.Services
{
    public class UnityGameService : IGameService
    {
        public IMonoView Instantiate(string asset)
        {
            var go =  GameObject.Instantiate(Resources.Load(asset)) as GameObject;
            return go.GetComponent<IMonoView>();
        }
    }
}