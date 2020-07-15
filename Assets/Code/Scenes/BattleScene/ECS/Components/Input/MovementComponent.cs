﻿using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Scenes.BattleScene.ECS.Components.Input
{
    [Input, Unique]
    public class MovementComponent:IComponent
    {
        public float x;
        public float y;
    }
}