using Entitas;
using Entitas.CodeGeneration.Attributes;

[Time]
[Unique]
public sealed class DeltaTimeComponent : IComponent
{
    public float value;
}