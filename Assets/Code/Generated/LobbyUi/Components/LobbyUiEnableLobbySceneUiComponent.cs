//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiEntity {

    static readonly Code.Scenes.LobbyScene.ECS.EnableLobbySceneUi enableLobbySceneUiComponent = new Code.Scenes.LobbyScene.ECS.EnableLobbySceneUi();

    public bool isEnableLobbySceneUi {
        get { return HasComponent(LobbyUiComponentsLookup.EnableLobbySceneUi); }
        set {
            if (value != isEnableLobbySceneUi) {
                var index = LobbyUiComponentsLookup.EnableLobbySceneUi;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : enableLobbySceneUiComponent;

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
public sealed partial class LobbyUiMatcher {

    static Entitas.IMatcher<LobbyUiEntity> _matcherEnableLobbySceneUi;

    public static Entitas.IMatcher<LobbyUiEntity> EnableLobbySceneUi {
        get {
            if (_matcherEnableLobbySceneUi == null) {
                var matcher = (Entitas.Matcher<LobbyUiEntity>)Entitas.Matcher<LobbyUiEntity>.AllOf(LobbyUiComponentsLookup.EnableLobbySceneUi);
                matcher.componentNames = LobbyUiComponentsLookup.componentNames;
                _matcherEnableLobbySceneUi = matcher;
            }

            return _matcherEnableLobbySceneUi;
        }
    }
}
