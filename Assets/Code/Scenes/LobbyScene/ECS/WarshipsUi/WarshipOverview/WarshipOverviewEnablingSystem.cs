using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Common;
using Code.Common.Logger;
using Code.Common.Statistics;
using Code.Scenes.LobbyScene.ECS.CommonLayoutSwitcher;
using Code.Scenes.LobbyScene.ECS.WarshipsUi.WarshipOverview.Skins;
using Code.Scenes.LobbyScene.Scripts;
using Code.Scenes.LobbyScene.Scripts.WarshipsUi;
using Entitas;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scenes.LobbyScene.ECS.WarshipsUi.WarshipOverview
{
    public class WarshipOverviewEnablingSystem : ReactiveSystem<LobbyUiEntity>
    {
        private readonly LobbyUiContext lobbyUiContext;
        private readonly WarshipsUiStorage warshipsUiStorage;
        private readonly LobbyEcsController lobbyEcsController;
        private readonly LobbyLayoutSwitcher lobbyLayoutSwitcher;
        private readonly ILog log = LogManager.CreateLogger(typeof(WarshipOverviewEnablingSystem));

        public WarshipOverviewEnablingSystem(Contexts contexts, WarshipsUiStorage warshipsUiStorage,
           LobbyLayoutSwitcher lobbyLayoutSwitcher, LobbyEcsController lobbyEcsController) 
            : base(contexts.lobbyUi)
        {
            this.warshipsUiStorage = warshipsUiStorage;
            this.lobbyLayoutSwitcher = lobbyLayoutSwitcher;
            this.lobbyEcsController = lobbyEcsController;
            lobbyUiContext = contexts.lobbyUi;
        }
        
        protected override ICollector<LobbyUiEntity> GetTrigger(IContext<LobbyUiEntity> context)
        {
            return context.CreateCollector(LobbyUiMatcher.EnableWarshipOverviewUiLayer);
        }

        protected override bool Filter(LobbyUiEntity entity)
        {
            return entity.hasEnableWarshipOverviewUiLayer;
        }

        protected override void Execute(List<LobbyUiEntity> entities)
        {
            // log.Debug(nameof(WarshipOverviewEnablingSystem));
            LobbyUiEntity entity = entities.Last();
            ShowOverviewLayer(entity.enableWarshipOverviewUiLayer.WarshipDto);
            lobbyLayoutSwitcher.SetCurrentLayer(ShittyUiLayerState.WarshipOverview);
        }
        
        private void ShowOverviewLayer(WarshipDto warshipDto)
        {
            warshipsUiStorage.hint.SetActive(false);
            ShowWarshipOverview(warshipDto);
            warshipsUiStorage.warshipListRootGameObject.SetActive(false);
            warshipsUiStorage.warshipRootGameObject.SetActive(true);
            warshipsUiStorage.warshipRoot.SetActive(true);
        }
        
        private void ShowWarshipOverview(WarshipDto warshipDto)
        {
            UpdateData(warshipDto);
            
            //Обновить уникальный компонент для листания и переключения скинов
            lobbyUiContext.ReplaceWarshipOverviewCurrentSkinModel( warshipDto.CurrentSkinIndex, warshipDto);
        }
        
        private void UpdateData(WarshipDto warshipDto)
        {
            //Установить название корабля
            warshipsUiStorage.warshipName.text = warshipDto.WarshipName.ToUpper();
            //Установить тип корабля
            warshipsUiStorage.warshipTypeName.text = warshipDto.CombatRoleName;
            //Установить ранг корабля
            WarshipRankModel rankModel = WarshipRatingScaleStorage.Instance.GetRankModel(warshipDto.Rating);
            warshipsUiStorage.warshipRank.text = rankModel.currentRank.ToString();
            //Установить слайдер для рейтинга
            warshipsUiStorage.trophySlider.value = rankModel.rankProgress;
            //Установить рейтинг корабля
            warshipsUiStorage.trophyText.text = rankModel.ToString();
            //Установить описание корабля
            warshipsUiStorage.warshipDescription.text = warshipDto.Description;
            //Установить уровень силы
            warshipsUiStorage.warshipPowerLevel.text = "POWER " + warshipDto.PowerLevel;
            //TODO Установить характеристики корабля (атаку, защиту, скорость или что там)
            
            //Проверить на кол-во ресурсов для перехода на новый уровень
            int softCurrency = lobbyEcsController.GetSoftCurrency();
            var model = WarshipPowerScale.GetModel(warshipDto.PowerLevel);
            int improvementCost = model.SoftCurrencyCost;
            int maxPowerPoints = model.PowerPointsCost;
                
            bool showImproveAnimation = softCurrency >= improvementCost && warshipDto.PowerPoints >= maxPowerPoints;

            Text powerPointsValueText;
            //Показать нужную шкалу. (Красную или зелёную)
            if (showImproveAnimation)
            {
                powerPointsValueText = warshipsUiStorage.greenPowerPointsValueText;
                warshipsUiStorage.redScale.SetActive(false);
                warshipsUiStorage.greenScale.SetActive(true);
            }
            else
            {
                warshipsUiStorage.redScale.SetActive(true);
                warshipsUiStorage.greenScale.SetActive(false);
                powerPointsValueText = warshipsUiStorage.powerValueText;
            }

            //Установить кол-во очков силы для текущего уровня
            powerPointsValueText.text =  warshipDto.PowerPoints + "/" + maxPowerPoints;

            //Установить слайдер для кол-ва очков силы
            warshipsUiStorage.powerSlider.maxValue = maxPowerPoints;
            warshipsUiStorage.powerSlider.value = warshipDto.PowerPoints;
            
            //Установить цену улучшения
            warshipsUiStorage.improveButtonCost.text = improvementCost.ToString();
            //Установить слушатель для кнопки улучшения
            warshipsUiStorage.improveButton.onClick.RemoveAllListeners();
            warshipsUiStorage.improveButton.onClick.AddListener(() =>
            {
                if (warshipDto.PowerPoints >= maxPowerPoints)
                {
                    //показать окно покупки улучшения
                    lobbyEcsController.ShowWarshipImprovementModalWindow(warshipDto);
                }
                else
                {
                    UiSoundsManager.Instance().PlayError();
                    //показать всплывающую подсказку о невозможности операции
                    if (!warshipsUiStorage.hint.activeSelf)
                    {
                        warshipsUiStorage.hint.SetActive(true);
                        Task.Run(async () =>
                        {
                            await Task.Delay(2000);
                            UnityThread.Execute(() => { warshipsUiStorage.hint.SetActive(false); });
                        });    
                    }
                }
            });
            //Установить слушатель для кнопки выбора корабля   
            warshipsUiStorage.chooseButton.onClick.RemoveAllListeners();
            warshipsUiStorage.chooseButton.onClick.AddListener(() =>
            {
                log.Debug("Слушатель работает");
                //todo звук
                //заменить скин если нужно
                int actualSkinIndex = lobbyUiContext.warshipOverviewCurrentSkinModel.skinIndex;
                if (actualSkinIndex != warshipDto.CurrentSkinIndex)
                {
                    warshipDto.CurrentSkinIndex = actualSkinIndex;
                    int warshipId = warshipDto.Id;
                    string skinName = warshipDto.GetCurrentSkinName();
                    var task = new SkinChangingNotifier().ChangeSkinOnServerAsync(warshipId, skinName);
                }
                else
                {
                    log.Debug("Скин не был изменён");
                }
                //изменить индекс текущего корабля
                ushort warshipIndex = lobbyEcsController.GetWarshipIndexById(warshipDto.Id);
                lobbyUiContext.ReplaceCurrentWarshipIndex(warshipIndex);
                //заменть компонент корабля
                var warshipEntity = lobbyUiContext.GetGroup(LobbyUiMatcher.Warship)
                    .AsEnumerable()
                    .Single(entity => entity.warship.warshipDto.Id == warshipDto.Id);
                warshipEntity.Destroy();
                lobbyUiContext.CreateEntity().AddWarship(warshipIndex, warshipDto);
                //выключить меню обзора корабля
                lobbyUiContext.CreateEntity().messageDisableWarshipOverviewUiLayer = true;
                //выключить меню со списком кораблей
                lobbyUiContext.CreateEntity().messageDisableWarshipListUiLayer = true;
            });
            
            //Установить стоимость для кнопки покупки улучшения
            warshipsUiStorage.popupWindowCostText.text = improvementCost.ToString();
            
            //Установить слушатель для кнопки покупки улучшения
            warshipsUiStorage.popupWindowBuyButton.onClick.RemoveAllListeners();
            warshipsUiStorage.popupWindowBuyButton.onClick.AddListener(()=>
            {
                //todo показать окно подтверждения
            });
            
            //Установить слушатель для меню с характеристиками корабля
            warshipsUiStorage.warshipCharacteristicsButton.onClick.RemoveAllListeners();
            warshipsUiStorage.warshipCharacteristicsButton.onClick.AddListener(()=>
            {
                lobbyEcsController.ShowWarshipCharacteristics(warshipDto);
            });
        }
    }
}