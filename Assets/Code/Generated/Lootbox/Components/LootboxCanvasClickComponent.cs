//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LootboxEntity {

    static readonly DefaultNamespace.CanvasClickComponent canvasClickComponent = new DefaultNamespace.CanvasClickComponent();

    public bool isCanvasClick {
        get { return HasComponent(LootboxComponentsLookup.CanvasClick); }
        set {
            if (value != isCanvasClick) {
                var index = LootboxComponentsLookup.CanvasClick;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : canvasClickComponent;

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
public sealed partial class LootboxMatcher {

    static Entitas.IMatcher<LootboxEntity> _matcherCanvasClick;

    public static Entitas.IMatcher<LootboxEntity> CanvasClick {
        get {
            if (_matcherCanvasClick == null) {
                var matcher = (Entitas.Matcher<LootboxEntity>)Entitas.Matcher<LootboxEntity>.AllOf(LootboxComponentsLookup.CanvasClick);
                matcher.componentNames = LootboxComponentsLookup.componentNames;
                _matcherCanvasClick = matcher;
            }

            return _matcherCanvasClick;
        }
    }
}
