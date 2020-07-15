﻿using System;
using System.Collections.Generic;
using Code.Common;
using Code.Scenes.BattleScene.ECS.Systems.TearDownSystems;
using Code.Scenes.BattleScene.ECS.Systems.ViewSystems;
using Code.Scenes.LobbyScene.ECS.Components.CommonLayoutSwitcher;
using Code.Scenes.LobbyScene.ECS.Components.WarshipsList;
using Code.Scenes.LobbyScene.ECS.Components.WarshipsUi.WarshipOverview;
using Code.Scenes.LobbyScene.ECS.Shop;
using Code.Scenes.LobbyScene.ECS.Systems.Execute;
using Code.Scenes.LobbyScene.ECS.Systems.Initialize;
using Code.Scenes.LobbyScene.ECS.Systems.Reactive;
using Code.Scenes.LobbyScene.ECS.Systems.Reactive.AccountInfoChangingHandlers;
using Code.Scenes.LobbyScene.ECS.Systems.Reactive.Warships;
using Code.Scenes.LobbyScene.ECS.Systems.TearDown;
using Entitas;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;

namespace Code.Scenes.LobbyScene.Scripts
{
    /// <summary>
    /// Создаёт ecs системы
    /// Вызывает системы
    /// Пропускает через себя запросы на создание компонентов 
    /// </summary>
    public class LobbyEcsController : MonoBehaviour
    {
        private Systems systems;
        private Contexts contexts;
        private ShopUiStorage shopUiStorage;
        private ShopUiSpawner shopUiSpawner;
        private LobbyUiStorage lobbyUiStorage;
        private UiLayersStorage uiLayersStorage;
        private WarshipsUiStorage warshipsUiStorage;
        private LobbySceneSwitcher lobbySceneSwitcher;
        private LobbyLayoutSwitcher lobbyLayoutSwitcher;
        private WarshipSpawnerSystem warshipSpawnerSystem;
        private MovingAwardsMainSystem movingAwardsMainSystem;
        private MovingAwardsUiElementsStorage movingAwardsUiStorage;
        private PurchaseConfirmationWindow purchaseConfirmationWindow;
        private MatchSearchDataUpdaterSystem matchSearchDataUpdaterSystem;
        private AccountDataComponentsCreatorSystem accountDataComponentsCreatorSystem;
        private MovingAwardImagesDataCreationSystem movingAwardImagesDataCreationSystem;
        private readonly ILog log = LogManager.CreateLogger(typeof(LobbyEcsController));
        private StartCancelMatchComponentsCreatorSystem startCancelMatchComponentsCreatorSystem;

        private void Awake()
        {
            lobbyUiStorage = FindObjectOfType<LobbyUiStorage>()
                            ?? throw new NullReferenceException(nameof(LobbyUiStorage));
            uiLayersStorage = FindObjectOfType<UiLayersStorage>()
                            ?? throw new NullReferenceException(nameof(UiLayersStorage));
            shopUiStorage = FindObjectOfType<ShopUiStorage>()
                            ?? throw new NullReferenceException(nameof(ShopUiStorage));
            movingAwardsUiStorage = FindObjectOfType<MovingAwardsUiElementsStorage>()
                            ?? throw new NullReferenceException(nameof(MovingAwardsUiElementsStorage));
            warshipsUiStorage = FindObjectOfType<WarshipsUiStorage>()
                            ?? throw new NullReferenceException(nameof(WarshipsUiStorage));
            purchaseConfirmationWindow = FindObjectOfType<PurchaseConfirmationWindow>()
                            ?? throw new NullReferenceException(nameof(PurchaseConfirmationWindow));
            shopUiSpawner = FindObjectOfType<ShopUiSpawner>()
                            ?? throw new NullReferenceException(nameof(shopUiSpawner));
            lobbySceneSwitcher = FindObjectOfType<LobbySceneSwitcher>()
                            ?? throw new NullReferenceException(nameof(lobbySceneSwitcher));
        }

