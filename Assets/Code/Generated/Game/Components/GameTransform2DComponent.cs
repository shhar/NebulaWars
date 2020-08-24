//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Code.Scenes.BattleScene.ECS.Components.Game.Transform2DComponent transform2D { get { return (Code.Scenes.BattleScene.ECS.Components.Game.Transform2DComponent)GetComponent(GameComponentsLookup.Transform2D); } }
    public bool hasTransform2D { get { return HasComponent(GameComponentsLookup.Transform2D); } }

    public void AddTransform2D(UnityEngine.Vector3 newPosition, float newAngle) {
        var index = GameComponentsLookup.Transform2D;
        var component = (Code.Scenes.BattleScene.ECS.Components.Game.Transform2DComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.Game.Transform2DComponent));
        component.position = newPosition;
        component.angle = newAngle;
        AddComponent(index, component);
    }

    public void ReplaceTransform2D(UnityEngine.Vector3 newPosition, float newAngle) {
        var index = GameComponentsLookup.Transform2D;
        var component = (Code.Scenes.BattleScene.ECS.Components.Game.Transform2DComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.Game.Transform2DComponent));
        component.position = newPosition;
        component.angle = newAngle;
        ReplaceComponent(index, component);
    }

    public void RemoveTransform2D() {
        RemoveComponent(GameComponentsLookup.Transform2D);
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

    static Entitas.IMatcher<GameEntity> _matcherTransform2D;

    public static Entitas.IMatcher<GameEntity> Transform2D {
        get {
            if (_matcherTransform2D == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Transform2D);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTransform2D = matcher;
            }

            return _matcherTransform2D;
        }
    }
}