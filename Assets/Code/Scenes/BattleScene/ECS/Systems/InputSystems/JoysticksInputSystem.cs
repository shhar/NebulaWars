﻿using System;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Code.BattleScene.ECS.Systems
{
    public class JoysticksInputSystem : IExecuteSystem, ICleanupSystem
    {
        private readonly InputContext inputContext;
        private readonly Joystick movementJoystick;
        private readonly Joystick attackJoystick;

        public JoysticksInputSystem(Contexts contexts, Joystick forMovement, Joystick forAttack)
        {
            inputContext = contexts.input;
            movementJoystick = forMovement;
            attackJoystick = forAttack;
        }

        public void Execute()
        {
            inputContext.SetMovement(movementJoystick.Horizontal, movementJoystick.Vertical);
#if UNITY_EDITOR_WIN
            var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(inputVector.sqrMagnitude > 0f)inputContext.ReplaceMovement(inputVector.x, inputVector.y);
#endif
            if (Math.Abs(attackJoystick.Horizontal) > 0.001 && Math.Abs(attackJoystick.Vertical) > 0.001)
            {
                var attackAngle = Mathf.Atan2(attackJoystick.Vertical, attackJoystick.Horizontal) * Mathf.Rad2Deg;
                inputContext.SetAttack(attackAngle);
            }
        }

        public void Cleanup()
        {
            inputContext.RemoveMovement();
            if(inputContext.hasAttack) inputContext.RemoveAttack();
        }
    }
}