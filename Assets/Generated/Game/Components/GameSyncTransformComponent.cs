//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly SyncTransformComponent syncTransformComponent = new SyncTransformComponent();

    public bool isSyncTransform {
        get { return HasComponent(GameComponentsLookup.SyncTransform); }
        set {
            if (value != isSyncTransform) {
                var index = GameComponentsLookup.SyncTransform;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : syncTransformComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherSyncTransform;

    public static Entitas.IMatcher<GameEntity> SyncTransform {
        get {
            if (_matcherSyncTransform == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SyncTransform);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSyncTransform = matcher;
            }

            return _matcherSyncTransform;
        }
    }
}
