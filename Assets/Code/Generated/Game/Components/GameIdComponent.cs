//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Scenes.BattleScene.ECS.Components.IdComponent id { get { return (Code.Scenes.BattleScene.ECS.Components.IdComponent)GetComponent(GameComponentsLookup.Id); } }
    public bool hasId { get { return HasComponent(GameComponentsLookup.Id); } }

    public void AddId(ushort newValue) {
        var index = GameComponentsLookup.Id;
        var component = (Code.Scenes.BattleScene.ECS.Components.IdComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.IdComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceId(ushort newValue) {
        var index = GameComponentsLookup.Id;
        var component = (Code.Scenes.BattleScene.ECS.Components.IdComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.IdComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveId() {
        RemoveComponent(GameComponentsLookup.Id);
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
public partial class GameEntity : IIdEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherId;

    public static Entitas.IMatcher<GameEntity> Id {
        get {
            if (_matcherId == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Id);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherId = matcher;
            }

            return _matcherId;
        }
    }
}
