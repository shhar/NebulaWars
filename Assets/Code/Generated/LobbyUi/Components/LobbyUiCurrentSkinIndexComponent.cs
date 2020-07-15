//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiContext {

    public LobbyUiEntity currentSkinIndexEntity { get { return GetGroup(LobbyUiMatcher.CurrentSkinIndex).GetSingleEntity(); } }
    public Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex currentSkinIndex { get { return currentSkinIndexEntity.currentSkinIndex; } }
    public bool hasCurrentSkinIndex { get { return currentSkinIndexEntity != null; } }

    public LobbyUiEntity SetCurrentSkinIndex(int newIndex) {
        if (hasCurrentSkinIndex) {
            throw new Entitas.EntitasException("Could not set CurrentSkinIndex!\n" + this + " already has an entity with Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex!",
                "You should check if the context already has a currentSkinIndexEntity before setting it or use context.ReplaceCurrentSkinIndex().");
        }
        var entity = CreateEntity();
        entity.AddCurrentSkinIndex(newIndex);
        return entity;
    }

    public void ReplaceCurrentSkinIndex(int newIndex) {
        var entity = currentSkinIndexEntity;
        if (entity == null) {
            entity = SetCurrentSkinIndex(newIndex);
        } else {
            entity.ReplaceCurrentSkinIndex(newIndex);
        }
    }

    public void RemoveCurrentSkinIndex() {
        currentSkinIndexEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiEntity {

    public Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex currentSkinIndex { get { return (Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex)GetComponent(LobbyUiComponentsLookup.CurrentSkinIndex); } }
    public bool hasCurrentSkinIndex { get { return HasComponent(LobbyUiComponentsLookup.CurrentSkinIndex); } }

    public void AddCurrentSkinIndex(int newIndex) {
        var index = LobbyUiComponentsLookup.CurrentSkinIndex;
        var component = (Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex));
        component.index = newIndex;
        AddComponent(index, component);
    }

    public void ReplaceCurrentSkinIndex(int newIndex) {
        var index = LobbyUiComponentsLookup.CurrentSkinIndex;
        var component = (Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.CurrentSkinIndex));
        component.index = newIndex;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentSkinIndex() {
        RemoveComponent(LobbyUiComponentsLookup.CurrentSkinIndex);
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

    static Entitas.IMatcher<LobbyUiEntity> _matcherCurrentSkinIndex;

    public static Entitas.IMatcher<LobbyUiEntity> CurrentSkinIndex {
        get {
            if (_matcherCurrentSkinIndex == null) {
                var matcher = (Entitas.Matcher<LobbyUiEntity>)Entitas.Matcher<LobbyUiEntity>.AllOf(LobbyUiComponentsLookup.CurrentSkinIndex);
                matcher.componentNames = LobbyUiComponentsLookup.componentNames;
                _matcherCurrentSkinIndex = matcher;
            }

            return _matcherCurrentSkinIndex;
        }
    }
}
