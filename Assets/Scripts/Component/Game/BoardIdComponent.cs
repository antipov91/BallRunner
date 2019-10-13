using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class BoardIdComponent : IComponent
{
     [PrimaryEntityIndex] 
     public int value;
}