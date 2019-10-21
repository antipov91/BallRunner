using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Event (EventTarget.Self)]
public sealed class LinearVelocityComponent : IComponent
{
     public float value;
}