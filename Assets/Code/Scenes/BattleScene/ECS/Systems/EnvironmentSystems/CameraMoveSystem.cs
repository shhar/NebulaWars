﻿using Code.Common.Storages;
using Entitas;
using Plugins.submodules.SharedCode.Logger;
using UnityEngine;

namespace Code.Scenes.BattleScene.ECS.Systems.EnvironmentSystems
{
    public class CameraMoveSystem : IExecuteSystem
    {
        private readonly Camera mainCamera;
        private readonly Vector3 cameraShift;
        private readonly ServerGameContext gameContext;
        private readonly ILog log = LogManager.CreateLogger(typeof(CameraMoveSystem));

        public CameraMoveSystem(Contexts contexts, Camera camera, Vector3 cameraShift)
        {
            mainCamera = camera;
            this.cameraShift = cameraShift;
            gameContext = contexts.serverGame;
        }

        public void Execute()
        {
            ServerGameEntity playerEntity = gameContext.GetEntityWithId(PlayerIdStorage.PlayerEntityId);
            if (playerEntity == null)
            {
                log.Debug("playerEntity is null");
                return;
            }
            
            if (!playerEntity.hasTransform)
            {
                log.Debug("playerEntity dont have hasViewTransform");
                return;
            }

            Vector3 playerPosition = playerEntity.transform.value.position;
            Transform cameraTransform = mainCamera.transform;
            Vector3 targetPosition = playerPosition + cameraShift;

            Vector3 currentPosition = cameraTransform.position;
            Vector3 currentVelocity = Vector3.zero;
            float smoothTime = 0.07f;
            cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref currentVelocity,
                smoothTime);
            mainCamera.transform.LookAt(cameraTransform.position - cameraShift);
        }
    }
}