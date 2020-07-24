//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class WppAccrualEntity {

    public Code.Scenes.LobbyScene.ECS.PositionComponent position { get { return (Code.Scenes.LobbyScene.ECS.PositionComponent)GetComponent(WppAccrualComponentsLookup.Position); } }
    public bool hasPosition { get { return HasComponent(WppAccrualComponentsLookup.Position); } }

    public void AddPosition(UnityEngine.Vector3 newValue) {
        var index = WppAccrualComponentsLookup.Position;
        var component = (Code.Scenes.LobbyScene.ECS.PositionComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.PositionComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePosition(UnityEngine.Vector3 newValue) {
        var index = WppAccrualComponentsLookup.Position;
        var component = (Code.Scenes.LobbyScene.ECS.PositionComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.PositionComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePosition() {
        RemoveComponent(WppAccrualComponentsLookup.Position);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class WppAccrualEntity : IPositionEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class WppAccrualMatcher {

    static Entitas.IMatcher<WppAccrualEntity> _matcherPosition;

    public static Entitas.IMatcher<WppAccrualEntity> Position {
        get {
            if (_matcherPosition == null) {
                var matcher = (Entitas.Matcher<WppAccrualEntity>)Entitas.Matcher<WppAccrualEntity>.AllOf(WppAccrualComponentsLookup.Position);
                matcher.componentNames = WppAccrualComponentsLookup.componentNames;
                _matcherPosition = matcher;
            }

            return _matcherPosition;
        }
    }
}