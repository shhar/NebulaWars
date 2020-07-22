using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.Common.Logger;
using Code.Scenes.LootboxScene.PrefabScripts;
using Code.Scenes.LootboxScene.PrefabScripts.Wpp;
using Code.Scenes.LootboxScene.Scripts;
using Entitas;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;
using ZeroFormatter;

namespace Code.Scenes.LootboxScene.ECS.Systems
{
    /// <summary>
    /// Показывает новый ресурс.
    /// </summary>
    public class ShowPrizeSystem:ReactiveSystem<LootboxEntity>
    {
        private LobbyUiContext lobbyContext;
        private readonly LootboxUiStorage uiStorage;
        private readonly ParticlesColorUpdater particlesColorUpdater;
        private readonly ILog log = LogManager.CreateLogger(typeof(ShowPrizeSystem));

        public ShowPrizeSystem(Contexts contexts, LootboxUiStorage uiStorage,
            ParticlesColorUpdater particlesColorUpdater) 
            : base(contexts.lootbox)
        {
            this.uiStorage = uiStorage;
            lobbyContext = contexts.lobbyUi;
            this.particlesColorUpdater = particlesColorUpdater;
        }
        
        protected override ICollector<LootboxEntity> GetTrigger(IContext<LootboxEntity> context)
        {
            return context.CreateCollector(LootboxMatcher.ShowPrize.Added());
        }

        protected override bool Filter(LootboxEntity entity)
        {
            return entity.hasShowPrize;
        }

        protected override void Execute(List<LootboxEntity> entities)
        {
            //очистить
            ClearResources();
            //начать показывать текущий приз
            LootboxEntity currentPrize = entities.Last();
            ShowPrize(currentPrize);
        }

        private void ClearResources()
        {
            uiStorage.resourcesRoot.transform.DestroyAllChildren();
        }

        private void ShowPrize(LootboxEntity currentPrize)
        {
            ShowPrizeComponent prize = currentPrize.showPrize;
            Transform parent = uiStorage.resourcesRoot.transform;
            
            switch (prize.LootboxPrizeModel.LootboxPrizeType)
            {
                case LootboxPrizeType.SoftCurrency:
                {
                    var go = UnityEngine.Object.Instantiate(uiStorage.softCurrencyPrefab, parent);
                    var script = go.GetComponent<SoftCurrencyAccrual>();
                    LootboxSoftCurrencyModel lootboxSoftCurrencyModel =
                        ZeroFormatterSerializer.Deserialize<LootboxSoftCurrencyModel>(prize.LootboxPrizeModel
                            .SerializedModel);
                    script.SetData(lootboxSoftCurrencyModel.Amount);
                    particlesColorUpdater.SetStartColor(Color.blue);
                    break;
                }
                case LootboxPrizeType.HardCurrency:
                {
                    var go = UnityEngine.Object.Instantiate(uiStorage.hardCurrencyPrefab, parent);
                    var script = go.GetComponent<HardCurrencyAccrual>();
                    LootboxHardCurrencyModel lootboxHardCurrencyModel =
                        ZeroFormatterSerializer.Deserialize<LootboxHardCurrencyModel>(prize.LootboxPrizeModel
                            .SerializedModel);
                    script.SetData(lootboxHardCurrencyModel.Amount);

                    Color purple = new Color(209, 0, 4);
                    particlesColorUpdater.SetStartColor(purple);
                    break;
                }
                case LootboxPrizeType.WarshipPowerPoints:
                {
                    var go = UnityEngine.Object.Instantiate(uiStorage.warshipPowerPointsPrefab, parent);
                    var script = go.GetComponent<WarshipPowerPointsAccrual>();
                    var lootboxWarshipPowerPointsModel =
                        ZeroFormatterSerializer.Deserialize<LootboxWarshipPowerPointsModel>(prize.LootboxPrizeModel
                            .SerializedModel);
                    
                    log.Debug(lootboxWarshipPowerPointsModel.StartValue);
                    log.Debug(lootboxWarshipPowerPointsModel.FinishValue);
                    log.Debug(lootboxWarshipPowerPointsModel.WarshipId);
                    log.Debug(lootboxWarshipPowerPointsModel.WarshipSkinName);
                    log.Debug(lootboxWarshipPowerPointsModel.MaxValueForLevel);
                    
                    script.SetData(lootboxWarshipPowerPointsModel);
                    Color red = new Color(209, 0, 0);
                    particlesColorUpdater.SetStartColor(red);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}