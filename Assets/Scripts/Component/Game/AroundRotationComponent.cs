using Entitas;
using UnityEngine;

[Game]
public sealed class AroundRotationComponent : IComponent
{
    public Vector3 point;
    public Vector3 axis;
    public float deltaAngle;
}