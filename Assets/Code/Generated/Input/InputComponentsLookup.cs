//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class InputComponentsLookup {

    public const int Id = 0;
    public const int Attack = 1;
    public const int Movement = 2;
    public const int TryingToUseAbility = 3;

    public const int TotalComponents = 4;

    public static readonly string[] componentNames = {
        "Id",
        "Attack",
        "Movement",
        "TryingToUseAbility"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Code.Scenes.BattleScene.ECS.Components.IdComponent),
        typeof(Code.Scenes.BattleScene.ECS.Components.Input.AttackComponent),
        typeof(Code.Scenes.BattleScene.ECS.Components.Input.MovementComponent),
        typeof(Code.Scenes.BattleScene.ECS.Components.Input.TryingToUseAbilityComponent)
    };
}
