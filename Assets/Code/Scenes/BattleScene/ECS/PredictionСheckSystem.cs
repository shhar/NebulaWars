﻿using System;
using Code.Common.Storages;
using Code.Scenes.BattleScene.Experimental.Prediction;
using Entitas;
using Plugins.submodules.SharedCode;
using Plugins.submodules.SharedCode.LagCompensation;
using Plugins.submodules.SharedCode.Logger;

namespace Code.Scenes.BattleScene.ECS
{
    /// <summary>
    /// Применяет новую информацию с сервера.
    /// </summary>
    public class PredictionСheckSystem:IExecuteSystem
    {
        private int lastSavedTickNumber;
        private readonly ISnapshotCatalog snapshotCatalog;
        private readonly PredictionManager predictionManager;
        private readonly ILog log = LogManager.CreateLogger(typeof(PredictionСheckSystem));
        
        public PredictionСheckSystem(ISnapshotCatalog snapshotCatalog, PredictionManager predictionManager)
        {
            this.snapshotCatalog = snapshotCatalog;
            this.predictionManager = predictionManager;
        }
        
        public void Execute()
        {
            try
            {
                int newestTickNumber = snapshotCatalog.GetNewestTickNumber();
                //Пришла новая информация
                if (lastSavedTickNumber < newestTickNumber)
                {
                    //Обновить локальный счётчик
                    lastSavedTickNumber = newestTickNumber;
                    SnapshotWithLastInputId newest = snapshotCatalog.GetNewestSnapshot();
                    ushort playerEntityId = PlayerIdStorage.PlayerEntityId;
                    if (playerEntityId == 0)
                    {
                        //todo изменить порядок установки playerEntityId
                        throw new Exception("PlayerEntityId не установлен");
                    }
                    //проверить, что игрок правильно предсказан или пересоздать текущее состояние
                    predictionManager.Reconcile(newest, playerEntityId);
                }
                else
                {
                    //новый тик от сервера не пришёл
                }
            }
            catch (Exception e)
            {
                log.Error(e.FullMessage());
            }
        }
    }
}