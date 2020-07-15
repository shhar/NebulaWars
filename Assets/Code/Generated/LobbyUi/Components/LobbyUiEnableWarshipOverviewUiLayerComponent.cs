//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiEntity {

    public Code.Scenes.LobbyScene.ECS.Components.EnableWarshipOverviewUiLayerComponent enableWarshipOverviewUiLayer { get { return (Code.Scenes.LobbyScene.ECS.Components.EnableWarshipOverviewUiLayerComponent)GetComponent(LobbyUiComponentsLookup.EnableWarshipOverviewUiLayer); } }
    public bool hasEnableWarshipOverviewUiLayer { get { return HasComponent(LobbyUiComponentsLookup.EnableWarshipOverviewUiLayer); } }

    public void AddEnableWarshipOverviewUiLayer(NetworkLibrary.NetworkLibrary.Http.WarshipDto newWarshipDto) {
        var index = LobbyUiComponentsLookup.EnableWarshipOverviewUiLayer;
        var component = (Code.Scenes.LobbyScene.ECS.Components.EnableWarshipOverviewUiLayerComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.EnableWarshipOverviewUiLayerComponent));
        component.WarshipDto = newWarshipDto;
        AddComponent(index, component);
    }

    public void ReplaceEnableWarshipOverviewUiLayer(NetworkLibrary.NetworkLibrary.Http.WarshipDto newWarshipDto) {
        var index = LobbyUiComponentsLookup.EnableWarshipOverviewUiLayer;
        var component = (Code.Scenes.LobbyScene.ECS.Components.EnableWarshipOverviewUiLayerComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.EnableWarshipOverviewUiLayerComponent));
        component.WarshipDto = newWarshipDto;
        ReplaceComponent(index, component);
    }

    public void RemoveEnableWarshipOverviewUiLayer() {
        RemoveComponent(LobbyUiComponentsLookup.EnableWarshipOverviewUiLayer);
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

    static Entitas.IMatcher<LobbyUiEntity> _matcherEnableWarshipOverviewUiLayer;

    public static Entitas.IMatcher<LobbyUiEntity> EnableWarshipOverviewUiLayer {
        get {
            if (_matcherEnableWarshipOverviewUiLayer == null) {
                var matcher = (Entitas.Matcher<LobbyUiEntity>)Entitas.Matcher<LobbyUiEntity>.AllOf(LobbyUiComponentsLookup.EnableWarshipOverviewUiLayer);
                matcher.componentNames = LobbyUiComponentsLookup.componentNames;
                _matcherEnableWarshipOverviewUiLayer = matcher;
            }

            return _matcherEnableWarshipOverviewUiLayer;
        }
    }
}