        private void Start()
        {
            lobbyUiStorage.Check();
            uiLayersStorage.Check();
            movingAwardsUiStorage.Check();
            warshipsUiStorage.Check();

            contexts = Contexts.sharedInstance;
            
            warshipSpawnerSystem = new WarshipSpawnerSystem(contexts, lobbyUiStorage.warshipsRoot);
            accountDataComponentsCreatorSystem = new AccountDataComponentsCreatorSystem(contexts);

            startCancelMatchComponentsCreatorSystem = new StartCancelMatchComponentsCreatorSystem(contexts.lobbyUi);
            matchSearchDataUpdaterSystem = new MatchSearchDataUpdaterSystem(contexts);
            
            movingAwardImagesDataCreationSystem = new MovingAwardImagesDataCreationSystem(contexts,
                movingAwardsUiStorage.movingAwardImageParentRectTransform);
            
            movingAwardsMainSystem = new MovingAwardsMainSystem(contexts);

            lobbyLayoutSwitcher = new LobbyLayoutSwitcher(contexts.lobbyUi, purchaseConfirmationWindow);
            systems = new Systems()
                    
                    //Движение наград
                    .Add(movingAwardsMainSystem)
                    
                    //Движение текта
                    .Add(new MovingAwardsTextCreationSystem(contexts))
                    .Add(new MovingAwardsTextSpawnSystem(contexts, movingAwardsUiStorage.movingAwardTextParentRectTransform))
                    .Add(new MovingAwardsTextDataUpdaterSystem(contexts))
                    .Add(new MovingAwardsTextGameObjectUpdaterSystem(contexts))
                    .Add(new MovingAwardsTextDeleteSystem(contexts))
                    
                    //Движение наград
                    .Add(movingAwardImagesDataCreationSystem)
                    .Add(new MovingAwardImagesGameObjectCreationSystem(contexts,  movingAwardsUiStorage.movingAwardImageParentRectTransform))
                    .Add(new MovingAwardImageDataUpdaterSystem(contexts))
                    .Add(new MovingAwardGameObjectUpdaterSystem(contexts, movingAwardsUiStorage.movingAwardImageUpperObject))
                    .Add(new MovingAwardImageDestroySystem(contexts))
                    
                    //Поиск матча
                    .Add(matchSearchDataUpdaterSystem)
                    .Add(new MatchSearchTimeUpdaterSystem(contexts.lobbyUi,lobbyUiStorage.waitTimeText))
                    .Add(new MatchSearchMenuUpdaterSystem(contexts.lobbyUi,
                        lobbyUiStorage.numberOfPlayersInQueueText, lobbyUiStorage.numberOfPlayersInBattlesText))
                    
                    //Обработка start/stop
                    .Add(startCancelMatchComponentsCreatorSystem)
                    .Add(new StartButtonHandler(contexts.lobbyUi, lobbyUiStorage.battleLoadingMenu, lobbyUiStorage.lobbySoundsManager))
                    .Add(new CancelButtonHandler(contexts.lobbyUi, lobbyUiStorage.battleLoadingMenu, lobbyUiStorage.lobbySoundsManager))
                    
                    //Установка размытия при поике матча
                    .Add(new BlurInitializeSystem(contexts.lobbyUi))
                    .Add(new BlurImageStateUpdaterSystem(contexts.lobbyUi))
                    .Add(new BlurValueUpdatingSystem(contexts.lobbyUi, lobbyUiStorage.blurMaterial))
                    .Add(new BlurImageDisableHandler(contexts.lobbyUi, lobbyUiStorage.blurImage))
                    .Add(new BlurImageEnableHandler(contexts.lobbyUi, lobbyUiStorage.blurImage))
                    
                    //Анимация кнопки START
                    .Add(new StartButtonAnimationSystem(contexts.lobbyUi, lobbyUiStorage.startButtonSatellites))

                    //Обновление статистики в ui (валюты, рейтинг, сундуки)
                    .Add(accountDataComponentsCreatorSystem)
                    .Add(new HardCurrencyChangingHandler(contexts.lobbyUi, lobbyUiStorage.hardCurrencyText))
                    .Add(new SoftCurrencyChangingHandler(contexts.lobbyUi, lobbyUiStorage.softCurrencyText))
                    .Add(new UsernameChangingHandler(contexts.lobbyUi, lobbyUiStorage.usernameText))
                    .Add(new LootboxPointsChangingHandler(contexts.lobbyUi, lobbyUiStorage.smallLootboxText,  lobbyUiStorage.smallLootboxPinGameObject, lobbyUiStorage.smallLootboxPinText))
                    .Add(new LootboxSliderChangingHandler(contexts.lobbyUi, lobbyUiStorage.lootboxSlider))
                    .Add(new AccountRatingChangingHandler(contexts.lobbyUi, lobbyUiStorage.accountRatingText))
                    
                    //Отрисовка кораблей
                    .Add(warshipSpawnerSystem)
                    .Add(new RenderSpriteSystem(contexts))
                    .Add(new RenderTransformSystem(contexts))
                    .Add(new SetAnimatorSystem(contexts))
                    
                    //Заполненеи списка кораблей
                    .Add(new WarshipListFillerSystem(contexts, warshipsUiStorage, this))
                    
                    //Листание кораблей
                    .Add(new ShiftWarshipsLeftReactiveSystem(contexts, lobbyUiStorage.lobbySoundsManager))
                    .Add(new ShiftWarshipsRightReactiveSystem(contexts, lobbyUiStorage.lobbySoundsManager))
                    .Add(new WarshipsMoverSystem(contexts))
                    //Включение/выключение кнопок после листания кораблей
                    .Add(new ScrollButtonSwitcherSystem(contexts, lobbyUiStorage.buttonScrollLeft, 
                        lobbyUiStorage.buttonScrollRight))
                    //Обновление рейтинга и ранга для текущего корабля
                    .Add(new WarshipDataUpdaterSystem(contexts, lobbyUiStorage.rankText,
                        lobbyUiStorage.ratingText, lobbyUiStorage.ratingSlider))

                    //Переключение слоёв ui 
                    .Add(lobbyLayoutSwitcher)
                    
                    //Магазин
                    .Add(new ShopUiLayerEnablingSystem(contexts.lobbyUi,uiLayersStorage, shopUiStorage, lobbyLayoutSwitcher, shopUiSpawner))
                    .Add(new ShopUiLayerDisablingSystem(contexts.lobbyUi,uiLayersStorage, lobbyLayoutSwitcher))
                    
                    //Список кораблей
                    .Add(new WarshipListEnablingSystem(contexts.lobbyUi, lobbyLayoutSwitcher,  uiLayersStorage))
                    .Add(new WarshipListDisablingSystem(contexts.lobbyUi, lobbyLayoutSwitcher,  uiLayersStorage))
                    
                    //Обзор корабля
                    .Add(new WarshipOverviewEnablingSystem(contexts, warshipsUiStorage, lobbyLayoutSwitcher, this))
                    .Add(new WarshipOverviewDisablingSystem(contexts.lobbyUi, warshipsUiStorage, lobbyLayoutSwitcher))
                    
                    //Переключение скинов корабля
                    .Add(new ShiftSkinRightSystem(contexts, lobbyUiStorage.lobbySoundsManager))
                    .Add(new ShiftSkinLeftSystem(contexts, lobbyUiStorage.lobbySoundsManager))
                    .Add(new SkinButtonsSwitcherSystem(contexts, warshipsUiStorage))
                    .Add(new SkinSwitcherSystem(contexts, warshipsUiStorage))
                    .Add(new SkinChangingNotifierSystem(contexts))
                    
                    
                    //Модальное окно с характеристиками корабля
                    .Add(new WarshipOverviewModalWindowEnablingSystem(contexts.lobbyUi, warshipsUiStorage, lobbyLayoutSwitcher))
                    .Add(new WarshipOverviewModalWindowDisablingSystem(contexts.lobbyUi, warshipsUiStorage,lobbyLayoutSwitcher))
                    
                    //Модальное окно улучшения корабля
                    .Add(new WarshipImprovementModalWindowEnablingSystem(contexts.lobbyUi, warshipsUiStorage, lobbyLayoutSwitcher, lobbySceneSwitcher))
                    .Add(new WarshipImprovementModalWindowDisablingSystem(contexts.lobbyUi, warshipsUiStorage, lobbyLayoutSwitcher))
                    
                    //Слушатель кнопки покупки улучшения
                    .Add(new WarshipImproveOnClickSystem(contexts.lobbyUi))
                    
                    //Очистка
                    .Add(new ContextsClearSystem(contexts))
                    .Add(new ClearLobbyUiSystem(contexts.lobbyUi))
                ;
            
        
            systems.ActivateReactiveSystems();    
            systems.Initialize();
            
            contexts.lobbyUi.CreateEntity().messageDisableWarshipImprovementModalWindow = true;
            contexts.lobbyUi.CreateEntity().messageDisableWarshipOverviewModalWindow = true;
            contexts.lobbyUi.CreateEntity().messageDisableWarshipOverviewUiLayer = true;
            contexts.lobbyUi.CreateEntity().messageDisableWarshipListUiLayer = true;
            contexts.lobbyUi.CreateEntity().messageDisableShopUiLayer = true;
        }

