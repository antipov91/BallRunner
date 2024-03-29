//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public RightComponent right { get { return (RightComponent)GetComponent(InputComponentsLookup.Right); } }
    public bool hasRight { get { return HasComponent(InputComponentsLookup.Right); } }

    public void AddRight(bool newIsUp, bool newIsDown, bool newIsPressed) {
        var index = InputComponentsLookup.Right;
        var component = (RightComponent)CreateComponent(index, typeof(RightComponent));
        component.isUp = newIsUp;
        component.isDown = newIsDown;
        component.isPressed = newIsPressed;
        AddComponent(index, component);
    }

    public void ReplaceRight(bool newIsUp, bool newIsDown, bool newIsPressed) {
        var index = InputComponentsLookup.Right;
        var component = (RightComponent)CreateComponent(index, typeof(RightComponent));
        component.isUp = newIsUp;
        component.isDown = newIsDown;
        component.isPressed = newIsPressed;
        ReplaceComponent(index, component);
    }

    public void RemoveRight() {
        RemoveComponent(InputComponentsLookup.Right);
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

    static Entitas.IMatcher<InputEntity> _matcherRight;

    public static Entitas.IMatcher<InputEntity> Right {
        get {
            if (_matcherRight == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Right);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherRight = matcher;
            }

            return _matcherRight;
        }
    }
}
