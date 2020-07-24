//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class WppAccrualContext {

    public WppAccrualEntity warshipPowerPointsEntity { get { return GetGroup(WppAccrualMatcher.WarshipPowerPoints).GetSingleEntity(); } }
    public Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent warshipPowerPoints { get { return warshipPowerPointsEntity.warshipPowerPoints; } }
    public bool hasWarshipPowerPoints { get { return warshipPowerPointsEntity != null; } }

    public WppAccrualEntity SetWarshipPowerPoints(int newValue, int newMaxValueForLevel) {
        if (hasWarshipPowerPoints) {
            throw new Entitas.EntitasException("Could not set WarshipPowerPoints!\n" + this + " already has an entity with Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent!",
                "You should check if the context already has a warshipPowerPointsEntity before setting it or use context.ReplaceWarshipPowerPoints().");
        }
        var entity = CreateEntity();
        entity.AddWarshipPowerPoints(newValue, newMaxValueForLevel);
        return entity;
    }

    public void ReplaceWarshipPowerPoints(int newValue, int newMaxValueForLevel) {
        var entity = warshipPowerPointsEntity;
        if (entity == null) {
            entity = SetWarshipPowerPoints(newValue, newMaxValueForLevel);
        } else {
            entity.ReplaceWarshipPowerPoints(newValue, newMaxValueForLevel);
        }
    }

    public void RemoveWarshipPowerPoints() {
        warshipPowerPointsEntity.Destroy();
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
public partial class WppAccrualEntity {

    public Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent warshipPowerPoints { get { return (Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent)GetComponent(WppAccrualComponentsLookup.WarshipPowerPoints); } }
    public bool hasWarshipPowerPoints { get { return HasComponent(WppAccrualComponentsLookup.WarshipPowerPoints); } }

    public void AddWarshipPowerPoints(int newValue, int newMaxValueForLevel) {
        var index = WppAccrualComponentsLookup.WarshipPowerPoints;
        var component = (Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent)CreateComponent(index, typeof(Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent));
        component.value = newValue;
        component.maxValueForLevel = newMaxValueForLevel;
        AddComponent(index, component);
    }

    public void ReplaceWarshipPowerPoints(int newValue, int newMaxValueForLevel) {
        var index = WppAccrualComponentsLookup.WarshipPowerPoints;
        var component = (Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent)CreateComponent(index, typeof(Code.Scenes.LootboxScene.PrefabScripts.Wpp.ECS.WarshipPowerPointsComponent));
        component.value = newValue;
        component.maxValueForLevel = newMaxValueForLevel;
        ReplaceComponent(index, component);
    }

    public void RemoveWarshipPowerPoints() {
        RemoveComponent(WppAccrualComponentsLookup.WarshipPowerPoints);
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
public sealed partial class WppAccrualMatcher {

    static Entitas.IMatcher<WppAccrualEntity> _matcherWarshipPowerPoints;

    public static Entitas.IMatcher<WppAccrualEntity> WarshipPowerPoints {
        get {
            if (_matcherWarshipPowerPoints == null) {
                var matcher = (Entitas.Matcher<WppAccrualEntity>)Entitas.Matcher<WppAccrualEntity>.AllOf(WppAccrualComponentsLookup.WarshipPowerPoints);
                matcher.componentNames = WppAccrualComponentsLookup.componentNames;
                _matcherWarshipPowerPoints = matcher;
            }

            return _matcherWarshipPowerPoints;
        }
    }
}