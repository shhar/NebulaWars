using Code.Common.Logger;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scenes.LobbyScene.Scripts.Shop.PurchaseConfirmation.UiWindow
{
    public class SoftCurrencyPurchaseConfirmationWindowController
    {
        private readonly InGameCurrencyPaymaster inGameCurrencyPaymaster;

        private readonly ILog log =
            LogManager.CreateLogger(typeof(WarshipPowerPointsPurchaseConfirmationWindowController));

        public SoftCurrencyPurchaseConfirmationWindowController(InGameCurrencyPaymaster inGameCurrencyPaymaster)
        {
            this.inGameCurrencyPaymaster = inGameCurrencyPaymaster;
        }

        public void Spawn(PurchaseModel purchaseModel, Transform parent)
        {
            GameObject softCurrencyContentPrefab = Resources
                .Load<GameObject>("Prefabs/LobbyShop/PurchasesConfirmation/SoftCurrencyContent");
            GameObject softCurrencyContent = Object.Instantiate(softCurrencyContentPrefab, parent, false);
            Button buttonBuy = softCurrencyContent.transform.Find("Button_Buy").GetComponent<Button>();
            FillData(softCurrencyContent, purchaseModel.productModel);
            AddListeners(buttonBuy, purchaseModel);
        }

        private void FillData(GameObject softCurrencyContent, ProductModel productModel)
        {
            //установить картинку
            Image image = softCurrencyContent.transform.Find("Image_ItemPreviewBg/Image_ItemPreview")
                .GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>(productModel.ImagePreviewPath);
            
            //установить прибавляемое кол-во товара
            Text textLabel = softCurrencyContent.transform.Find("Image_ItemPreviewBg/Image_ItemPreview/Text_Label")
                .GetComponent<Text>();
            textLabel.text = productModel.Name;
            
            //установить описание
            Text description = softCurrencyContent.transform.Find("Text_Description").GetComponent<Text>();
            description.text = $"Power points: {productModel.Name}. Collect power points to activate improvements for the spacecraft.";
            //установить цену
            Text cost = softCurrencyContent.transform.Find("Button_Buy/Text_Cost").GetComponent<Text>();
            cost.text = productModel.CostString;
            //TODO сделать установку типа валюты
        }

        private void AddListeners(Button buttonBuy, PurchaseModel purchaseModel)
        {
            //установить слушатель на кнопку покупки
            buttonBuy.onClick.RemoveAllListeners();
            buttonBuy.onClick.AddListener(() =>
            {
                inGameCurrencyPaymaster.StartBuying(purchaseModel);
            });
        }
    }
}