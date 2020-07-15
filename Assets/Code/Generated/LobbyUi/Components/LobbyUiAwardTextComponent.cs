//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class LobbyUiEntity {

    public Code.Scenes.LobbyScene.ECS.Components.AwardTextComponent awardText { get { return (Code.Scenes.LobbyScene.ECS.Components.AwardTextComponent)GetComponent(LobbyUiComponentsLookup.AwardText); } }
    public bool hasAwardText { get { return HasComponent(LobbyUiComponentsLookup.AwardText); } }

    public void AddAwardText(int newQuantity, UnityEngine.Vector3 newStartPosition, System.DateTime newCreationTime, System.DateTime newFadeTime) {
        var index = LobbyUiComponentsLookup.AwardText;
        var component = (Code.Scenes.LobbyScene.ECS.Components.AwardTextComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.AwardTextComponent));
        component.quantity = newQuantity;
        component.startPosition = newStartPosition;
        component.CreationTime = newCreationTime;
        component.FadeTime = newFadeTime;
        AddComponent(index, component);
    }

    public void ReplaceAwardText(int newQuantity, UnityEngine.Vector3 newStartPosition, System.DateTime newCreationTime, System.DateTime newFadeTime) {
        var index = LobbyUiComponentsLookup.AwardText;
        var component = (Code.Scenes.LobbyScene.ECS.Components.AwardTextComponent)CreateComponent(index, typeof(Code.Scenes.LobbyScene.ECS.Components.AwardTextComponent));
        component.quantity = newQuantity;
        component.startPosition = newStartPosition;
        component.CreationTime = newCreationTime;
        component.FadeTime = newFadeTime;
        ReplaceComponent(index, component);
    }

    public void RemoveAwardText() {
        RemoveComponent(LobbyUiComponentsLookup.AwardText);
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

    static Entitas.IMatcher<LobbyUiEntity> _matcherAwardText;

    public static Entitas.IMatcher<LobbyUiEntity> AwardText {
        get {
            if (_matcherAwardText == null) {
                var matcher = (Entitas.Matcher<LobbyUiEntity>)Entitas.Matcher<LobbyUiEntity>.AllOf(LobbyUiComponentsLookup.AwardText);
                matcher.componentNames = LobbyUiComponentsLookup.componentNames;
                _matcherAwardText = matcher;
            }

            return _matcherAwardText;
        }
    }
}