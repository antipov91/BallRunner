//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public JumpComponent jump { get { return (JumpComponent)GetComponent(InputComponentsLookup.Jump); } }
    public bool hasJump { get { return HasComponent(InputComponentsLookup.Jump); } }

    public void AddJump(bool newIsUp, bool newIsDown, bool newIsPressed) {
        var index = InputComponentsLookup.Jump;
        var component = (JumpComponent)CreateComponent(index, typeof(JumpComponent));
        component.isUp = newIsUp;
        component.isDown = newIsDown;
        component.isPressed = newIsPressed;
        AddComponent(index, component);
    }

    public void ReplaceJump(bool newIsUp, bool newIsDown, bool newIsPressed) {
        var index = InputComponentsLookup.Jump;
        var component = (JumpComponent)CreateComponent(index, typeof(JumpComponent));
        component.isUp = newIsUp;
        component.isDown = newIsDown;
        component.isPressed = newIsPressed;
        ReplaceComponent(index, component);
    }

    public void RemoveJump() {
        RemoveComponent(InputComponentsLookup.Jump);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherJump;

    public static Entitas.IMatcher<InputEntity> Jump {
        get {
            if (_matcherJump == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Jump);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherJump = matcher;
            }

            return _matcherJump;
        }
    }
}
