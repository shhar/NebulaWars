//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiContext {

    public LobbyUiEntity blockWarshipsShiftToTheRightEntity { get { return GetGroup(LobbyUiMatcher.BlockWarshipsShiftToTheRight).GetSingleEntity(); } }

    public bool isBlockWarshipsShiftToTheRight {
        get { return blockWarshipsShiftToTheRightEntity != null; }
        set {
            var entity = blockWarshipsShiftToTheRightEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isBlockWarshipsShiftToTheRight = true;
                } else {
                    entity.Destroy();
                }
            }
        }
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

    static readonly Code.Scenes.LobbyScene.ECS.Components.BlockWarshipsShiftToTheRight blockWarshipsShiftToTheRightComponent = new Code.Scenes.LobbyScene.ECS.Components.BlockWarshipsShiftToTheRight();

    public bool isBlockWarshipsShiftToTheRight {
        get { return HasComponent(LobbyUiComponentsLookup.BlockWarshipsShiftToTheRight); }
        set {
            if (value != isBlockWarshipsShiftToTheRight) {
                var index = LobbyUiComponentsLookup.BlockWarshipsShiftToTheRight;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : blockWarshipsShiftToTheRightComponent;

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

    static Entitas.IMatcher<LobbyUiEntity> _matcherBlockWarshipsShiftToTheRight;

    public static Entitas.IMatcher<LobbyUiEntity> BlockWarshipsShiftToTheRight {
        get {
            if (_matcherBlockWarshipsShiftToTheRight == null) {
                var matcher = (Entitas.Matcher<LobbyUiEntity>)Entitas.Matcher<LobbyUiEntity>.AllOf(LobbyUiComponentsLookup.BlockWarshipsShiftToTheRight);
                matcher.componentNames = LobbyUiComponentsLookup.componentNames;
                _matcherBlockWarshipsShiftToTheRight = matcher;
            }

            return _matcherBlockWarshipsShiftToTheRight;
        }
    }
}
