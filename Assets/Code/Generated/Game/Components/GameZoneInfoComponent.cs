//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity zoneInfoEntity { get { return GetGroup(GameMatcher.ZoneInfo).GetSingleEntity(); } }
    public Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent zoneInfo { get { return zoneInfoEntity.zoneInfo; } }
    public bool hasZoneInfo { get { return zoneInfoEntity != null; } }

    public GameEntity SetZoneInfo(UnityEngine.Vector2 newPosition, float newRadius) {
        if (hasZoneInfo) {
            throw new Entitas.EntitasException("Could not set ZoneInfo!\n" + this + " already has an entity with Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent!",
                "You should check if the context already has a zoneInfoEntity before setting it or use context.ReplaceZoneInfo().");
        }
        var entity = CreateEntity();
        entity.AddZoneInfo(newPosition, newRadius);
        return entity;
    }

    public void ReplaceZoneInfo(UnityEngine.Vector2 newPosition, float newRadius) {
        var entity = zoneInfoEntity;
        if (entity == null) {
            entity = SetZoneInfo(newPosition, newRadius);
        } else {
            entity.ReplaceZoneInfo(newPosition, newRadius);
        }
    }

    public void RemoveZoneInfo() {
        zoneInfoEntity.Destroy();
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
public partial class GameEntity {

    public Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent zoneInfo { get { return (Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent)GetComponent(GameComponentsLookup.ZoneInfo); } }
    public bool hasZoneInfo { get { return HasComponent(GameComponentsLookup.ZoneInfo); } }

    public void AddZoneInfo(UnityEngine.Vector2 newPosition, float newRadius) {
        var index = GameComponentsLookup.ZoneInfo;
        var component = (Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent));
        component.position = newPosition;
        component.radius = newRadius;
        AddComponent(index, component);
    }

    public void ReplaceZoneInfo(UnityEngine.Vector2 newPosition, float newRadius) {
        var index = GameComponentsLookup.ZoneInfo;
        var component = (Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.Game.ZoneInfoComponent));
        component.position = newPosition;
        component.radius = newRadius;
        ReplaceComponent(index, component);
    }

    public void RemoveZoneInfo() {
        RemoveComponent(GameComponentsLookup.ZoneInfo);
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

    static Entitas.IMatcher<GameEntity> _matcherZoneInfo;

    public static Entitas.IMatcher<GameEntity> ZoneInfo {
        get {
            if (_matcherZoneInfo == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ZoneInfo);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherZoneInfo = matcher;
            }

            return _matcherZoneInfo;
        }
    }
}
