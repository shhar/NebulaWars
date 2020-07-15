using System;
using Code.Scenes.LobbyScene.ECS.Components;
using Entitas;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;
using CommandToCreateAwardImagesComponent = Code.Scenes.LobbyScene.ECS.Components.CommandToCreateAwardImagesComponent;
using MovingAwardComponent = Code.Scenes.LobbyScene.ECS.Components.MovingAwardComponent;

namespace Code.Scenes.LobbyScene.ECS.Systems.Execute
{
    /// <summary>
    /// Создаёт компоненты c информацией про награды и сообщение о том, что нужно показать текст.
    /// </summary>
    public class MovingAwardsMainSystem : IExecuteSystem
    {
        private readonly object lockObj = new object();
        private readonly LobbyUiContext lobbyUiContext;
        private RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown;

        public MovingAwardsMainSystem(Contexts contexts)
        {
            lobbyUiContext = contexts.lobbyUi;
        }
        
        public void CreateAwards(RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShownArgs)
        {
            lock (lockObj)
            {
                rewardsThatHaveNotBeenShown = rewardsThatHaveNotBeenShownArgs;
            }
        }
        
        public void Execute()
        {
            lock (lockObj)
            {
                if (rewardsThatHaveNotBeenShown != null)
                {
                    DateTime startSpawnTime = DateTime.Now;

                    if (rewardsThatHaveNotBeenShown.AccountRatingDelta > 0)
                    {
                        AddAwardSpawnCommand(AwardType.AccountRating, rewardsThatHaveNotBeenShown.AccountRatingDelta, startSpawnTime);
                        startSpawnTime += TimeSpan.FromSeconds(2);
                    }

                    if (rewardsThatHaveNotBeenShown.SoftCurrencyDelta > 0)
                    {
                        AddAwardSpawnCommand(AwardType.SoftCurrency, rewardsThatHaveNotBeenShown.SoftCurrencyDelta, startSpawnTime);
                        startSpawnTime += TimeSpan.FromSeconds(2);
                    }
                    
                    if (rewardsThatHaveNotBeenShown.HardCurrencyDelta > 0)
                    {
                        AddAwardSpawnCommand(AwardType.HardCurrency, rewardsThatHaveNotBeenShown.HardCurrencyDelta, startSpawnTime);
                        startSpawnTime += TimeSpan.FromSeconds(2);
                    }
                    
                    if (rewardsThatHaveNotBeenShown.LootboxPointsDelta > 0)
                    {
                        AddAwardSpawnCommand(AwardType.LootboxPoints, rewardsThatHaveNotBeenShown.LootboxPointsDelta, startSpawnTime);
                        startSpawnTime += TimeSpan.FromSeconds(2);
                    }
                    
                    //TODO добавить другие виды наград
                    
                    rewardsThatHaveNotBeenShown = null;
                }
            }
        }

        private void AddAwardSpawnCommand(AwardType awardType, int quantity, DateTime spawnStartTime)
        {
            LobbyUiEntity entity = lobbyUiContext.CreateEntity();
            entity.AddCommandToCreateAwardImages(quantity, awardType , spawnStartTime);
        }
    }
}