//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiEntity {

    public Code.Scenes.LobbyScene.ECS.Components.ScaleComponent scale { get { return (Code.Scenes.LobbyScene.ECS.Components.ScaleComponent)GetComponent(LobbyUiComponentsLookup.Scale); } }
    public bool hasScale { get { return HasComponent(LobbyUiComponentsLookup.Scale); } }

    public void AddScale(UnityEngine.Vector3 newScale) {
        var index = LobbyUiComponentsLookup.Scale;
        var component = (Code.Scenes.LobbyScene.ECS.Components.ScaleComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.ScaleComponent));
        component.scale = newScale;
        AddComponent(index, component);
    }

    public void ReplaceScale(UnityEngine.Vector3 newScale) {
        var index = LobbyUiComponentsLookup.Scale;
        var component = (Code.Scenes.LobbyScene.ECS.Components.ScaleComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.ScaleComponent));
        component.scale = newScale;
        ReplaceComponent(index, component);
    }

    public void RemoveScale() {
        RemoveComponent(LobbyUiComponentsLookup.Scale);
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
public sealed partial class LobbyUiMatcher {

    static Entitas.IMatcher<LobbyUiEntity> _matcherScale;

    public static Entitas.IMatcher<LobbyUiEntity> Scale {
        get {
            if (_matcherScale == null) {
                var matcher = (Entitas.Matcher<LobbyUiEntity>)Entitas.Matcher<LobbyUiEntity>.AllOf(LobbyUiComponentsLookup.Scale);
                matcher.componentNames = LobbyUiComponentsLookup.componentNames;
                _matcherScale = matcher;
            }

            return _matcherScale;
        }
    }
}
