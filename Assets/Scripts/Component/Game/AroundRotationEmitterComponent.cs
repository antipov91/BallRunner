using Entitas;
using UnityEngine;

[Game]
public sealed class AroundRotationEmitterComponent : IComponent
{
    public float time;
    public Vector3 point;
    public Vector3 axis;
    public float deltaAngle;
}