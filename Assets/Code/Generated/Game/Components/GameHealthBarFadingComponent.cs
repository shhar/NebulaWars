// //------------------------------------------------------------------------------
// // <auto-generated>
// //     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
// //
// //     Changes to this file may cause incorrect behavior and will be lost if
// //     the code is regenerated.
// // </auto-generated>
// //------------------------------------------------------------------------------
// public partial class GameEntity {
//
//     public Code.Scenes.BattleScene.ECS.Components.Game.HealthBarFadingComponent healthBarFading { get { return (Code.Scenes.BattleScene.ECS.Components.Game.HealthBarFadingComponent)GetComponent(GameComponentsLookup.HealthBarFading); } }
//     public bool hasHealthBarFading { get { return HasComponent(GameComponentsLookup.HealthBarFading); } }
//
//     public void AddHealthBarFading(float newPercentage) {
//         var index = GameComponentsLookup.HealthBarFading;
//         var component = (Code.Scenes.BattleScene.ECS.Components.Game.HealthBarFadingComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.Game.HealthBarFadingComponent));
//         component.percentage = newPercentage;
//         AddComponent(index, component);
//     }
//
//     public void ReplaceHealthBarFading(float newPercentage) {
//         var index = GameComponentsLookup.HealthBarFading;
//         var component = (Code.Scenes.BattleScene.ECS.Components.Game.HealthBarFadingComponent)CreateComponent(index, typeof(Code.Scenes.BattleScene.ECS.Components.Game.HealthBarFadingComponent));
//         component.percentage = newPercentage;
//         ReplaceComponent(index, component);
//     }
//
//     public void RemoveHealthBarFading() {
//         RemoveComponent(GameComponentsLookup.HealthBarFading);
//     }
// }
//
// //------------------------------------------------------------------------------
// // <auto-generated>
// //     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
// //
// //     Changes to this file may cause incorrect behavior and will be lost if
// //     the code is regenerated.
// // </auto-generated>
// //------------------------------------------------------------------------------
// public sealed partial class GameMatcher {
//
//     static Entitas.IMatcher<GameEntity> _matcherHealthBarFading;
//
//     public static Entitas.IMatcher<GameEntity> HealthBarFading {
//         get {
//             if (_matcherHealthBarFading == null) {
//                 var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HealthBarFading);
//                 matcher.componentNames = GameComponentsLookup.componentNames;
//                 _matcherHealthBarFading = matcher;
//             }
//
//             return _matcherHealthBarFading;
//         }
//     }
// }