        private void Update()
        {
            if (systems != null)
            {
                systems.Execute();
                systems.Cleanup();
            }
            else
            {
                log.Error($"{nameof(systems)} was null");
            }
        }

        private void OnDestroy()
        {
            if (systems != null)
            {
                systems.DeactivateReactiveSystems();
                systems.TearDown();
                systems.ClearReactiveSystems();    
            }
        }
     
        public void ShiftWarshipsRight()
        {
            contexts.lobbyUi.CreateEntity().isShiftWarshipsRightCommand = true;
        }
        
        public void ShiftWarshipsLeft()
        {
            contexts.lobbyUi.CreateEntity().isShiftWarshipsLeftCommand = true;
        }
        
        public bool IsWarshipsCreationCompleted()
        {
            return warshipSpawnerSystem != null && warshipSpawnerSystem.IsWarshipCreationCompleted();
        }
        
        public void CreateUnshownRewardsComponent(RewardsThatHaveNotBeenShown rewardsThatHaveNotBeenShown)
        {
            movingAwardsMainSystem.CreateAwards(rewardsThatHaveNotBeenShown);
        }
        
        public void SetAccountData(AccountDto accountData)
        {
            accountDataComponentsCreatorSystem.SetAccountData(accountData);
            PlayerIdStorage.AccountId = accountData.AccountId;
        }

