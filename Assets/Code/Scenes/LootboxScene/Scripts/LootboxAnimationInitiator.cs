using System;
using System.Collections;
using Code.Common.Logger;
using Code.Scenes.LobbyScene.Scripts.Listeners;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;

namespace Code.Scenes.LootboxScene.Scripts
{
    /// <summary>
    /// Стартует анимацию после того как данные о сундуке были получены в LootboxModelDownloader.
    /// </summary>
    public class LootboxAnimationInitiator : MonoBehaviour
    {
        private LootboxEcsController lootboxEcsController;
        private readonly ILog log = LogManager.CreateLogger(typeof(LootboxAnimationInitiator));

        protected void Awake()
        {
            lootboxEcsController = FindObjectOfType<LootboxEcsController>()
                ?? throw new NullReferenceException(nameof(lootboxEcsController));
        }

        private void Start()
        {
            StartCoroutine(ShowResourcesAccrual());
        }

        private IEnumerator ShowResourcesAccrual()
        {
            yield return new WaitUntil(()=>LootboxModelDownloader.Instance.IsDownloadingCompleted());
            LootboxModel lootboxModel = LootboxModelDownloader.Instance.GetLootboxModel();
            if (lootboxModel == null)
            {
                log.Error("Не удалось скачать данные о лутбоксе");
                yield break; 
            }

            log.Info("Данные о лутбоксе успешно скачаны");
            StartAnimation(lootboxModel);
        }

        private void StartAnimation(LootboxModel lootboxModelArg)
        {
            log.Info("Старт анимации");
            foreach (LootboxPrizeModel prize in lootboxModelArg.Prizes)
            {
                log.Info($"{nameof(prize.LootboxPrizeType)} {prize.LootboxPrizeType} {nameof(prize.Quantity)} {prize.Quantity}");
            }
            
            lootboxEcsController.SetLootboxModel(lootboxModelArg);
        }
    }
}