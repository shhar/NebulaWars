﻿using Entitas;
using Entitas.Unity;
using Plugins.submodules.SharedCode.Logger;
using UnityEngine;

namespace Code.Scenes.BattleScene.ECS
{
    public class DestroyViewSystem : ICleanupSystem
    {
        private readonly IGroup<ServerGameEntity> destroyedGroup;
        private readonly ILog log = LogManager.CreateLogger(typeof(DestroyViewSystem));

        public DestroyViewSystem(Contexts contexts)
        {
            destroyedGroup = contexts.serverGame.GetGroup(ServerGameMatcher.Destroyed);
        }

        public void Cleanup()
        {
            var destroyed = destroyedGroup.GetEntities();
            for (int index = 0; index < destroyed.Length; index++)
            {
                var entity = destroyed[index];
                if (entity.hasView)
                {
                    GameObject gameObject = entity.view.gameObject;
                    if (gameObject != null)
                    {
                        gameObject.Unlink();
                        Object.Destroy(gameObject);   
                    }
                    
                    // log.Debug("Уничтожение объекта "+entity.view.gameObject.name);
                }
                
                entity.Destroy();
            }
        }
    }
}