        public int GetCurrentWarshipId()
        {
            return accountDataComponentsCreatorSystem.GetCurrentWarshipId();
        }

        public void Button_Start_Click()
        {
            startCancelMatchComponentsCreatorSystem.Button_Start_Click();
        }

        public void Button_Cancel_Click()
        {
            startCancelMatchComponentsCreatorSystem.Button_Cancel_Click();
        }

        public void SetNewMatchSearchData(int responseNumberOfPlayersInQueue, int responseNumberOfPlayersInBattles)
        {
            matchSearchDataUpdaterSystem.SetNewData(responseNumberOfPlayersInQueue, responseNumberOfPlayersInBattles);
        }

        public bool LootboxCanBeOpened()
        {
            return contexts.lobbyUi.pointsForSmallLootbox.value>=100;
        }

        public void BackButton_OnClick()
        {
            contexts.lobbyUi.CreateEntity().messageBackButtonPressed = true;
        }
        
        public void PhysicalBackButton_OnClick()
        {
            log.Debug(nameof(PhysicalBackButton_OnClick));
        }

        public void ShopButton_OnClick()
        {
            contexts.lobbyUi.CreateEntity().messageEnableShopUiLayer = true;
        }

        public void ShowWarshipList()
        {
            contexts.lobbyUi.CreateEntity().messageEnableWarshipListUiLayer = true;
        }

        public void ShowWarshipOverview(WarshipDto warshipDto)
        {
            contexts.lobbyUi.CreateEntity().AddEnableWarshipOverviewUiLayer(warshipDto);
        }

        public void PurchaseConfirmationWindowWasShown()
        {
            //TODO это костыль
            lobbyLayoutSwitcher.SetCurrentLayer(ShittyUiLayerState.ShopPurchaseConfirmationLayer);
        }

        public void ShowWarshipImprovementModalWindow(WarshipDto warshipDto)
        {
            contexts.lobbyUi.CreateEntity().AddEnableWarshipImprovementModalWindow(warshipDto);
        }

        public void ShowWarshipCharacteristics(WarshipDto warshipDto)
        {
            log.Debug(nameof(ShowWarshipCharacteristics));
            contexts.lobbyUi.CreateEntity().AddEnableWarshipOverviewModalWindow(warshipDto);
        }

        public bool IsSoftCurrencyReady()
        {
            return contexts.lobbyUi.hasSoftCurrency;
        }
        
        public int GetSoftCurrency()
        {
            if (contexts.lobbyUi.hasSoftCurrency)
            {
                return contexts.lobbyUi.softCurrency.value;
            }
            else
            {
                return 0;
            }
        }
        
        public int GetHardCurrency()
        {
            return contexts.lobbyUi.hardCurrency.value;
        }

        public void ShiftSkinLeft()
        {
            contexts.lobbyUi.CreateEntity().messageShiftSkinLeft = true;
        }

        public void ShiftSkinRight()
        {
            contexts.lobbyUi.CreateEntity().messageShiftSkinRight = true;
        }
    }
}