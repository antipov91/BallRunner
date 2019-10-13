using UnityEngine;

namespace BallRunner.Commands
{
    public class AroundRotationCommand : Command
    {
        private readonly Contexts contexts;
        private readonly GameEntity entity;

        private Vector3 point;
        private Vector3 axis;
        private float angle;
        private float angularVelocity;

        public AroundRotationCommand(Contexts contexts, GameEntity entity, Vector3 point, Vector3 axis, float angle, float angularVelocity) : base()
        {
            this.contexts = contexts;
            this.entity = entity;

            this.point = point;
            this.axis = axis;
            this.angle = angle;
            this.angularVelocity = angularVelocity;
        }

        public override void Initialize()
        {
            entity.ReplaceAroundRotation(point, axis, angle);
            entity.ReplaceAngularVelocity(angularVelocity);
        }

        public override void Execute()
        {
            if (entity.hasAroundRotation == false)
                IsComplete = true;
        }
    }
}