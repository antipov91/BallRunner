using Entitas;

[Input]
public sealed class LeftComponent : IComponent
{
    public bool isUp;
    public bool isDown;
    public bool isPressed;
}