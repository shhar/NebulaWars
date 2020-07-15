//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Scenes.BattleScene.ECS.Components.Game.HiddenComponent hiddenComponent = new Code.Scenes.BattleScene.ECS.Components.Game.HiddenComponent();

    public bool isHidden {
        get { return HasComponent(GameComponentsLookup.Hidden); }
        set {
            if (value != isHidden) {
                var index = GameComponentsLookup.Hidden;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : hiddenComponent;

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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherHidden;

    public static Entitas.IMatcher<GameEntity> Hidden {
        get {
            if (_matcherHidden == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Hidden);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHidden = matcher;
            }

            return _matcherHidden;
        }
    }
}
