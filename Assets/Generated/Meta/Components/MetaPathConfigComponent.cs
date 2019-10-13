//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MetaEntity {

    public PathConfigComponent pathConfig { get { return (PathConfigComponent)GetComponent(MetaComponentsLookup.PathConfig); } }
    public bool hasPathConfig { get { return HasComponent(MetaComponentsLookup.PathConfig); } }

    public void AddPathConfig(BallRunner.Configs.PathConfig newInstance) {
        var index = MetaComponentsLookup.PathConfig;
        var component = (PathConfigComponent)CreateComponent(index, typeof(PathConfigComponent));
        component.instance = newInstance;
        AddComponent(index, component);
    }

    public void ReplacePathConfig(BallRunner.Configs.PathConfig newInstance) {
        var index = MetaComponentsLookup.PathConfig;
        var component = (PathConfigComponent)CreateComponent(index, typeof(PathConfigComponent));
        component.instance = newInstance;
        ReplaceComponent(index, component);
    }

    public void RemovePathConfig() {
        RemoveComponent(MetaComponentsLookup.PathConfig);
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
public sealed partial class MetaMatcher {

    static Entitas.IMatcher<MetaEntity> _matcherPathConfig;

    public static Entitas.IMatcher<MetaEntity> PathConfig {
        get {
            if (_matcherPathConfig == null) {
                var matcher = (Entitas.Matcher<MetaEntity>)Entitas.Matcher<MetaEntity>.AllOf(MetaComponentsLookup.PathConfig);
                matcher.componentNames = MetaComponentsLookup.componentNames;
                _matcherPathConfig = matcher;
            }

            return _matcherPathConfig;
        }
    }
}
